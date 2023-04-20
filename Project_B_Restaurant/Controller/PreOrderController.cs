using System.Text.Json;

public class PreOrderController
{
    private List<Dish> _dishes = new List<Dish>();
    

    public  PreOrderController(){
        _dishes = MenuAccess.LoadMenu();
    }
// void veranderen --> moet een dish returnen
    public Dish PreOrderDishes(){
        foreach (Dish dish in _dishes)
        {
            if (dish.PreOrderAmount < dish.MaxAmountPreOrder)
            {
                Console.WriteLine($"- {dish.Name}\n{string.Join(", ",dish.Ingredients)}\n{dish.Price}");
            }
        }
        Console.WriteLine("Please type here which dish you want to pre order:\n");
        string? preOrderDish = Console.ReadLine();
        foreach (Dish dish in _dishes)
        {
            if (dish.Name == preOrderDish)
            {
                dish.PreOrderAmount++;
                return dish;
            }
        }
        return null;
    }

    public void PreOrderCourseMenu()
    {
        
    }
}