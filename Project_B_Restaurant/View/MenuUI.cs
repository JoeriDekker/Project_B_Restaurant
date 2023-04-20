using System.Text;
using System.Text.Json;
public class MenuUI : UI
{
    private static MenuController menu = new MenuController();
    private static InventoryController inventory = new InventoryController();
    private int _index;

    private bool _filter;

    private bool _detailedView;


    public int Index
    {
        get => _index;
        // Clamps the value between a min and max.
        set => _index = Math.Clamp(
            value,
            0,
            // Upper limit is Length minus Step or 0. Whichever is highest.
            (inventory.Dishes.Count - Step) > 0 ? inventory.Dishes.Count - Step : 0);
    }

    public int Step
    {
        get => _detailedView ? 5 : 10;
    }

    public override string Header
    {
        get => @"
      __  __                  
     |  \/  |                 
     | \  / | ___ _ __  _   _ 
     | |\/| |/ _ \ '_ \| | | |
     | |  | |  __/ | | | |_| |
     |_|  |_|\___|_| |_|\__,_|
    ==========================";
    }

    public override string SubText
    {
        get => _detailedView ? DetailedViewOfDishes() : NormalViewOfDishes();
    }

    public MenuUI(UI previousUI) : base(previousUI)
    {
        Index = 0;
    }

    public string NormalViewOfDishes()
    {
        // Strings and appending are a match made in hell so we need a stringbuilder.
        StringBuilder sb = new();
        // setting header and padding left
        string header = String.Format("{0,-11}| {1,-30}| {2,-48}| {3,-25}| {4,-16}| {5,-17}|",
                                    "\x1b[1mID\x1b[0m", "\x1b[1mName\x1b[0m", "\x1b[1mIngredients\x1b[0m", "\x1b[1mAllergies\x1b[0m", "\x1b[1mPrice\x1b[0m", "\x1b[1mType\x1b[0m");
        // divider set to headers length minus the width of the bold escape characters times amount of occurences.
        string divider = new('=', header.Length - 8 * 6);

        sb.AppendLine(header);
        sb.AppendLine(divider);
        Console.OutputEncoding = System.Text.Encoding.Unicode;


        for (int i = Index; i < Index + Step && i < inventory.Dishes.Count; i++)
        {
            Dish dish = inventory.Dishes[i];
            // Check how many ingredients can be displayed in our arbitrarily set width
            string ingredients = GetMaxItemsToPrint(dish.Ingredients, 36);

            string row = $"{dish.ID,3}| {dish.Name,22}| {ingredients,40}| {dish.Allergies,17}|  €{dish.Price,-7}| {dish.Type,9}|";

            sb.AppendLine(row);
        }
        return sb.ToString();
    }

    public string DetailedViewOfDishes()
    {
        StringBuilder sb = new();
        string header = "================================================================";
        sb.AppendLine(header);

        for (int i = Index; i < Index + Step && i < inventory.Dishes.Count; i++)
        {
            Dish dish = inventory.Dishes[i];
            string details =
@$"ID: {dish.ID}
Name: {dish.Name}
Ingredients: {string.Join(", ", dish.Ingredients)}
Allergies: {dish.Allergies}
Price: €{dish.Price}
Type: {dish.Type}
Max amount of pre-order: {dish.MaxAmountPreOrder}
================================================================";
            sb.AppendLine(details);
        }
        return sb.ToString();
    }


    private void UpdateIndexBy(int amount) => Index += amount;

    private string GetMaxItemsToPrint(List<string> items, int maxLength)
    {
        // Creating stringbuilder.
        StringBuilder sb = new();
        // Appending first item.
        sb.Append(items[0]);
        for (int i = 1; i < items.Count; i++)
        {
            // Appending the next item to the string.
            sb.Append($", {items[i]}");
            // If we the length of the string passed the maxLength
            if (sb.Length > maxLength)
            {
                // We remove the last addition, add dots and return
                sb.Remove(sb.Length - $", {items[i]}".Length, $", {items[i]}".Length);
                sb.Append("...");
                return sb.ToString();
            }
        }
        return sb.ToString();
    }

    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem("Next items"));
        MenuItems.Add(new MenuItem("Previous items"));
        MenuItems.Add(new MenuItem("Change View"));
        MenuItems.Add(new MenuItem("Sort Menu", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Filter Menu", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Reset Filter", AccountLevel.Guest));
        MenuItems.Add(new MenuItem(Constants.MenuUI.ADD_DISH, AccountLevel.Admin));
        MenuItems.Add(new MenuItem(Constants.MenuUI.REMOVE_DISH, AccountLevel.Admin));
        MenuItems.Add(new MenuItem(Constants.MenuUI.UPDATE_DISH, AccountLevel.Admin));
        MenuItems.Add(new MenuItem(Constants.MenuUI.SHOW_PREORDERS, AccountLevel.Guest));
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Next items":
                UpdateIndexBy(Step);
                break;
            case "Previous items":
                UpdateIndexBy(-Step);
                break;
            case "Change View":
                _detailedView = !_detailedView;
                break;
            case "Sort Menu":
                Sort();
                break;
            case "Filter Menu":
                string toFilter = GetString("Type any combination to filter on");
                inventory.Filter(toFilter.ToLower());
                break;
            case "Reset Filter":
                inventory.Reset();
                break;
            case Constants.MenuUI.ADD_DISH:
                Add();
                break;
            case Constants.MenuUI.REMOVE_DISH:
                Console.WriteLine("Removing a Dish");
                break;
            case Constants.MenuUI.UPDATE_DISH:
                Console.WriteLine("Which Dish do you want to Change? (Give the name of the dish)");
                string? change_dish = Console.ReadLine();
                Dish dish = menu.GetDishByName(change_dish);
                UpdateDishUI updateDishUI = new UpdateDishUI(this, dish);
                updateDishUI.Start();
                break;
            case Constants.MenuUI.SHOW_PREORDERS:
                inventory.ShowPreOrders();
                break;
            case Constants.UI.EXIT:
            case Constants.UI.GO_BACK:
                Exit();
                break;
            default:
                Console.WriteLine("Invalid option");
                Start();
                break;
        }
    }

    private void Sort()
    {
        Console.WriteLine("     1: Sort by ID");
        Console.WriteLine("     2: Sort by Name");
        Console.WriteLine("     3: Sort by Price");
        Console.WriteLine("     4: Sort by Type");
        Console.WriteLine("     0: Go Back");

        int choice = GetInt("Sort by?");
        if (choice == 0) return;
        inventory.SortBy(choice);
    }

    public void Add()
    {
        Console.WriteLine("What is the Dish name?");
        string dish_name = Console.ReadLine() ?? "Unknown";

        Console.WriteLine("What are the Dish ingedients?");
        string dish_ingredients = Console.ReadLine() ?? "Unknown";

        Console.WriteLine("What are the Dish allergies?");
        string dish_allergies = Console.ReadLine() ?? "Unknown";

        Console.WriteLine("What is the Dish price?");
        double dish_price = Convert.ToDouble(Console.ReadLine() ?? "0");

        Console.WriteLine("What is the Dish Type?");
        string? dish_type = Console.ReadLine();
        if (dish_type == null || dish_type == "")
        {
            dish_type = "Unknown";
        }
        menu.Add(dish_name, dish_ingredients.Split(' ').ToList(), dish_allergies, dish_price, dish_type);
    }

    public void Delete()
    {
        Console.WriteLine("Which Dish do you want to remove? (Give the name of the dish)");
        string? remove_dish = Console.ReadLine();
        menu.Delete(remove_dish);
    }

    public void Update()
    {
        Console.WriteLine("Which Dish do you want to Change? (Give the name of the dish)");
        string change_dish = Console.ReadLine() ?? string.Empty;
        if (menu.GetDishByName(change_dish) != null)
        {
            Dish dish = menu.GetDishByName(change_dish);
            while (true)
            {
                Console.WriteLine("What do you want to change?");
                Console.WriteLine("1: Change Name");
                Console.WriteLine("2: Change Ingredients");
                Console.WriteLine("3: Change Allergies");
                Console.WriteLine("4: Change Price");
                Console.WriteLine("5: Change Type");
                Console.WriteLine("6: Change Max amount of pre-order");
                Console.WriteLine("7: Save Changes");
                int choosed_number = Convert.ToInt32(Console.ReadLine());
                if (choosed_number == 1)
                {
                    Console.WriteLine("What is the new dish name?");
                    string? new_dish_name = Console.ReadLine();
                    dish.Name = new_dish_name;
                }
                else if (choosed_number == 2)
                {
                    Console.WriteLine("What are the updated ingredients?");
                    string? new_dish_ingredients = Console.ReadLine();
                    dish.Ingredients = new_dish_ingredients.Split(' ').ToList();
                }
                else if (choosed_number == 3)
                {
                    Console.WriteLine("What are the updated allergies?");
                    string? new_dish_allergies = Console.ReadLine();
                    dish.Allergies = new_dish_allergies;
                }
                else if (choosed_number == 4)
                {
                    Console.WriteLine("What is the new dish price?");
                    double new_dish_price = Convert.ToDouble(Console.ReadLine());
                    dish.Price = new_dish_price;
                }
                else if (choosed_number == 5)
                {
                    Console.WriteLine("What is the new dish type?");
                    string? new_dish_type = Console.ReadLine();
                    dish.Type = new_dish_type;
                }
                else if (choosed_number == 6)
                {
                    Console.WriteLine("What is the new Max amount of pre-order?");
                    int new_max_preoder = Convert.ToInt32(Console.ReadLine());
                    dish.MaxAmountPreOrder = new_max_preoder;
                }
                else if (choosed_number == 7)
                {
                    menu.Update(dish);
                    Console.WriteLine($"{dish.Name} has been updated");
                    break;
                }
                else
                {
                    Console.WriteLine("Input error! Please enter an number");
                }
            }
        }
        else
        {
            Console.WriteLine("Dish does not exist");
            Start();
        }

    }
}
// public void Start()
// {
//     while (true)
//     {

//         Console.WriteLine("What do you want to do?");
//         Console.WriteLine("1: Show Menu");
//         Console.WriteLine("2: Sort Menu // still in progress");
//         Console.WriteLine("3: Filter Menu // still in progress");
//         if (UserLogin.loggedIn == false)
//         {
//             Console.WriteLine("4: Exit");
//         }
//         else
//         {
//             if (AccountsLogic.CurrentAccount.Type == "Customer")
//             {
//                 Console.WriteLine("4: Exit");
//             }
//         }

//         if (UserLogin.loggedIn == true)
//         {
//             if (AccountsLogic.CurrentAccount.Type == "Admin")
//             {
//                 Console.WriteLine("4: Add a Dish");
//                 Console.WriteLine("5: Remove a Dish");
//                 Console.WriteLine("6: Change a Dish");
//                 Console.WriteLine("7: Show PreOrders");
//                 Console.WriteLine("8: Exit");
//             }

//         }
//         int choice = Convert.ToInt32(Console.ReadLine());

//         switch (choice)
//         {
//             case 1:
//                 ShowMenu();
//                 break;
//             case 2:
//                 // Upcoming Sort function
//                 ShowMenu();
//                 break;
//             case 3:
//                 Console.WriteLine("On what type do you want to filter?");
//                 string filter = Console.ReadLine() ?? "";
//                 // Upcoming Filter function
//                 ShowMenu();
//                 break;
//             case 4:
//                 if (UserLogin.loggedIn == false || AccountsLogic.CurrentAccount.Type == "Customer")
//                 {
//                     OpeningUI.Start();
//                     break;
//                 }
//                 else
//                 {
//                     Add();
//                     break;
//                 }

//             case 5:
//                 Delete();
//                 break;
//             case 6:
//                 Update();
//                 break;
//             case 7:
//                 inventory.ShowPreOrders();
//                 break;
//             case 8:
//                 OpeningUI.Start();
//                 break;
//             default:
//                 Console.WriteLine("Invalid choice.");
//                 break;
//         }
//         Console.WriteLine();
//     }
// }

// public void ShowMenu()
// {
//     foreach (Dish dish in menu.GetMenu())
//     {
//         Console.WriteLine($"- {dish.Name}: {dish.Type} (${dish.Price})");
//         Console.WriteLine($" - {dish.Ingredients}");
//     }
// }


// }