public abstract class UI
{
    // Idea of design for an abstract UI. Each Implementation will implement its own methods to request and validate the corresponding input.
    // The UI will be responsible for displaying the UI, the header, populating the dictionary with the menu items,getting the input from the user
    // And executing the corresponding method in the controller.

    // If a screen results in its own suboptions. Make it a UI class.
    protected List<MenuItem> MenuItems { get; set; } = new();

    // MenuItems mapped to 1 to MenuItems.Count
    protected Dictionary<int, MenuItem> UserOptions = new();

    public UI? PreviousUI { get; set; }
    public abstract string Header { get; }

    // When instantiating the new UI, pass 'this' as argument.
    public UI(UI previousUI)
    {
        PreviousUI = previousUI;
        CreateMenuItems();
    }

    // Add the strings as Constants in Constants.cs then create the MenuItems with those constants by implementing this function.
    // See OpeningUI or MenuUI for example. 
    public abstract void CreateMenuItems();

    public void ShowUI()
    {
        Console.Clear();
        Console.WriteLine(Header);
        Console.WriteLine("Please select an option:");
        UserOptions.Clear();
        PopulateDictionary();
        foreach (var option in UserOptions)
        {
            Console.WriteLine($"{option.Key}. {option.Value.Name}");
        }
    }

    // Gets input as int to use in the Dictionary.
    public int GetInput()
    {
        string input;

        do
        {
            input = Console.ReadLine() ?? string.Empty;
        }
        while (!UserOptions.ContainsKey(int.Parse(input)));

        return int.Parse(input);
    }

    public void Add(MenuItem menuItem) => MenuItems.Add(menuItem);


    public void PopulateDictionary()
    {
        for (int i = 1; i <= MenuItems.Count; i++)
        {
            UserOptions.Add(i, MenuItems[i - 1]);
        }

        if (PreviousUI == null)
        {
            UserOptions.Add(0, new MenuItem(Constants.UI.EXIT));
        }
        else
        {
            UserOptions.Add(0, new MenuItem(Constants.UI.GO_BACK));
        }
    }

    // Switch case the option and the Constants used in MenuItems, see OpeningUI or MenuUI for implementation.
    public abstract void UserChoosesOption(int option);

    public void Start()
    {
        while (true)
        {
            ShowUI();
            int option = GetInput();
            UserChoosesOption(option);
            Continue();
        }
    }

    public void Exit()
    {
        if (PreviousUI == null)
        {
            Environment.Exit(0);
        }
        else
        {
            PreviousUI.Start();
        }
    }

    public void Continue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
