using System.Text.Json;

public class PreOrderController
{

// void veranderen --> moet een dish returnen
    public Dish PreOrderDishes()
    {
        Console.WriteLine("\nThe menu will be showed after you pressed the enter button.");
        Console.ReadLine();
        foreach (Dish dish in MenuAccess.Dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder)
            {
                Console.WriteLine($"- {dish.Name}\n{string.Join(", ",dish.Ingredients)}\n{dish.Price}");
            }
        }
        Console.WriteLine("Please type here which dish you want to pre order:\n");
        string? preOrderDish = Console.ReadLine();
        foreach (Dish dish in MenuAccess.Dishes)
        {
            if (dish.Name == preOrderDish)
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
        foreach (Dish dish in MenuAccess.Dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder && dish.Type == "Appetizer")
            {
                Console.WriteLine($"- {dish.Name}\n{string.Join(", ",dish.Ingredients)}\n{dish.Price}");
            }
        }
        Console.WriteLine("Please type here which appetizer you want to pre order:\n");
        string? preOrderAppetizer = Console.ReadLine();
        foreach (Dish dish in MenuAccess.Dishes)
        {
            if (dish.Name == preOrderAppetizer)
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
        foreach (Dish dish in MenuAccess.Dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder && dish.Type == "Main")
            {
                Console.WriteLine($"- {dish.Name}\n{string.Join(", ",dish.Ingredients)}\n{dish.Price}");
            }
        }
        Console.WriteLine("\nPlease type here which main you want to pre order:");
        string? preOrderMain = Console.ReadLine();
        foreach (Dish dish in MenuAccess.Dishes)
        {
            if (dish.Name == preOrderMain)
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
        foreach (Dish dish in MenuAccess.Dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder && dish.Type == "Dessert")
            {
                Console.WriteLine($"- {dish.Name}\n{string.Join(", ",dish.Ingredients)}\n{dish.Price}");
            } 
        }
        Console.WriteLine("Please type here which dessert you want to pre order:\n");
        string? preOrderDessert = Console.ReadLine();
        foreach (Dish dish in MenuAccess.Dishes)
        {
            if (dish.Name == preOrderDessert)
            {
                dish.PreOrderAmount++;
                return dish;
            }
        }
    return null;
    }
}