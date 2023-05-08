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

    
}