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
            Console.WriteLine("2: Add a Dish");
            Console.WriteLine("3: Remove a Dish");
            Console.WriteLine("4: Change a Dish");
            Console.WriteLine("5: Show pre orders");
            Console.WriteLine("6: Exit");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowMenu();
                    break;
                case 2:
                    Console.WriteLine("What is the Dish name?");
                    string dish_name = Console.ReadLine();
                    if (dish_name == null || dish_name == ""){
                        dish_name = "Unknown";
                    }
                    Console.WriteLine("What is the Dish Description?");
                    string dish_description = Console.ReadLine();
                    if (dish_description == null || dish_description == ""){
                        dish_description = "Unknown";
                    }
                    Console.WriteLine("What is the Dish price?");
                    double dish_price = Convert.ToDouble(Console.ReadLine());
                    if (dish_price == null || dish_price == 0){
                        dish_price = 0.0;
                    }
                    Console.WriteLine("What is the Dish Type?");
                    string dish_type = Console.ReadLine();
                    if (dish_type == null || dish_type == ""){
                        dish_type = "Unknown";
                    }
                    menu.Add(dish_name, dish_description, dish_price, dish_type);
                    break;
                case 3:
                    Console.WriteLine("Which Dish do you want to remove? (Give the name of the dish)");
                    string? remove_dish = Console.ReadLine();
                    menu.Delete(remove_dish);
                    break;
                case 4:
                    menu.Update();
                    break;
                case 5:
                    Console.WriteLine("Here is a list of all the pre orders that were made");
                    inventory.ShowPreOrders();
                    break;
                case 6:
                    Console.WriteLine("Goodbye!");
                    UI.Start();
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