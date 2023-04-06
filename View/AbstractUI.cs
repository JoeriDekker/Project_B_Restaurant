public abstract class UI
{
    // Idea of design for an abstract UI. Check OpeningUI for example.
    public string[] MenuItems { get; set; }

    public Dictionary<int, string> UserOptions = new();

    public UI(string[] menuItems)
    {
        MenuItems = menuItems;
    }

    public abstract void ShowUI();

    public string GetInput() => Console.ReadLine() ?? string.Empty;
    
    public abstract string Header { get; }

    public void PopulateDictionary()
    {
        for (int i = 1; i <= MenuItems.Length; i++)
        {
            UserOptions.Add(i, MenuItems[i]);
        }
    }
}