public abstract class UI
{
    // Idea of design for an abstract UI. Each Implementation will implement its own methods to request and validate the corresponding input.
    // The UI will be responsible for displaying the UI, the header, populating the dictionary with the menu items,getting the input from the user
    // And executing the corresponding method in the controller.

    // If a screen results in its own suboptions. Make it a UI class.
    protected AccountType? AccountLevel = null;
    protected List<MenuItem> MenuItems { get; set; } = new();

    // MenuItems mapped 1 to MenuItems.Count
    protected Dictionary<int, MenuItem> UserOptions = new();

    public UI? PreviousUI { get; set; }
    public abstract string Header { get; }

    public UI(UI previousUI)
    {
        PreviousUI = previousUI;
        AccountLevel = AccountsLogic.CurrentAccount?.Type;
        CreateMenuItems();
    }


    // Switch case the option and the Constants used in MenuItems, see OpeningUI or MenuUI for implementation.
    public abstract void UserChoosesOption(int option);

    // Add the strings as Constants in Constants.cs then create the MenuItems with those constants by implementing this function.
    // See OpeningUI or MenuUI for example. 
    public abstract void CreateMenuItems();

    public void Add(MenuItem menuItem) => MenuItems.Add(menuItem);

    public void Start()
    {
        while (true)
        {
            ShowUI();
            Continue();
        }
    }

    public virtual void ShowUI()
    {
        Console.Clear();
        Console.WriteLine(Header);
        Console.WriteLine("Please select an option:");
        ResetUserOptions();
        foreach (var opt in UserOptions)
        {
            Console.WriteLine($"{opt.Key}. {opt.Value.Name}");
        }
        int choice = GetInput();
        UserChoosesOption(choice);
    }

    // Gets input as int to use in the Dictionary.
    public int GetInput()
    {
        string input;
        int choice = 99;

        do
        {
            Console.Write("?: > ");
            input = Console.ReadLine() ?? string.Empty;
            try
            {
                choice = int.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Incorrect choice.");
                continue;
            }
        }
        while (!UserOptions.ContainsKey(choice));

        return choice;
    }

    public void ResetUserOptions()
    {
        UserOptions.Clear();
        List<MenuItem> filteredMenuItems = FilterMenuItems();
        for (int i = 1; i <= filteredMenuItems.Count; i++)
        {
            UserOptions.Add(i, filteredMenuItems[i - 1]);
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

    public List<MenuItem> FilterMenuItems()
    {
        if (AccountLevel == null)
        {
            return MenuItems.FindAll(x => x.AccountLevel == AccountType.Guest);
        }
        else
        {
            return MenuItems.FindAll(x => x.AccountLevel <= AccountLevel);
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