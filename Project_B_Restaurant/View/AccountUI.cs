using System.Text;

class AccountUI : UI
{

    private AccountsLogic accountsLogic = new AccountsLogic();
    private ReservationLogic reservationLogic = new();

    AccountModel? account;

    public override string SubText => AccountInfo;

    public string AccountInfo = "";

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


    public AccountUI(UI previousUI) : base(previousUI)
    {
        if (AccountsLogic.CurrentAccount != null)
            AccountInfo = GenerateSubText();
        else
            AccountInfo = "\nYou have not been logged in.\nPlease log in to show you account details\n";

    }
    public string GenerateSubText()
    {

        account = accountsLogic.GetById(AccountsLogic.CurrentAccount!.Id);
        StringBuilder sb = new();
        sb.AppendLine("\nACCOUNT INFORMATION");
        sb.AppendLine("======================================");
        sb.AppendLine($"Name: {AccountsLogic.CurrentAccount.FullName}");
        sb.AppendLine($"Email: {AccountsLogic.CurrentAccount.EmailAddress}");
        sb.AppendLine($"Account type: {AccountsLogic.CurrentAccount.Level}");
        sb.AppendLine("======================================");

        if (account!.Reservations.Count() != 0)
        {
            sb.AppendLine("Your Reservations:");
            foreach (string rcode in account.Reservations)
            {
                sb.AppendLine($"  - {reservationLogic.getReservationByCode(rcode)!.ToString()}");

            }
            sb.AppendLine("======================================");
        }
        return sb.ToString();
    }


    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        if (AccountsLogic.CurrentAccount == null)
        {
            MenuItems.Add(new MenuItem("Log in", AccountLevel.Guest));
            MenuItems.Add(new MenuItem("Create Account", AccountLevel.Guest));
        }

        
        if (AccountsLogic.CurrentAccount != null)
        {
            MenuItems.Add(new MenuItem("Reset Password", AccountLevel.Customer));
            MenuItems.Add(new MenuItem("Update Account Details", AccountLevel.Customer));
            MenuItems.Add(new MenuItem("Create Account", AccountLevel.Admin));

            account = accountsLogic.GetById(AccountsLogic.CurrentAccount.Id);
            if (account!.Reservations.Count() != 0)
            {
                MenuItems.Add(new MenuItem("Cancel Reservation", AccountLevel.Customer));
                MenuItems.Add(new MenuItem("Update Reservation", AccountLevel.Customer));
            }
        }
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Log in":
                UserLogin login = new(this);
                login.LogIn();
                break;
            case "Create Account":
                CreateAccount();
                break;
            case "Reset Password":
                UserLogin user = new(this);
                user.ResetPassword();
                break;
            case "Update Account Details":
                UpdateAccountUI UpdateAcc = new(this);
                UpdateAcc.Start();
                break;
            case "Cancel Reservation":
                CancelReservation();
                AccountInfo = GenerateSubText();
                break;
            case "Update Reservation":
                UpdateReservation();
                AccountInfo = GenerateSubText();
                break;
            case Constants.UI.GO_BACK:
            case Constants.UI.EXIT:
                AccountsLogic.CurrentAccount = account;
                Exit();
                break;
            default:
                Console.WriteLine("Invalid input");
                break; ;
        }
    }
    public void CancelReservation()
    {
        string res_code = GetString("Enter the Reservation Code").ToUpper();
        if (reservationLogic.getReservationByCode(res_code) != null)
        {
            reservationLogic.DeleteReservationByID(res_code);
            accountsLogic.RemoveReservationCode(res_code);
        }
        else
        {
            Console.WriteLine("Reservation could not be found! Please try another code.");
        }
    }

    public void UpdateReservation()
    {
        string res_code = GetString("Enter the Reservation Code").ToUpper();
        if (account!.Reservations.Contains(res_code))
        {
            UpdateReservationUI res = new(this, reservationLogic.getReservationByCode(res_code)!);
            res.Start();
        }
        else
            Console.WriteLine("Reservation not found in account");
    }

    public void CreateAccount()
    {
        AccountLevel? CurrentLevel = AccountsLogic.CurrentAccount?.Level;
        var level = AccountLevel.Customer;
        if (CurrentLevel == AccountLevel.Admin)
        {
            Console.WriteLine("What type of account do you want to make? \nEnter 1 for Admin \nEnter 2 for Employee \nEnter 3 for Customer");
            string choice = Console.ReadLine() ?? string.Empty;
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
}
