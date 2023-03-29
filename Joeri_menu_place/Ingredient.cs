public class Ingredient
{
    public string Name { get; set; }
    public int Quantity { get; set;}
    public string Unit { get; set; }

    public Ingredient(string name, int quantity, string unit)
    {
        Name = name;
        Quantity = quantity;
        Unit = unit;
    }
}