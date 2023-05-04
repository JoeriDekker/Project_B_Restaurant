using System.Text.Json;

public class InventoryController
{

    private bool _filter;

    public List<Dish> Dishes = new List<Dish>();
    public InventoryController()
    {
        Dishes = MenuAccess.Dishes;
    }

     public InventoryController(bool isFuture)
    {
        if (isFuture){
            Dishes = MenuAccess.LoadFutureMenu();
        }
        else{
            Dishes = MenuAccess.Dishes;
        }
        
    }

    public void ShowPreOrders()
    {
        foreach (Dish dish in MenuAccess.Dishes)
        {
           if (dish.PreOrderAmount > 0) Console.WriteLine($"Dish: {dish.Name}\nPre Order Amount: {dish.PreOrderAmount}");
        }
    }

    public void Filter(string query)
    {
        Dishes = Dishes.FindAll(d => d.ToString().ToLower().Contains(query));
    }

    public void Reset()
    {
        Dishes.Clear();
        MenuAccess.LoadMenu();
    }

    public void Reset(bool isFuture)
    {
        Dishes.Clear();
         if (isFuture){
            Dishes = MenuAccess.LoadFutureMenu();
        }
        else{
            MenuAccess.LoadMenu();
        }
    }

    public void SortBy(int type)
    {
        switch (type)
        {
            case 1:
                Dishes = Dishes.OrderBy(d => d.ID).ToList();
                break;
            case 2:
                Dishes = Dishes.OrderBy(d => d.Name).ToList();
                break;
            case 3:
                Dishes = Dishes.OrderBy(d => d.Price).ToList();
                break;
            case 4:
                Dishes = Dishes.OrderBy(d => d.Type).ToList();
                break;
            default:
                break;
        }
    }
}