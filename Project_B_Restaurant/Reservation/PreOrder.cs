using System.Text.Json;

public class PreOrder
{
    private static MenuController menu = new MenuController();
    private static InventoryController inventory = new InventoryController();
    private static PreOrderController preOrderController = new PreOrderController();
    // lijst aan pre order dishes die de gebruiker heeft gekozen
    private List<Dish> preOrders = new List<Dish>();
    public void Start()
    {
        while (true)
        {
            Console.WriteLine("\nThere's an option to make a pre order, so the dish will definitely be available when you arrive.\nWould you like to choose this option?");
            Console.WriteLine("\nyes");
            Console.WriteLine("no\n");
            string? answer = Console.ReadLine();
            if (answer == "yes")
            {
                while (true)
                {
                    Console.WriteLine("\nWhat would you like to pre order?\n");
                    Console.WriteLine("dishes");
                    Console.WriteLine("course menu");
                    Console.WriteLine("go back\n");
                    string? preOrderAnswer = Console.ReadLine();
                    if (preOrderAnswer == "dishes")
                    {
                        //toevoegen aan de pre order lijst
                        preOrders.Add(preOrderController.PreOrderDishes());
                                              
                        while (true)
                        {
                            Console.WriteLine("\nDo you want to pre order more? yes/no");
                            Console.WriteLine("yes\nno\n");
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
                                price = Math.Round(price, 2);
                                Console.WriteLine($"\nThank you for making a pre order. Your total amount will be: {price}");
                                //reset lijst
                                preOrders.Clear();
                                break;
                            }
                        }
                    }
                    else if (preOrderAnswer == "course menu")//menu needs to sort type (starter, main, desert)
                    {
                        preOrders.Add(preOrderController.PreOrderAppetizer());
                        preOrders.Add(preOrderController.PreOrderMain());
                        preOrders.Add(preOrderController.preOrderDessert());

                        while (true)
                        {
                            Console.WriteLine("Do you want to pre order more? yes/no");
                            string morePreOrder = Console.ReadLine().ToLower();
                            if (morePreOrder == "yes")
                            {
                                //toevoegen aan de pre order lijst
                                preOrders.Add(preOrderController.PreOrderAppetizer());
                                preOrders.Add(preOrderController.PreOrderMain());
                                preOrders.Add(preOrderController.preOrderDessert());
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
                                price = Math.Round(price, 2);
                                Console.WriteLine($"Thank you for making a pre order. Your total amount will be: {price}");
                                //reset lijst
                                preOrders.Clear();
                                break;
                            }
                        }
                    }
                    else if (preOrderAnswer == "go back")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This is not available.");
                    }
                    break;
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
            break;
        }   
    }
}