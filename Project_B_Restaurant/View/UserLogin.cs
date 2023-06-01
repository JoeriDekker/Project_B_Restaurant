public static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static public bool loggedIn = false;


    public static AccountModel Start()
    {
        Console.WriteLine("Welcome to the login page");
        string email = GetString("Please enter your email address");
        var password = GetPassword("Please enter your password");
        
        AccountModel? acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine("\nWelcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);

            loggedIn = true;
            //Write some code to go back to the menu
            //Menu.Start();
            return acc;
        }
        else
        {
            Console.WriteLine("\nNo account found with that email and password");
            Console.WriteLine("Did you forget your password?");
            string answer = "";
            while (answer != "1" || answer != "2" || answer != "3")
            {
                Console.WriteLine("Enter 1 to reset your password");
                Console.WriteLine("Enter 2 to try log in again");
                Console.WriteLine("Enter 3 Go Back");
                answer = Console.ReadLine();
                if (answer == "1")
                {
                    ResetPassword();
                    return acc;
                }
                else if (answer == "2")
                {
                    Start();
                    return acc;
                }
            }
            return acc;
        }
    }
    private static void ResetPassword()
    {
        string email = GetString("Please enter your email: ");
        AccountModel acc = accountsLogic.GetByEmail(email);
        if (acc == null){
            Console.WriteLine("Email has not been found!");
            ResetPassword();
        }
        string new_password = GetPassword("Please enter your new password: ");
        string confirm_password = GetPassword("Please enter your new password again to confirm: ");
        if (new_password == confirm_password)
        {
            acc.Password = new_password;
            accountsLogic.UpdateList(acc);
            Console.WriteLine("Your password has been updated!");
        }
        else
        {
            ResetPassword();
        }
    }

    public static void CreateAccount()
    {   
        AccountLevel? CurrentLevel = AccountsLogic.CurrentAccount?.Level;
        var level = AccountLevel.Guest;
        if (CurrentLevel == AccountLevel.Admin){
            Console.WriteLine("What type of account do you want to make? \nEnter 1 for Admin \nEnter 2 for Employee \nEnter 3 for Customer");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                level = AccountLevel.Admin;
            }
            else if (choice == "2")
            {
                level = AccountLevel.Employee;
            }
            else if (choice == "3")
            {
                level = AccountLevel.Customer;
            }
            else
            {
                Console.WriteLine("Invalid input");
                CreateAccount();
            }
        }
        
        string fullName = GetString("What your full name?");
        string emailAddress = GetString("What is your email address?");
        string password = GetPassword("What is your password?");
        var confirm_password = GetPassword("Confirm your password:");
        while (password != confirm_password)
        {
            Console.WriteLine("\nThe passwords do not match. Please try again.");
            password = GetPassword("Enter a password:");
            confirm_password = GetPassword("Confirm your password:");
        }
        int id = accountsLogic.GetLastId() + 1;
        AccountModel acc = new AccountModel(id, emailAddress, password, fullName, level);
        accountsLogic.UpdateList(acc);

        Console.WriteLine("You have succesfully created an account!");
    }

    public static void AddEmployee()
    {
        var level = AccountLevel.Employee;
        Console.WriteLine("What their full name?");
        string fullName = Console.ReadLine();
        Console.WriteLine("What is their email address?");
        string emailAddress = Console.ReadLine();
        Console.WriteLine("Enter a password:");
        string password = Console.ReadLine();
        Console.WriteLine("Confirm the password:");
        string confirm_password = Console.ReadLine();
        while (password != confirm_password)
        {
            Console.WriteLine("The passwords do not match. Please try again.");
            Console.WriteLine("Enter a password:");
            password = Console.ReadLine();
            Console.WriteLine("Confirm the password:");
            confirm_password = Console.ReadLine();
        }
        int id = accountsLogic.GetLastId() + 1;
        AccountModel acc = new AccountModel(id, emailAddress, password, fullName, level);
        accountsLogic.UpdateList(acc);

        Console.WriteLine("You have succesfully created an employee account!");
    }

    public static string GetString(string question)
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

    public static string GetPassword(string question){
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

    public static void Print(AccountModel acc)
    {
        Console.WriteLine($"\nAccount Information:\nID: {acc.Id} \nFull Name: {acc.FullName} \nEmail address {acc.EmailAddress} \nAccount Level: {acc.Level}\n");
    }
}