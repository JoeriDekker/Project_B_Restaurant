using System.Text.Json;

public class InventoryController
{
    private List<Dish> _dishes = new List<Dish>();

    public InventoryController(){
        _dishes = MenuAccess.LoadMenu();
    }
    
    public void ShowPreOrders()
    {
       foreach (Dish dish in _dishes)
       {
            Console.WriteLine($"Dish: {dish.Name}\nPre Order Amount: {dish.PreOrderAmount}\n");
       }
    }
}