public class Dish
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public List<Ingredient> Ingredients { get; set; }

    public Dish(string Name, string Description, double Price, string Ingredients){
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
        this.Ingredients = new();
    }
}