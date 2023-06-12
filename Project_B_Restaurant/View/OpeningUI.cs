
public class OpeningUI : UI
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

    public override string SubText
    {
        get => string.Empty;
    }

    public OpeningUI(UI previousUI) : base(previousUI)
    {
    }

    public override void CreateMenuItems()
    {
        MenuItems.Clear();

        if (AccountsLogic.CurrentAccount == null)
            MenuItems.Add(new MenuItem(Constants.OpeningUI.LOGIN, AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Account Info", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Restaurant Info", AccountLevel.Guest));
        MenuItems.Add(new MenuItem(Constants.OpeningUI.MENU, AccountLevel.Guest));
        MenuItems.Add(new MenuItem(Constants.OpeningUI.RESERVATION, AccountLevel.Guest));
    }
    public AccountModel acc;
    public override void UserChoosesOption(int choice)
    {
        switch (UserOptions[choice].Name)
        {
            case Constants.OpeningUI.LOGIN:
                UserLogin userlogin = new(this);
                userlogin.LogIn();
                break;
            case "Account Info":
                AccountUI account = new(this);
                account.Start();
                break;
            case "Restaurant Info":
                RestaurantInfoUI restaurantInfo = new(this);
                restaurantInfo.Start();
                break;
            case Constants.OpeningUI.MENU:
                MenuUI menu = new(this);
                menu.Start();
                break;
            case Constants.OpeningUI.RESERVATION:
                ReservationUI reservation = new(this);
                reservation.Start();
                break;
            case Constants.UI.GO_BACK:
            case Constants.UI.EXIT:
                Exit();
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }


    // public static void Start()
    // {
    //     OpeningUI opening = UIFactory.CreateOpeningUI();
    //     Console.WriteLine(opening.Header);
    //     if (UserLogin.loggedIn == true)
    //     {
    //         Console.ForegroundColor = ConsoleColor.DarkGray;
    //         Console.WriteLine("Enter 1 to login", Console.ForegroundColor);
    //         Console.ForegroundColor = ConsoleColor.White;
    //     }
    //     else
    //     {
    //         Console.WriteLine("Enter 1 to login");
    //     }
    //     Console.WriteLine("Enter 2 to See the menu");
    //     Console.WriteLine("Enter 3 to create an account");
    //     // For Demo
    //     if (UserLogin.loggedIn == true)
    //     {
    //         Console.WriteLine("Enter 3 Reservation");
    //         Console.WriteLine("Enter 4 Leave");
    //     }
    //     else
    //     {
    //         Console.WriteLine("Enter 4 Leave");
    //     }

    //     string input = Console.ReadLine();
    //     if (input == "1")
    //     {
    //         if (UserLogin.loggedIn == true)
    //         {
    //             Console.WriteLine("You are already logged in.");
    //             Start();
    //         }
    //         else
    //         {
    //             UserLogin.Start();
    //         }
    //     }
    //     else if (input == "2")
    //     {
    //         MenuUI menu = UIFactory.CreateMenuUI();
    //         menu.Start();
    //     }
    //     else if (input == "3")
    //     {
    //         if (UserLogin.loggedIn == true)
    //         {
    //             ReservationUI.initReserve();
    //         }
    //         else
    //         {
    //             UserLogin.CreateAccount();

    //         }

    //     }
    //     else if (input == "4")
    //     {
    //         Console.WriteLine("Goodbye!");
    //         Environment.Exit(0);
    //     }
    //     else
    //     {
    //         Console.WriteLine("Invalid input");
    //         Start();
    //     }

    // }
}