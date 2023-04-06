static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static public bool loggedIn = false;


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);
            loggedIn = true;
            OpeningUI.Start();
            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
            Console.WriteLine("Did you forget your password?");
            string answer = "";
            while (answer != "1" || answer != "2")
            {
                Console.WriteLine("Enter 1 to reset your password");
                Console.WriteLine("Enter 2 to try log in again");
                answer = Console.ReadLine();
                if (answer == "1")
                {
                    ResetPassword();
                }
                else if (answer == "2")
                {
                    Start();
                }
            }
        }
    }
    private static void ResetPassword()
    {
        Console.WriteLine("Please enter your email: ");
        string email = Console.ReadLine();
        AccountModel acc = accountsLogic.GetByEmail(email);
        Console.WriteLine("Please enter your new password: ");
        string new_password = Console.ReadLine();
        Console.WriteLine("Please enter your new password again to confirm: ");
        string confirm_password = Console.ReadLine();
        if (new_password == confirm_password)
        {
            acc.Password = new_password;
            accountsLogic.UpdateList(acc);
            Console.WriteLine("Your password has been updated!");
            OpeningUI.Start();
        }
        else
        {
            ResetPassword();
        }
    }

    public static void CreateAccount()
    {
        string type = "";
        Console.WriteLine("Are you an admin or customer? \nEnter 1 for Admin \nEnter 2 for Customer");
        string choice = Console.ReadLine();
        if (choice == "1")
        {
            type = "Admin";
        }
        else if (choice == "2")
        {
            type = "Customer";
        }
        else
        {
            Console.WriteLine("Invalid input");
            CreateAccount();
        }
        Console.WriteLine("What your full name?");
        string fullName = Console.ReadLine();
        Console.WriteLine("What is your e-mailaddress?");
        string emailAddress = Console.ReadLine();
        Console.WriteLine("Enter a password:");
        string password = Console.ReadLine();
        Console.WriteLine("Confirm your password:");
        string confirm_password = Console.ReadLine();
        while (password != confirm_password)
        {
            Console.WriteLine("Enter a password:");
            password = Console.ReadLine();
            Console.WriteLine("Confirm your password:");
            confirm_password = Console.ReadLine();
        }
        int id = accountsLogic.GetLastId() + 1;
        AccountModel acc = new AccountModel(id, emailAddress, password, fullName, type);
        accountsLogic.UpdateList(acc);

        Console.WriteLine("You have succesfulle created an account!");
        OpeningUI.Start();
    }
}