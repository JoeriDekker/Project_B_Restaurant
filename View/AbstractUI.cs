public abstract class UI
{
    // Idea of design for an abstract UI. Each Implementation will implement its own methods to request and validate the corresponding input.
    // The UI will also be responsible for displaying the UI, the header, populating the dictionary with the menu items,getting the input from the user
    // And executing the corresponding method in the controller.
    protected List<MenuItem> MenuItems { get; set; } = new();
    protected Dictionary<int, MenuItem> UserOptions = new();
    public UI? PreviousUI { get; set; }
    public abstract string Header { get; }

    public UI(UI previousUI)
    {
        PreviousUI = previousUI;
        CreateMenuItems();
    }

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
