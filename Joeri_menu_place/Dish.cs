public class Dish
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

    public Dish(string Name, string Description, double Price){
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
    }
}