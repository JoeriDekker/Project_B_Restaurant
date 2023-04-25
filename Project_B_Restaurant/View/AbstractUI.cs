public abstract class UI
{
    // Idea of design for an abstract UI. Each Implementation will implement its own methods to request and validate the corresponding input.
    // The UI will be responsible for displaying the UI, the header, populating the dictionary with the menu items,getting the input from the user
    // And executing the corresponding method in the controller.

    // If a screen results in its own suboptions. Make it a UI class.
    protected AccountLevel? Level = null;
    protected List<MenuItem> MenuItems { get; set; } = new();

    // MenuItems mapped 1 to MenuItems.Count
    protected Dictionary<int, MenuItem> UserOptions = new();

    public UI? PreviousUI { get; set; }
    public abstract string Header { get; }

    public bool _futuremenu;

    public abstract string SubText { get; }

    public UI(UI previousUI)
    {
        PreviousUI = previousUI;
        Level = AccountsLogic.CurrentAccount?.Level;
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
            int choice = RequestChoice();
            UserChoosesOption(choice);
            Continue();
        }
    }

    public virtual void ShowUI()
    {
        Console.Clear();
        ShowHeader();
        ShowSubText();
        ShowUser();
        ShowOptions();
    }
    public void ShowHeader()
    {
        Console.WriteLine(Header);
    }

    public void ShowSubText()
    {
        if (SubText != string.Empty)
        {
            Console.WriteLine(SubText);
        }
    }

    public void ShowOptions()
    {
        ResetUserOptions();
        Console.WriteLine("Please select an option:");
        foreach (var opt in UserOptions)
        {
            Console.WriteLine($"{opt.Key}. {opt.Value.Name}");
        }
    }

    public void ShowUser()
    {
        Console.Write("Hello, ");
        if (AccountsLogic.CurrentAccount != null)
            Console.WriteLine(AccountsLogic.CurrentAccount.ToString());
        else 
            Console.WriteLine("Guest");
    }

    // Gets choice as int to use in the Dictionary.
    public int RequestChoice()
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
                Console.WriteLine($"Incorrect choice. Please provide a number between 0 and {MenuItems.Count}");
                continue;
            }
        }
        while (!UserOptions.ContainsKey(choice));

        return choice;
    }

    // Request methods for different types of input.
    public string GetString(string question)
    {
        string input;
        do
        {
            Console.WriteLine(question);
            Console.Write("?: >");
            input = Console.ReadLine() ?? string.Empty;
        }
        while (input == string.Empty);

        return input;
    }

    public int GetInt(string question)
    {
        string input;
        int number = 0;
        do
        {
            Console.WriteLine(question);
            Console.Write("?: >");
            input = Console.ReadLine() ?? string.Empty;
            try
            {
                number = int.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect input. Please supply a whole number.");
                continue;
            }
        }
        while (input == string.Empty);

        return number;
    }

    public double RequestDouble(string question)
    {
        string input;
        double number = 0;
        do
        {
            Console.WriteLine(question);
            Console.Write("?: >");
            input = Console.ReadLine() ?? string.Empty;
            try
            {
                number = double.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect input. Follow the following format: 0.0");
                continue;
            }
        }
        while (input == string.Empty);

        return number;
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
        if (Level == null)
        {
            return MenuItems.FindAll(x => x.Level == AccountLevel.Guest);
        }
        else
        {
            return MenuItems.FindAll(x => x.Level <= this.Level);
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