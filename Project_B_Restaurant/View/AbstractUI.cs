public abstract class UI
{
    // Idea of design for an abstract UI. Each Implementation will implement its own methods to request and validate the corresponding input.
    // The UI will be responsible for displaying the UI, the header, populating the dictionary with the menu items,getting the input from the user
    // And executing the corresponding method in the controller.

    // If a screen results in its own suboptions. Make it a UI class.
    protected List<MenuItem> MenuItems { get; set; } = new();

    // MenuItems mapped 1 to MenuItems.Count
    protected Dictionary<int, MenuItem> UserOptions = new();

    public UI? PreviousUI { get; set; }
    public abstract string Header { get; }
    public abstract string SubText { get; }

    public UI(UI previousUI)
    {
        PreviousUI = previousUI;
        CreateMenuItems();
    }


    // Switch case the option and the string used in MenuItems, see OpeningUI or MenuUI for implementation.
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
            Reset();
        }
    }

    public virtual void Reset()
    {
        MenuItems.Clear();
        CreateMenuItems();
        ResetUserOptions();
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
        string user = (AccountsLogic.CurrentAccount != null) ? AccountsLogic.CurrentAccount.ToString() : "Guest";
        Console.WriteLine($"Hello, {user}");
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
                Console.WriteLine("Incorrect Choice");
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
            Console.Write("?: > ");
            input = Console.ReadLine() ?? string.Empty;
        }
        while (input == string.Empty);

        return input;
    }

    public string GetPassword(string question)
    {
        Console.WriteLine($"{question}");
        var password = string.Empty;
        ConsoleKey key;
        Console.Write("?: > ");
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && password.Length > 0)
            {
                Console.Write("\b \b");
                password = password[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                password += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        Console.WriteLine("");
        return password;
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
            if (!int.TryParse(input, out number))
            {
                Console.WriteLine("Incorrect input. Please supply a whole number.");
                continue;
            }
        } while (!int.TryParse(input, out number));

        return number;
    }

    public double GetDouble(string question)
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
        if (AccountsLogic.CurrentAccount == null)
        {
            return MenuItems.FindAll(x => x.Level == AccountLevel.Guest);
        }
        else
        {
            return MenuItems.FindAll(x => x.Level <= AccountsLogic.CurrentAccount.Level);
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