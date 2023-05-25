using System.Text.Json;
public class MenuController
{
    private List<Dish> _dishes = new List<Dish>();
    private List<Dish> _future_dishes = new List<Dish>();

    public List<Dish> Dishes
    {
        get => IsFuture ? _future_dishes : _dishes;
        set 
        {
            if (IsFuture)
                _future_dishes = value;
            
            else
                _dishes = value;
        }
    }

    public static bool IsAlchoholeDrink { get; set; } 

    public static bool IsFuture { get; set; }

    public MenuController()
    {
        _dishes = MenuAccess.LoadMenu();
        _future_dishes = MenuAccess.LoadFutureMenu();
    }

    public void Add(string dish_name, List<string> dish_ingredients, string dish_allergies, double dish_price, string dish_type)
    {
        Dish new_dish = new Dish(dish_name, dish_ingredients, dish_allergies, dish_price, dish_type);
        Dishes.Add(new_dish);
        this.Save();
    }
    public void Load()
    {
        if (IsFuture)
        {
            Dishes = MenuAccess.LoadFutureMenu();
        }
        else
        {
            Dishes = MenuAccess.LoadMenu();
        }
    }

    public void Save()
    {
        if (IsFuture)
        {
            MenuAccess.SaveFutureMenu(Dishes);
        }
        else
        {
            MenuAccess.SaveMenu(Dishes);
        }
    }

    public void Reset()
    {
        Dishes.Clear();
        this.Load();
    }


    public bool Delete(int DishID)
    {
        Dish dishToRemove = new();

        if (FindDishWithID(DishID)){
            dishToRemove = Dishes[GetDishIndexWithID(DishID)];
        }
        else{
            return false;
        }
        Dishes.Remove(dishToRemove!);
        this.Save();
        return true;
    }

    public bool FindDishByName(string dish_name)
    {
        foreach (Dish dish in Dishes)
        {
            if (dish.Name == dish_name)
            {
                return true;
            }
        }
        return false;
    }

    public Dish GetDishByName(string dish_name)
    {
        foreach (Dish dish in Dishes)
        {
            if (dish.Name == dish_name)
            {
                return dish;
            }
        }
        return null;
    }

    public void Update(Dish dish_item, string dish_name)
    {
        foreach (Dish dish in Dishes)
        {
            if (dish_name == dish.Name)
            {
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

        this.Save();
    }
    public void ShowPreOrders()
    {
        foreach (Dish dish in MenuAccess.Dishes)
        {
            if (dish.PreOrderAmount > 0)
                Console.WriteLine($"Dish: {dish.Name}\nPre Order Amount: {dish.PreOrderAmount}");
        }
    }

    public void Search(string query)
    {
        Dishes = Dishes.FindAll(d => d.ToString().ToLower().Contains(query));
    }

    public void Filter(string query, bool isAlergies)
    {
        List<Dish> filteredItems = new List<Dish>();

        foreach (Dish dish in Dishes)
        {
            if (isAlergies){
                if (!dish.Allergies.Contains(query)){
                    filteredItems.Add(dish);
                }
            }
            else{
                if(!dish.Type.Contains(query)){
                    filteredItems.Add(dish);
                }
            }
            
        }
        Dishes = filteredItems;
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

    public bool FindDishWithID(int value)
    {
        return FindDishWithID(value, 0, Dishes.Count - 1);
    }

    private bool FindDishWithID(int value, int first, int last)
    {
        if (first > last)
        {
            return false;
        }

        int mid = (first + last) / 2;

        if (Dishes[mid].ID == value)
        {
            return true;
        }
        else if (Dishes[mid].ID > value)
        {
            return FindDishWithID(value, first, mid - 1);
        }
        else
        {
            return FindDishWithID(value, mid + 1, last);
        }
    }

    public int GetDishIndexWithID(int value)
    {
        return GetDishIndexWithID(value, 0, Dishes.Count - 1);
    }

    private int GetDishIndexWithID(int value, int first, int last)
    {
        if (first > last)
        {
            return -1;
        }

        int mid = (first + last) / 2;

        if (Dishes[mid].ID == value)
        {
            return mid;
        }
        else if (Dishes[mid].ID > value)
        {
            return GetDishIndexWithID(value, first, mid - 1);
        }
        else
        {
            return GetDishIndexWithID(value, mid + 1, last);
        }
    }
}