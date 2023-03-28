using System.Text.Json;

public class Inventory 
{
    private List<Dish> _dishes = new List<Dish>();
    public void LoadMenu()
        {
            using (StreamReader reader = new StreamReader("reservation/inventory.json"))
            {
                string json = reader.ReadToEnd();
                List<Dish>dishes = JsonSerializer.Deserialize<List<Dish>>(json);
                _dishes = dishes ?? new List<Dish>(); 
            }
        }

    public void Showinventory(){
            foreach (Dish dish in _dishes)
            {
                Console.WriteLine($"- {dish.Name}: {dish.Ingredients}");
            }
        }
}