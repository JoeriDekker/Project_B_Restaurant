using System.Text.Json;
public class Menu 
{
    private static MenuController menu = new MenuController();
    private static InventoryController inventory = new InventoryController();
    public void Start(){
          // it is still all here bc i have to make an MVC format for the menu
        
        
        while (true)
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1: Show Menu");
            Console.WriteLine("2: Sort Menu // still in progress");
            Console.WriteLine("3: Filter Menu // still in progress");
            Console.WriteLine("4: Add a Dish");
            Console.WriteLine("5: Remove a Dish");
            Console.WriteLine("6: Change a Dish");
            Console.WriteLine("7: Show PreOrders");
            Console.WriteLine("8: Exit");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowMenu();
                    break;
                case 2:
                    // Upcoming Sort function
                    ShowMenu();
                    break;
                case 3:
                    Console.WriteLine("On what type do you want to filter?");
                    string filter = Console.ReadLine() ?? "";
                    // Upcoming Filter function
                    ShowMenu();
                    break;
                case 4:
                    Console.WriteLine("What is the Dish name?");
                    string dish_name = Console.ReadLine() ?? "Unknown";
                    
                    Console.WriteLine("What is the Dish Description?");
                    string dish_description = Console.ReadLine() ?? "Unknown";
                   
                    Console.WriteLine("What is the Dish price?");
                    double dish_price = Convert.ToDouble(Console.ReadLine() ?? "0");

                    Console.WriteLine("What is the Dish Type?");
                    string dish_type = Console.ReadLine();
                    if (dish_type == null || dish_type == ""){
                        dish_type = "Unknown";
                    }
                    menu.Add(dish_name, dish_description, dish_price, dish_type);
                    break;
                case 5:
                    Console.WriteLine("Which Dish do you want to remove? (Give the name of the dish)");
                    string? remove_dish = Console.ReadLine();
                    menu.Delete(remove_dish);
                    break;
                case 6:
                    menu.Update();
                    break;
                case 7:
                    inventory.ShowPreOrders();
                    break;
                case 8:
                    Console.WriteLine("Goodbye!");
                    OpeningUI.Start();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
            Console.WriteLine();
        }    
    }

    public void ShowMenu(){
        foreach (Dish dish in menu.GetMenu())
        {
            Console.WriteLine($"- {dish.Name}: {dish.Type} (${dish.Price})");
            Console.WriteLine($" - {dish.Description}");
        }
    }
   
}