class OpeningUI : UI
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role

    // logic not completely refactored yet.
    public OpeningUI(string[] menuItems) : base(menuItems)
    {

    }

    public override void ShowMenu()
    {
        for (int i = 0; i < MenuItems.Length; i++)
        {
            Console.WriteLine($"Enter {i + 1} to {MenuItems[i]}");
        }
    }

    // Could add validation to input, or seperate function
    public override string GetInput() => Console.ReadLine() ?? string.Empty;

    static public void Start()
    {
        Console.WriteLine(Header("menu"));
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
        else{
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
            Menu menu = new Menu();
            menu.Start();
        }
        else if (input == "3")
        {
            if (UserLogin.loggedIn == true)
            {
                ReservationModule.initReserve();
            }
            else{
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