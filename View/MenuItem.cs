public class MenuItem
{
    public string Name { get; set; }

    public AccountLevel Level { get; set; }
    public MenuItem(string name, AccountLevel level)
    {
        Name = name;
        Level = level;
    }

    public MenuItem(string name) : this(name, AccountLevel.Guest)
    {

    }
}