public class Dish
{
    private static int _counter = 0;

    private int _id;

    public int ID
    {
        get => _id;
        set => _id = _counter++;
    }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Allergies { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public bool InStock { get; set; }
    public int PreOrderAmount { get; set; }
    public int MaxAmountPreOrder { get; set; }


    public Dish(string Name, string Ingredients, string Allergies, double Price, string Type)
    {
        this.Name = Name;
        this.Ingredients = Ingredients;
        this.Allergies = Allergies;
        this.Price = Price;
        this.Type = Type;
        this.InStock = true;
        this.PreOrderAmount = 0;
        this.MaxAmountPreOrder = 3;
    }
}