using System.Text.Json;

public class PreOrderController
{
    private List<Dish> preOrders = new List<Dish>();
    public MenuController Menu = new();
    public Dish PreOrderDishes()
    {
        Console.WriteLine("\nThe menu will be showed after you pressed the enter button.");
        Console.ReadLine();
        Menu.GetEverythingExeptDrinks();
        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder)
            {
                Console.WriteLine($"- {dish.ID}: {dish.Name}\n{string.Join(", ",dish.Ingredients)}\n{dish.Price}");
            }
        }
        Console.WriteLine("\nPlease type here the dish ID you want to pre order:\n");
        int? preOrderID = Convert.ToInt32(Console.ReadLine());
        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.ID == preOrderID)
            {
                dish.PreOrderAmount++;
                return dish;
            }
        }
        return null;
    }

    public Dish PreOrderAppetizer()
    {
        //pre order appetizer
        Console.WriteLine("What appetizer do you want to order?");
        Console.WriteLine("The appetizer menu will be showed after you pressed enter.");
        Console.ReadLine();
        Menu.GetEverythingExeptDrinks();
        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder && dish.Type == "Appetizer")
            {
                Console.WriteLine($"- {dish.ID}: {dish.Name}\n{string.Join(", ",dish.Ingredients)}\n{dish.Price}");
            }
        }
        Console.WriteLine("Please type here which appetizer you want to pre order:\n");
        int? preOrderAppetizer = Convert.ToInt32(Console.ReadLine());
        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.ID == preOrderAppetizer)
            {
                dish.PreOrderAmount++;
                return dish;
            }
        }
        return null;
    }

    public Dish PreOrderMain()
    {
        //pre order main
        Console.WriteLine("\nWhat main dish do you want to order?");
        Console.WriteLine("The main menu will be showed after you pressed enter.");
        Console.ReadLine();
        Menu.GetEverythingExeptDrinks();
        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder && dish.Type == "Main")
            {
                Console.WriteLine($"- {dish.ID}: {dish.Name}\n{string.Join(", ",dish.Ingredients)}\n{dish.Price}");
            }
        }
        Console.WriteLine("\nPlease type here which main you want to pre order:");
        int? preOrderMain = Convert.ToInt32(Console.ReadLine());
        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.ID == preOrderMain)
            {
                dish.PreOrderAmount++;
                return dish;
            }
        }
        return null;
    }

    public Dish preOrderDessert()
    {
        //pre order dessert
        Console.WriteLine("\nWhat dessert do you want to order?");
        Console.WriteLine("The dessert menu will be showed after you pressed enter.");
        Console.ReadLine();
        Menu.GetEverythingExeptDrinks();
        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder && dish.Type == "Dessert")
            {
                Console.WriteLine($"- {dish.ID}: {dish.Name}\n{string.Join(", ",dish.Ingredients)}\n{dish.Price}");
            } 
        }
        Console.WriteLine("Please type here which dessert you want to pre order:\n");
        int? preOrderDessert = Convert.ToInt32(Console.ReadLine());
        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.ID == preOrderDessert)
            {
                dish.PreOrderAmount++;
                return dish;
            }
        }
    return null;
    }
}