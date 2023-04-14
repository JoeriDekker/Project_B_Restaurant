public class Dish
{
    private static int nextID = 1;

    public int ID { get; set; }
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
    public string Allergies { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public bool InStock { get; set; }
    public int PreOrderAmount { get; set; }
    public int MaxAmountPreOrder { get; set; }


    public Dish(string Name, List<string> Ingredients, string Allergies, double Price, string Type)
    {
        this.Name = Name;
        this.Ingredients = Ingredients;
        this.Allergies = Allergies;
        this.Price = Price;
        this.Type = Type;
        this.InStock = true;
        this.PreOrderAmount = 0;
        this.MaxAmountPreOrder = 3;
        ID = nextID;
        nextID++;
    }
    public Dish() : this(string.Empty, new(), string.Empty, 0.0, string.Empty)
    {

    }

    public override string ToString()
    {
        return  $"{ID}:{Name}:{string.Join(", ", Ingredients)}:{Allergies}:{Price}:{Type}";
    }
}