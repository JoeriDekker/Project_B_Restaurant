public abstract class UI
{
    // Idea of design for an abstract UI. Check OpeningUI for example.
    public string[] MenuItems { get; set; }

    public UI(string[] menuItems)
    {
        MenuItems = menuItems;
    }

    public abstract void ShowUI();

    public string GetInput() => Console.ReadLine() ?? string.Empty;
    
    public abstract string Header { get; }
}