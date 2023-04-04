using System.Text.Json;

public class PreOrder
{
    private static MenuController menu = new MenuController();
    private static InventoryController inventory = new InventoryController();
    private static PreOrderController preOrder = new PreOrderController();
    private List<Dish> _dishes = new List<Dish>();
    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Do you want to pre order?");
            Console.WriteLine("yes");
            Console.WriteLine("no");
            string? answer = Console.ReadLine();
            if (answer == "yes")
            {
                while (true)
                {
                    Console.WriteLine("What would you like to pre order?");
                    Console.WriteLine("dishes");
                    Console.WriteLine("course menu");
                    Console.WriteLine("go back");
                    string? preOrderAnswer = Console.ReadLine();
                    if (preOrderAnswer == "dishes")
                    {
                        preOrder.PreOrderDishes();
                        
                        while (true)
                        {
                            Console.WriteLine("Do you want to pre order more? yes/no");
                            string morePreOrder = Console.ReadLine().ToLower();
                            if (morePreOrder == "yes")
                            {
                                preOrder.PreOrderDishes();
                            }
                            else
                            {
                                Console.WriteLine("Thank you for making a pre order. Your total amount will be: "); //need to add prices/total amount.
                            }
                        }
                    }
                    else if (preOrderAnswer == "course menu")//menu needs to sort type (starter, head, desert)
                    {

                    }
                    else if (preOrderAnswer == "go back")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This is not available.");
                    }
                }
            }
            else if (answer == "no")
            {
            break;
            }
            else
            {
            Console.WriteLine("This is not available.");
            }
        }   
    }
}