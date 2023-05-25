class UpdateDishUI : UI
{
    private Dish _dish;
    private string _dish_name;

    private static MenuController Menu = new MenuController();

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
        "Save Changes\n(Changes Won't be automatically saved for future uses)",
    };

    public UpdateDishUI(UI previousUI, Dish dish) : base(previousUI)
    {
        _dish = dish;
        _dish_name = dish.Name;
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
                string? new_dish_name = GetString("What is the new dish name?");
                _dish.Name = new_dish_name;
                break;

            case "Change Ingredients":
                string? new_dish_ingredients = GetString("What are the updated ingredients?");
                _dish.Ingredients = new_dish_ingredients.Split(',').ToList();
                break;

            case "Change Allergies":
                string? new_dish_allergies = GetString("What are the updated allergies?");
                _dish.Allergies = new_dish_allergies;
                break;

            case "Change Price":
                double new_dish_price = GetDouble("What is the new dish price?");
                _dish.Price = new_dish_price;
                break;

            case "Change Type":
                string? new_dish_type = GetString("What is the new dish type?");
                _dish.Type = new_dish_type;
                break;

            case "Change Max amount of pre-order":
                int new_dish_max_amount_pre_order = GetInt("What is the new max amount of pre-order?");
                _dish.MaxAmountPreOrder = new_dish_max_amount_pre_order;
                break;

            case "Save Changes\n(Changes Won't be automatically saved for future uses)":
                Menu.Update(_dish, _dish_name);
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