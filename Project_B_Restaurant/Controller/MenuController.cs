using System.Text.Json;
public class MenuController
{
    private List<Dish> _dishes = new List<Dish>();
    private List<Dish> _future_dishes = new List<Dish>();

    public MenuController()
    {
        _dishes = MenuAccess.LoadMenu();
        _future_dishes = MenuAccess.LoadFutureMenu();
    }

    public void Add(string dish_name, List<string> dish_ingredients, string dish_allergies, double dish_price, string dish_type, bool isFutureMenu)
    {
        Dish new_dish = new Dish(dish_name, dish_ingredients, dish_allergies, dish_price, dish_type);
        
        if (isFutureMenu){
            _future_dishes.Add(new_dish);
            MenuAccess.SaveFutureMenu(_future_dishes);
        }
        else{
            _dishes.Add(new_dish);
            MenuAccess.SaveMenu(_dishes);
        }
    }

    public void Delete(string remove_dish, bool isFutureMenu)
    {
        if (isFutureMenu){
            bool found_dish = false;
            List<Dish> dishesToRemove = new List<Dish>();
            foreach (Dish dish in _future_dishes)
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
                    _future_dishes.Remove(dish);
                }
                
                MenuAccess.SaveFutureMenu(_future_dishes);
                
                Console.WriteLine($"{remove_dish} has been removed from the menu");
            }
        }

        else{
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
                
                MenuAccess.SaveMenu(_dishes);
                
                Console.WriteLine($"{remove_dish} has been removed from the menu");
            }
        }
        
    }

    public bool FindDishByName(string dish_name, bool isFutureMenu){
        if (isFutureMenu){
            foreach (Dish dish in _future_dishes){
                if (dish.Name == dish_name){
                    return true;
                }
            }
        }
        else{
            foreach (Dish dish in _dishes){
                if (dish.Name == dish_name){
                    return true;
                }
            }
        }
        
        return false;
    }

    public Dish GetDishByName(string dish_name, bool isFutureMenu){
        if (isFutureMenu){
            foreach (Dish dish in _future_dishes){
                if (dish.Name == dish_name){
                    return dish;
                }
            }
        }
        else{
            foreach (Dish dish in _dishes){
                if (dish.Name == dish_name){
                    return dish;
                }
            }
        }
        
        return null;
    }

    public void Update(Dish dish_item, string dish_name, bool isFutureMenu)
    {   
        if (isFutureMenu){
            foreach (Dish dish in _future_dishes)
            {
                Console.WriteLine("-");
                Console.WriteLine(dish_item.Name + "-" + dish_item.Name);
                Console.WriteLine(dish.Name);
                Console.WriteLine("-");
                if (dish_name == dish.Name){
                    dish.Name = dish_item.Name;
                    dish.Ingredients = dish_item.Ingredients;
                    dish.Allergies = dish_item.Allergies;
                    dish.Price = dish_item.Price;
                    dish.Type = dish_item.Type;
                    dish.InStock = dish_item.InStock;
                    dish.PreOrderAmount = dish_item.PreOrderAmount;
                    dish.MaxAmountPreOrder = dish_item.MaxAmountPreOrder;
                }
            }
           
            MenuAccess.SaveFutureMenu(_future_dishes);
           
        }
        else{
            foreach (Dish dish in _dishes)
            {
                if (dish_name == dish.Name){
                    dish.Name = dish_item.Name;
                    dish.Ingredients = dish_item.Ingredients;
                    dish.Allergies = dish_item.Allergies;
                    dish.Price = dish_item.Price;
                    dish.Type = dish_item.Type;
                    dish.InStock = dish_item.InStock;
                    dish.PreOrderAmount = dish_item.PreOrderAmount;
                    dish.MaxAmountPreOrder = dish_item.MaxAmountPreOrder;
                }
            }
            MenuAccess.SaveMenu(_dishes);
        
        }
    }   
}