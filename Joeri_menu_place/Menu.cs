using System.Text.Json;
public class Menu : IChangeable
{
    private List<Dish> _dishes = new List<Dish>();

   
    public void LoadMenu()
    {
        using (StreamReader reader = new StreamReader("joeri_menu_place/menu.json"))
        {
            string json = reader.ReadToEnd();
            List<Dish> dishes = JsonSerializer.Deserialize<List<Dish>>(json);
            _dishes = dishes ?? new List<Dish>(); // assign the deserialized list to _dishes or an empty list if the deserialized value is null
        }
    }

    public void ShowMenu(){
        foreach (Dish dish in _dishes)
        {
            Console.WriteLine($"- {dish.Name}: {dish.Description} (${dish.Price})");
        }
    }


    public void Add()
    {
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
        Dish new_dish = new Dish(dish_name, dish_description, dish_price);
        _dishes.Add(new_dish);
        SaveMenu();
    }

    public void Delete()
{
    Console.WriteLine("Which Dish do you want to remove? (Give the name of the dish)");
    string remove_dish = Console.ReadLine();
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
        SaveMenu();
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
                    Console.WriteLine("4: Save Changes");
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
                         Console.WriteLine("What is the new dish description?");
                        double new_dish_price = Convert.ToDouble(Console.ReadLine());
                        dish.Price = new_dish_price;
                    }
                    else if (choosed_number == 4){
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
            SaveMenu();
            Console.WriteLine($"{change_dish} has been updated");
        }
    }

    public void SaveMenu()
    {
        string json = JsonSerializer.Serialize(_dishes);

        using (StreamWriter writer = new StreamWriter("joeri_menu_place/menu.json"))
        {
            writer.Write(json);
        }
    }
}