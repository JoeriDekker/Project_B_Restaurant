using System.Text;

class PreOrderView : UI
{
    private ReservationLogic ReservationController = new();

    public MenuController Menu = new();

    public ReservationModel? Reservation { get; set; }

    private double totalPrice;

    public override string Header
    {
        get => @"
     _____             ____          _           
    |  __ \           / __ \        | |          
    | |__) | __ ___  | |  | |_ __ __| | ___ _ __ 
    |  ___/ '__/ _ \ | |  | | '__/ _` |/ _ \ '__|
    | |   | | |  __/ | |__| | | | (_| |  __/ |   
    |_|   |_|  \___|  \____/|_|  \__,_|\___|_|   
    =============================================";
    }

    public override string SubText
    {
        get
        {
            if (Reservation!.PreOrders!.Count <= 0)
                return $"Reservation Code: {Reservation.R_Code}";
            else
            {
                StringBuilder sb = new();
                sb.Append($"Reservation Code: {Reservation.R_Code}\n");
                totalPrice = 0;
                for (int i = 0; i < Reservation.PreOrders.Count; i++)
                {
                    Dish dish = Reservation.PreOrders[i];
                    totalPrice += dish.Price;
                    sb.AppendLine("==================================");
                    sb.AppendLine($"ID: {dish.ID}\nName: {dish.Name}\nPrice: {dish.Price} euro");
                }
                sb.AppendLine("==================================");
                sb.AppendLine($"\nYour total price is: €{Math.Round(totalPrice, 2)}");
                sb.AppendLine("Preorders will be saved on exit.");
                return sb.ToString();
            }
        }
    }

    

    public PreOrderView(UI previousUI, ReservationModel reservation) : base(previousUI)
    {
        Reservation = reservation;
    }
    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem("Add Single Dish", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Add Full Course", AccountLevel.Guest));

        if (Reservation != null && Reservation.PreOrders!.Count > 0)
            MenuItems.Add(new MenuItem("Remove Dish From Pre-Order", AccountLevel.Guest));
    }
    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Add Single Dish":
                AddSingleDish();
                break;
            case "Add Full Course":
                AddCourseMenus();
                break;
            case "Remove Dish From Pre-Order":
                RemoveDish();
                break;
            case Constants.UI.GO_BACK:
            case Constants.UI.EXIT:
                ReservationController.Update(Reservation!);
                Exit();
                break;
            default:
                Console.WriteLine("Invalid input");
                break; ;
        }
    }

    public void AddSingleDish()
    {
        ShowDishes();
        int appetizer_id = GetInt("Please type here the dish ID you want to pre order:");
        if (!Menu.FindDishWithID(appetizer_id))
        {
            AddSingleDish();
        }
        int dish_index = Menu.GetDishIndexWithID(appetizer_id);
        Dish chosen_dish = new();
        chosen_dish = Menu.Dishes[dish_index];
        if (chosen_dish == null || chosen_dish.Type == "Drink")
        {
            Console.WriteLine("This Dish is not available\nPress Enter to continue");
            Console.ReadLine();
            AddSingleDish();
        }
        else if (chosen_dish.MaxAmountPreOrder <= chosen_dish.PreOrderAmount)
        {
            Console.WriteLine("This dish is not in stock right now\nPlease select another dish\nPress Enter to continue");
            Console.ReadLine();
            AddSingleDish();
        }
        else
        {
            Menu.Dishes[dish_index].PreOrderAmount += 1;
            Menu.Save();
            Reservation!.PreOrders!.Add(chosen_dish);
        }
    }

    public void AddCourseMenus()
    {
        AddDishToCM("Appetizer");
        AddDishToCM("Main");
        AddDishToCM("Dessert");
        Menu.Save();
    }

    public void RemoveDish()
    {
        int ID = GetInt("ID of Dish to remove?");
        Dish? dish = Reservation!.PreOrders!.Find(d => d.ID == ID);
        if (dish != null)
        {
            Reservation.PreOrders.Remove(dish);
            Menu.RemovePreOderInDish(dish);
        }
        else
            Console.WriteLine("Dish not found in Pre-Order");
    }


    public void AddDishToCM(string course)
    {
        Console.Clear();
        ShowDishes(course);
        int appetizer_id = GetInt("Please type here the dish ID you want to pre order:");
        if (!Menu.FindDishWithID(appetizer_id))
        {
            AddDishToCM(course);
        }
        int dish_index = Menu.GetDishIndexWithID(appetizer_id);
        Dish dish = new();
        dish = Menu.Dishes[dish_index];
        if (dish == null || dish.Type != course || dish.Type == "Drink")
        {
            Console.WriteLine($"This Dish is not a(n) {course}\nPress Enter to continue");
            Console.ReadLine();
            AddDishToCM(course);
        }
        else if (dish.MaxAmountPreOrder <= dish.PreOrderAmount)
        {
            Console.WriteLine("This dish is not in stock right now\nPlease select another dish\nPress Enter to continue");
            Console.ReadLine();
            AddDishToCM(course);
        }
        else
        {
            Menu.Dishes[dish_index].PreOrderAmount += 1;
            Reservation!.PreOrders!.Add(dish);

        }
    }

    public ReservationModel EndPreOrder()
    {
        return this.Reservation!;
    }
    public static void Change()
    {
        Console.WriteLine($"What info would you like to change?");
    }

    public void ShowDishes()
    {
        StringBuilder sb = new();
        string top = @"
      __  __                  
     |  \/  |                 
     | \  / | ___ _ __  _   _ 
     | |\/| |/ _ \ '_ \| | | |
     | |  | |  __/ | | | |_| |
     |_|  |_|\___|_| |_|\__,_|
    ==========================";
        string header = String.Format("{0,-11}| {1,-30}| {2,-48}| {3,-25}| {4,-16}| {5,-17}|",
                                    "\x1b[1mID\x1b[0m", "\x1b[1mName\x1b[0m", "\x1b[1mIngredients\x1b[0m", "\x1b[1mAllergies\x1b[0m", "\x1b[1mPrice\x1b[0m", "\x1b[1mType\x1b[0m");
        string divider = new('=', header.Length - 8 * 6);
        sb.AppendLine(top);
        sb.AppendLine(header);
        sb.AppendLine(divider);

        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder && dish.PreOrderAmount < dish.MaxAmountPreOrder && dish.Type != "Drink")
            {
                string ingredients = GetMaxItemsToPrint(dish.Ingredients, 36);
                string row = $"{dish.ID,3}| {dish.Name,22}| {ingredients,40}| {dish.Allergies,17}|  €{dish.Price,-7}| {dish.Type,9}|";
                sb.AppendLine(row);
            }
        }

        Console.WriteLine(sb.ToString());
    }

    public void ShowDishes(string type)
    {
        StringBuilder sb = new();
        string top = @"
      __  __                  
     |  \/  |                 
     | \  / | ___ _ __  _   _ 
     | |\/| |/ _ \ '_ \| | | |
     | |  | |  __/ | | | |_| |
     |_|  |_|\___|_| |_|\__,_|
    ==========================";
        string header = String.Format("{0,-11}| {1,-30}| {2,-48}| {3,-25}| {4,-16}| {5,-17}|",
                                    "\x1b[1mID\x1b[0m", "\x1b[1mName\x1b[0m", "\x1b[1mIngredients\x1b[0m", "\x1b[1mAllergies\x1b[0m", "\x1b[1mPrice\x1b[0m", "\x1b[1mType\x1b[0m");
        string divider = new('=', header.Length - 8 * 6);
        sb.AppendLine(top);
        sb.AppendLine(header);
        sb.AppendLine(divider);

        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder && dish.Type == type && dish.PreOrderAmount < dish.MaxAmountPreOrder)
            {
                string ingredients = GetMaxItemsToPrint(dish.Ingredients, 36);
                string row = $"{dish.ID,3}| {dish.Name,22}| {ingredients,40}| {dish.Allergies,17}|  €{dish.Price,-7}| {dish.Type,9}|";
                sb.AppendLine(row);
            }
        }

        Console.WriteLine(sb.ToString());
    }

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
}