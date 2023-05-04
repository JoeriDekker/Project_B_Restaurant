class UpdateDishUI : UI
{
    private Dish _dish;

    private bool _futureMenu;

    private static MenuController menu = new MenuController();
    private static MenuController future_menu = new MenuController();

    // Lets put this in the Header of the UI
    public override string Header => "Update Dish:";

    public override string SubText
    {
        get =>
@$"================================================================
Name: {_dish.Name}
Ingredients: {string.Join(", ", _dish.Ingredients)}
Allergies: {_dish.Allergies}
Price: {_dish.Price}
Type: {_dish.Type}
Max amount of pre-order: {_dish.MaxAmountPreOrder}
================================================================";
    }
    private string[] options = new string[]
    {
        "Change Name",
        "Change Ingredients",
        "Change Allergies",
        "Change Price",
        "Change Type",
        "Change Max amount of pre-order",
        "Save Changes",
    };

    public UpdateDishUI(UI previousUI, Dish dish, bool future_menu) : base(previousUI)
    {
        _dish = dish;
        _futureMenu = future_menu;
    }

    public override void CreateMenuItems()
    {
        for (int i = 0; i < options.Length; i++)
        {
            Add(new MenuItem(options[i]));
        }
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Change Name":
                Console.WriteLine("What is the new dish name?");
                string? new_dish_name = Console.ReadLine();
                _dish.Name = new_dish_name;
                break;

            case "Change Ingredients":
                Console.WriteLine("What are the updated ingredients?");
                string? new_dish_ingredients = Console.ReadLine();
                _dish.Ingredients = new_dish_ingredients.Split(',').ToList();
                break;

            case "Change Allergies":
                Console.WriteLine("What are the updated allergies?");
                string? new_dish_allergies = Console.ReadLine();
                _dish.Allergies = new_dish_allergies;
                break;

            case "Change Price":
                Console.WriteLine("What is the new dish price?");
                double new_dish_price = Convert.ToDouble(Console.ReadLine());
                _dish.Price = new_dish_price;
                break;

            case "Change Type":
                Console.WriteLine("What is the new dish type?");
                string? new_dish_type = Console.ReadLine();
                _dish.Type = new_dish_type;
                break;

            case "Change Max amount of pre-order":
                Console.WriteLine("What is the new max amount of pre-order?");
                int new_dish_max_amount_pre_order = Convert.ToInt32(Console.ReadLine());
                _dish.MaxAmountPreOrder = new_dish_max_amount_pre_order;
                break;
            
            case "Save Changes":
                if (_futureMenu){
                    future_menu.Update(_dish, _futureMenu);
                }
                else{
                    menu.Update(_dish, _futureMenu);
                }
                
                
                break;

            case Constants.UI.EXIT:
            case Constants.UI.GO_BACK:
                Exit();
                break;
            default:
                Console.WriteLine("Invalid option");
                Start();
                break;
        }
    }
}