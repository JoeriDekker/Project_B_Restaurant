abstract class UI
{
    // Idea of design for an abstract UI. Check openingUI for example.
    public string[] MenuItems;

    public UI(string[] menuItems)
    {
        MenuItems = menuItems; 
    }

    public abstract void ShowMenu();

    public abstract string GetInput();
}