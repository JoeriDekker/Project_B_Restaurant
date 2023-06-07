class UpdateAccountUI : UI
{
    private AccountsLogic AccountController = new AccountsLogic();

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

    public override string SubText
    {
        
        get => @$"
======================================
{AccountsLogic.CurrentAccount.ShowInfo()}
======================================
        ";
    }


    public AccountModel? CurrentAccount = AccountsLogic.CurrentAccount;
    public UpdateAccountUI(UI previousUI) : base(previousUI)
    {
    }
    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem("Change Full name"));
        MenuItems.Add(new MenuItem("Change Email"));
        MenuItems.Add(new MenuItem("Change Password"));
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Change Full name": 
                ChangeName();
                break;
            case "Change Email":
                ChangeEmail();
                break;
            case "Change Password":
                UserLogin userPassword = new(this);
                userPassword.ResetPassword();
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

    public void ChangeName(){
        String NewName = GetString("What is your Full Name?");
        CurrentAccount.FullName = NewName;
        AccountController.UpdateList(CurrentAccount);
        Start();
    }

    public void ChangeEmail(){
        String NewEmail = GetString("What is your new Email?");
        CurrentAccount.EmailAddress = NewEmail;
        AccountController.UpdateList(CurrentAccount);
        Start();
    }
}