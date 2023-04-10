public class MenuItem
{
    public string Name { get; set; }

    public AccountType AccountLevel { get; set; }
    public MenuItem(string name, AccountType accountLevel)
    {
        Name = name;
        AccountLevel = accountLevel;
    }

    public MenuItem(string name) : this(name, AccountType.Guest)
    {

    }
}