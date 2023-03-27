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
    }

    public void Delete()
    {
        // ask for dish name
        // loop dishes and check if dish name match
            // _dishes.Remove();
    }

    public void Update()
    {
        // dishToChange.Name = newName;
        // dishToChange.Description = newDescription;
        // dishToChange.Price = newPrice;
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