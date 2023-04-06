using System.Text.Json;

public class PreOrder
{
    private static MenuController menu = new MenuController();
    private static InventoryController inventory = new InventoryController();
    private static PreOrderController preOrderController = new PreOrderController();
    private List<Dish> _dishes = new List<Dish>();
    // lijst aan pre order dishes die de gebruiker heeft gekozen
    private List<Dish> preOrders = new List<Dish>();
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
                        //toevoegen aan de pre order lijst
                        preOrders.Add(preOrderController.PreOrderDishes());
                                              
                        while (true)
                        {
                            Console.WriteLine("Do you want to pre order more? yes/no");
                            string morePreOrder = Console.ReadLine().ToLower();
                            if (morePreOrder == "yes")
                            {
                                //toevoegen aan de pre order lijst
                                preOrders.Add(preOrderController.PreOrderDishes());
                            }
                            else
                            {  
                                double price = 0;
                                foreach (Dish dish in preOrders)
                                {
                                    //laat naam van pre ordered dishes en prijzen zien
                                    Console.WriteLine(String.Format("{0,20} {1,6}", dish.Name, dish.Price));
                                    price += dish.Price;
                                }
                                //laat totale prijs zien door alle prijzen op te tellen van de gekozen dishes
                                Console.WriteLine($"Thank you for making a pre order. Your total amount will be: {price}");
                                //reset lijst
                                preOrders.Clear(); 
                                break;
                            }
                        }
                    }
                    else if (preOrderAnswer == "course menu")//menu needs to sort type (starter, main, desert)
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