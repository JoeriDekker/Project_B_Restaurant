using System.Text.Json;

public class InventoryController
{
    private List<Dish> _dishes = new List<Dish>();

    public List<Dish> Dishes {
        get => _dishes;
        set => _dishes = value;
    }
    public InventoryController(){
        Dishes = MenuAccess.LoadMenu();
    }
    
    public void ShowPreOrders()
    {
       foreach (Dish dish in _dishes)
       {
            Console.WriteLine($"Dish: {dish.Name}\nPre Order Amount: {dish.PreOrderAmount}");
       }
    }
}