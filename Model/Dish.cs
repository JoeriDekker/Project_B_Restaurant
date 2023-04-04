public class Dish
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public bool InStock { get; set; }
    public int PreOrderAmount { get; set; }
    public int MaxAmountPreOrder { get; set; }


    public Dish(string Name, string Description, double Price, string Type){
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
        this.Type = Type;
        this.InStock = true;
        this.PreOrderAmount = 0;
        this.MaxAmountPreOrder = 3;
    }
}