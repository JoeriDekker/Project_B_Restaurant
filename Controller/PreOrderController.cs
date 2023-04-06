using System.Text.Json;

public class PreOrderController
{
    private List<Dish> _dishes = new List<Dish>();

    public  PreOrderController(){
        _dishes = MenuAccess.LoadMenu();
    }

    public void PreOrderDishes(){
        foreach (Dish dish in _dishes)
        {
            Console.WriteLine($"- {dish.Name}\n{dish.Ingredients}\n{dish.Price}");
        }
        Console.WriteLine("Please type here which dish you want to pre order:\n");
        string? preOrderDish = Console.ReadLine();
        foreach (Dish dish in _dishes)
        {
            if (dish.Name == preOrderDish)
            {
                dish.PreOrderAmount++;
            }
        }
    }

    public void GetPrice(){
        foreach (Dish dish in _dishes)
        {
            
        }
    }
}