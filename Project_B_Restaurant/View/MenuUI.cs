using System.Text;
using System.Text.Json;
public class MenuUI : UI
{
    private static MenuController Menu = new MenuController();

    private int _index;

    private bool _filter;

    private bool _detailedView;

    private string SwitchMenu { get => MenuController.IsFuture ? "Show Current Menu" : "Show Future Menu"; }


    public int Index
    {
        get => _index;
        // Clamps the value between a min and max.
        set => _index = Math.Clamp(value,
            0,
            // Upper limit is Length minus Step or 0. Whichever is highest.
            (Menu.Dishes.Count - Step) > 0 ? Menu.Dishes.Count - Step : 0);
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
        for (int i = Index; i < Index + Step && i < Menu.Dishes.Count; i++)
        {
            Dish dish = Menu.Dishes[i];
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

        for (int i = Index; i < Index + Step && i < Menu.Dishes.Count; i++)
        {
            Dish dish = Menu.Dishes[i];
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
        MenuItems.Add(new MenuItem("Add Dish", AccountLevel.Admin));
        MenuItems.Add(new MenuItem("Remove Dish", AccountLevel.Admin));
        MenuItems.Add(new MenuItem("Update Dish", AccountLevel.Admin));
        if (!MenuController.IsFuture)
        {
            MenuItems.Add(new MenuItem("Show Preorder", AccountLevel.Guest));
        }
        MenuItems.Add(new MenuItem(SwitchMenu, AccountLevel.Guest));
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
                Menu.Filter(toFilter.ToLower());
                break;
            case "Reset Filter":
                Menu.Reset();
                break;
            case "Add Dish":
                this.Add();
                Menu.Reset();
                break;
            case "Remove Dish":
                this.Delete();
                Menu.Reset();
                break;
            case "Update Dish":
                this.Update();
                break;
            case "Show Preorder":
                Menu.ShowPreOrders();
                break;
            case "Show Future Menu":
            case "Show Current Menu":
                MenuController.IsFuture = !MenuController.IsFuture;
                CreateMenuItems();
                break;
            case Constants.UI.EXIT:
            case Constants.UI.GO_BACK:
                this.Exit();
                break;
            default:
                Console.WriteLine("Invalid option");
                this.Start();
                break;
        }
    }

    private void Update()
    {
        string dishToChange;
        do
        {
            dishToChange = GetString("Which Dish do you want to Change? (Give the name of the dish)");

            if (!Menu.FindDishByName(dishToChange))
            {
                Console.WriteLine($"Dish with the name {dishToChange} has not been found!");
                dishToChange = string.Empty;
            }
        }
        while (dishToChange == string.Empty);

        Dish dish = Menu.GetDishByName(dishToChange);
        UpdateDishUI updateDishUI = new UpdateDishUI(this, dish);
        updateDishUI.Start();
        CreateMenuItems();
    }

    private void Sort()
    {
        Console.WriteLine("     1: Sort by ID");
        Console.WriteLine("     2: Sort by Name");
        Console.WriteLine("     3: Sort by Price");
        Console.WriteLine("     4: Sort by Type");
        Console.WriteLine("     0: Go Back");

        int choice = GetInt("Sort by?");
        if (choice == 0)
            return;

        Menu.SortBy(choice);
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

        Menu.Add(dish_name, dish_ingredients.Split(' ').ToList(), dish_allergies, dish_price, dish_type);


    }
    public void Delete()
    {
        string remove_dish = GetString("Which Dish do you want to remove? (Give the name of the dish)");
        if (Menu.Delete(remove_dish))
            Console.WriteLine($"{remove_dish} has been removed from the menu");
        else
            Console.WriteLine($"{remove_dish} has not been found");
    }
}