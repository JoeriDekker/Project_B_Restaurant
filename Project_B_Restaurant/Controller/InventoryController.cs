using System.Text.Json;

public class InventoryController
{
    private List<Dish> _dishes = new List<Dish>();

    private bool _filter;

    public List<Dish> Dishes
    {
        get => _dishes;
        set => _dishes = value;
    }
    public InventoryController()
    {
        Dishes = MenuAccess.LoadMenu();
    }

    public void ShowPreOrders()
    {
        foreach (Dish dish in _dishes)
        {
            Console.WriteLine($"Dish: {dish.Name}\nPre Order Amount: {dish.PreOrderAmount}");
        }
    }

    public void Filter(string query)
    {
        Dishes = Dishes.FindAll(d => d.ToString().ToLower().Contains(query));
    }

    public void Reset()
    {
        Dishes.Clear();
        Dishes = MenuAccess.LoadMenu();
    }
}