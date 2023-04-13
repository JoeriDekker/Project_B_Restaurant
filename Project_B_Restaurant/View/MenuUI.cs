using System.Text;
using System.Text.Json;
public class MenuUI : UI
{
    private static MenuController menu = new MenuController();
    private static InventoryController inventory = new InventoryController();

    public int Index;

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
        get => CreateTableOfDishesNormal();
    }

    public MenuUI(UI previousUI) : base(previousUI)
    {
        Index = 0;
    }

    public string CreateTableOfDishesNormal()
    {
        // Strings and appending are a match made in hell so we need a stringbuilder.
        StringBuilder sb = new();
        // setting header and padding left
        string header = String.Format("{0,-3}| {1,-22}| {2,-58}| {3,-17}| {4,-8}| {5,-9}|",
                                    "ID", "Name", "Ingredients", "Allergies", "Price", "Type");
        // divider set to headers length
        string divider = new('-', header.Length);

        sb.AppendLine(header);
        sb.AppendLine(divider);


        for (int i = Index; i < inventory.Dishes.Count; i++)
        {
            Dish dish = inventory.Dishes[i];
            // We need to check how many ingredients can be displayed in our set width
            int maxAmountOfIngredients = GetMaximumIngredientsToDisplay(dish.Ingredients, 47);
            string row = string.Empty;

            if (dish.Ingredients.Count == maxAmountOfIngredients + 1)
            {
                row = String.Format("{0,3}| {1,22}| {2,50}| {3,17}| €{4,-7}| {5,9}|",
                                    dish.ID,
                                    dish.Name,
                                    string.Join(", ", dish.Ingredients.Take(maxAmountOfIngredients)),
                                    dish.Allergies,
                                    dish.Price,
                                    dish.Type);
            }
            else
            {
                row = String.Format("{0,3}| {1,22}| {2,47}...| {3,17}| €{4,-7}| {5,9}|",
                                    dish.ID,
                                    dish.Name,
                                    string.Join(", ", dish.Ingredients.Take(maxAmountOfIngredients)),
                                    dish.Allergies,
                                    dish.Price,
                                    dish.Type);
            }

            sb.AppendLine(row);
        }
        return sb.ToString();
    }

    private int GetMaximumIngredientsToDisplay(List<string> ingredients, int maxLength)
    {
        // Starting at end of list
        for (int i = ingredients.Count - 1; i >= 0; i--)
        {
            // If the length of the total string is smaller than the max return the index
            if (inventory.Dishes[i].Ingredients.Take(i).Count() < maxLength)
            {
                return i;
            }
        }
        return 0;
    }

    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem(Constants.MenuUI.SHOWMENU, AccountLevel.Guest));
        MenuItems.Add(new MenuItem(Constants.MenuUI.SORTMENU, AccountLevel.Guest));
        MenuItems.Add(new MenuItem(Constants.MenuUI.FILTERMENU, AccountLevel.Guest));
        MenuItems.Add(new MenuItem(Constants.MenuUI.ADD_DISH, AccountLevel.Admin));
        MenuItems.Add(new MenuItem(Constants.MenuUI.REMOVE_DISH, AccountLevel.Admin));
        MenuItems.Add(new MenuItem(Constants.MenuUI.UPDATE_DISH, AccountLevel.Admin));
        MenuItems.Add(new MenuItem(Constants.MenuUI.SHOW_PREORDERS, AccountLevel.Guest));
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case Constants.MenuUI.SHOWMENU:
                Console.WriteLine("Showing Menu");
                break;
            case Constants.MenuUI.SORTMENU:
                Console.WriteLine("Sorting Menu");
                break;
            case Constants.MenuUI.FILTERMENU:
                Console.WriteLine("Filtering Menu");
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
                Console.WriteLine("Showing Preorders");
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
    // Add Dish to the menu method
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
        string dish_type = Console.ReadLine();
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