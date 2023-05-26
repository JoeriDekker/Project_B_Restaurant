class RestaurantInfoUI : UI
{
    private infoController infoLogic = new infoController();

    public override string Header
    {
        get => @"     
     _____           _                              _   
    |  __ \         | |                            | |  
    | |__) |___  ___| |_ __ _ _   _ _ __ __ _ _ __ | |_ 
    |  _  // _ \/ __| __/ _` | | | | '__/ _` | '_ \| __|
    | | \ \  __/\__ \ || (_| | |_| | | | (_| | | | | |_ 
    |_|  \_\___||___/\__\__,_|\__,_|_|  \__,_|_| |_|\__|
    ====================================================";
    }

    public override string SubText
    {
        get => @$"
======================================
{infoLogic.Restaurant.ToString()}
======================================
        ";
    }
    public RestaurantInfoUI(UI previousUI) : base(previousUI)
    {
    }
    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem("Create Employee Account", AccountLevel.Admin));
        MenuItems.Add(new MenuItem("Change Restaurant Info", AccountLevel.Admin));
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Create Employee Account":
                UserLogin.AddEmployee();
                break;
            case "Change Restaurant Info":
                UpdateInfoUI updateInfo = new(this);
                updateInfo.Start();
                break;
            case "Show Restaurant Info":
                Console.WriteLine(infoLogic.Restaurant.ToString());
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