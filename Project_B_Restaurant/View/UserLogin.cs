using System.Text;

class UserLogin : UI
{

    private AccountsLogic accountsLogic = new AccountsLogic();

    public override string SubText => "";

    public static bool Loggedin;

    public override string Header
    {
        get => @"
                                        _   
         /\                            | |  
        /  \   ___ ___ ___  _   _ _ __ | |_ 
       / /\ \ / __/ __/ _ \| | | | '_ \| __|
      / ____ \ (_| (_| (_) | |_| | | | | |_ 
     /_/    \_\___\___\___/ \__,_|_| |_|\__|
    ========================================";
    }


    public UserLogin(UI previousUI) : base(previousUI)
    {
    }
    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem("Log in", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Create Account", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Reset Password", AccountLevel.Customer));
        MenuItems.Add(new MenuItem("Update Accountdetails", AccountLevel.Customer));
        MenuItems.Add(new MenuItem("Reset Password", AccountLevel.Customer));
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Log in":
                LogIn();
                break;
            case "Create Account":
                CreateAccount();
                break;
            case "Reset Password":
                ResetPassword();
                break;
            case Constants.UI.GO_BACK:
            case Constants.UI.EXIT:
                Exit();
                break;
            default:
                Console.WriteLine("Invalid input");
                break; ;
        }
    }

    public void LogIn()
    {
        Console.WriteLine("Welcome to the login page");
        string email = GetString("Please enter your email address");
        var password = GetPassword("Please enter your password");

        AccountModel? acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine("\nWelcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);
            Loggedin = true;
            OpeningUI opening = new(null);
            opening.Start();


            //Write some code to go back to the menu
            //Menu.Start();
            // return acc;
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
                }
                else if (answer == "2")
                {
                    Start();
                }
                else
                {
                    break;
                }
            }
            // return acc;
        }
    }
    public void ResetPassword()
    {
        string email = GetString("Please enter your email: ");
        AccountModel acc = accountsLogic.GetByEmail(email);
        if (acc == null)
        {
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

    public void CreateAccount()
    {
        AccountLevel? CurrentLevel = AccountsLogic.CurrentAccount?.Level;
        var level = AccountLevel.Customer;
        if (CurrentLevel == AccountLevel.Admin)
        {
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

    public string GetPassword(string question){
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
}
// public static class UserLogin
// {
//     static private AccountsLogic accountsLogic = new AccountsLogic();
//     static public bool loggedIn = false;




//         string fullName = GetString("What your full name?");
//         string emailAddress = GetString("What is your email address?");
//         string password = GetPassword("What is your password?");
//         var confirm_password = GetPassword("Confirm your password:");
//         while (password != confirm_password)
//         {
//             Console.WriteLine("\nThe passwords do not match. Please try again.");
//             password = GetPassword("Enter a password:");
//             confirm_password = GetPassword("Confirm your password:");
//         }
//         int id = accountsLogic.GetLastId() + 1;
//         AccountModel acc = new AccountModel(id, emailAddress, password, fullName, level);
//         accountsLogic.UpdateList(acc);

//         Console.WriteLine("You have succesfully created an account!");
//     }

//     public static void AddEmployee()
//     {
//         var level = AccountLevel.Employee;
//         Console.WriteLine("What their full name?");
//         string fullName = Console.ReadLine();
//         Console.WriteLine("What is their email address?");
//         string emailAddress = Console.ReadLine();
//         Console.WriteLine("Enter a password:");
//         string password = Console.ReadLine();
//         Console.WriteLine("Confirm the password:");
//         string confirm_password = Console.ReadLine();
//         while (password != confirm_password)
//         {
//             Console.WriteLine("The passwords do not match. Please try again.");
//             Console.WriteLine("Enter a password:");
//             password = Console.ReadLine();
//             Console.WriteLine("Confirm the password:");
//             confirm_password = Console.ReadLine();
//         }
//         int id = accountsLogic.GetLastId() + 1;
//         AccountModel acc = new AccountModel(id, emailAddress, password, fullName, level);
//         accountsLogic.UpdateList(acc);

//         Console.WriteLine("You have succesfully created an employee account!");
//     }




// }