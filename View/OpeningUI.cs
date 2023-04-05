class OpeningUI : UI
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role

    // logic not completely refactored yet.
    public override string Header
    {
        get => @"
     __          __  _                          
     \ \        / / | |                         
      \ \  /\  / /__| | ___ ___  _ __ ___   ___ 
       \ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \
        \  /\  /  __/ | (_| (_) | | | | | |  __/
         \/  \/ \___|_|\___\___/|_| |_| |_|\___|
    =============================================
    ";
    }
    public OpeningUI(string[] menuItems) : base(menuItems)
    {

    }

    public override void ShowUI()
    {
        Console.WriteLine(Header);
        for (int i = 0; i < MenuItems.Length; i++)
        {
            Console.WriteLine($"Enter {i + 1} to {MenuItems[i]}");
        }
    }

    // Could add validation to input, or seperate function

    public static void Start()
    {
        OpeningUI opening = UIFactory.CreateOpeningUI();
        Console.WriteLine(opening.Header);
        if (UserLogin.loggedIn == true)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter 1 to login", Console.ForegroundColor);
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.WriteLine("Enter 1 to login");
        }
        Console.WriteLine("Enter 2 to See the menu");
        // For Demo
        if (UserLogin.loggedIn == true)
        {
            Console.WriteLine("Enter 3 Reservation");
            Console.WriteLine("Enter 4 Leave");
        }
        else
        {
            Console.WriteLine("Enter 3 Leave");
        }

        string input = Console.ReadLine();
        if (input == "1")
        {
            if (UserLogin.loggedIn == true)
            {
                Console.WriteLine("You are already logged in.");
                Start();
            }
            else
            {
                UserLogin.Start();
            }
        }
        else if (input == "2")
        {
            MenuUI menu = UIFactory.CreateMenuUI();
            menu.Start();
        }
        else if (input == "3")
        {
            if (UserLogin.loggedIn == true)
            {
                ReservationUI.initReserve();
            }
            else
            {
                Console.WriteLine("Goodbye!");

            }

        }
        else if (input == "4")
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }
}