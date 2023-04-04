using System.Text.Json;
public class MenuController
{
    private List<Dish> _dishes = new List<Dish>();

    public MenuController(){
        _dishes = MenuAccess.LoadMenu();
    }

    public List<Dish> GetMenu(){
        return _dishes;
    }


    public void Add(string dish_name, string dish_description, double dish_price, string dish_type)
    {
        Dish new_dish = new Dish(dish_name, dish_description, dish_price, dish_type);
        _dishes.Add(new_dish);
        MenuAccess.SaveMenu(_dishes);
    }

    public void Delete(string remove_dish)
    {
        
        bool found_dish = false;
        List<Dish> dishesToRemove = new List<Dish>();
        foreach (Dish dish in _dishes)
        {
            if (dish.Name == remove_dish)
            {
                dishesToRemove.Add(dish);
                found_dish = true;
            }
        }
        if (!found_dish)
        {
            Console.WriteLine($"{remove_dish} has not been found");
        }
        else
        {
            foreach (Dish dish in dishesToRemove)
            {
                _dishes.Remove(dish);
            }
            MenuAccess.SaveMenu(_dishes);
            Console.WriteLine($"{remove_dish} has been removed from the menu");
        }
    }


    public void Update()
    {
        Console.WriteLine("Which Dish do you want to Change? (Give the name of the dish)");
        string? change_dish = Console.ReadLine();
        bool found_dish = false;
        foreach (Dish dish in _dishes){
            if (dish.Name == change_dish){
                while (true){
                    Console.WriteLine("What do you want to change?");
                    Console.WriteLine("1: Change Name");
                    Console.WriteLine("2: Change Description");
                    Console.WriteLine("3: Change Price");
                    Console.WriteLine("4: Change Type");
                    Console.WriteLine("5: Change Max amount of pre-order");
                    Console.WriteLine("6: Save Changes");
                    int choosed_number = Convert.ToInt32(Console.ReadLine());
                    if (choosed_number == 1){
                        Console.WriteLine("What is the new dish name?");
                        string? new_dish_name = Console.ReadLine();
                        dish.Name = new_dish_name;
                    }
                    else if (choosed_number == 2){
                        Console.WriteLine("What is the new dish description?");
                        string? new_dish_description = Console.ReadLine();
                        dish.Description = new_dish_description;
                    }
                    else if (choosed_number == 3){
                        Console.WriteLine("What is the new dish price?");
                        double new_dish_price = Convert.ToDouble(Console.ReadLine());
                        dish.Price = new_dish_price;
                    }
                    else if (choosed_number == 4){
                        Console.WriteLine("What is the new dish type?");
                        string? new_dish_type = Console.ReadLine();
                        dish.Type = new_dish_type;
                    }
                    else if (choosed_number == 5){
                        Console.WriteLine("What is the new Max amount of pre-order?");
                        int new_max_preoder = Convert.ToInt32(Console.ReadLine());
                        dish.MaxAmountPreOrder = new_max_preoder;
                    }
                    else if (choosed_number == 6){
                        MenuAccess.SaveMenu(_dishes);
                        break;
                    }
                    else{
                        Console.WriteLine("Input error! Please enter an number");
                    }
                }
            }
        }
        if (!found_dish){
            Console.WriteLine($"{change_dish} has not been found");
        }
        else{
            MenuAccess.SaveMenu(_dishes);
            Console.WriteLine($"{change_dish} has been updated");
        }
    }
}