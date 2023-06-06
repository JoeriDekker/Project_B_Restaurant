using System.Text;

class AccountUI : UI
{

    private AccountsLogic accountsLogic = new AccountsLogic();

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
        if (AccountsLogic.CurrentAccount != null){
            
            AccountInfo = $"\nName: {AccountsLogic.CurrentAccount.FullName}\nEmail: {AccountsLogic.CurrentAccount.EmailAddress}\nAccount type: {AccountsLogic.CurrentAccount.Level}\n";
        }
        else{
            AccountInfo = "\nYou have not been loggedin.\nPlease log in to show you account details\n";
        }
    }
    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem("Log in", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Create Account", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Reset Password", AccountLevel.Customer));
        MenuItems.Add(new MenuItem("Update Accountdetails", AccountLevel.Customer));
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
                UserLogin create_account = new(this);
                create_account.CreateAccount();
                break;
            case "Reset Password":
                UserLogin user = new(this);
                user.ResetPassword();
                break;
            case "Update Accountdetails":
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
}
