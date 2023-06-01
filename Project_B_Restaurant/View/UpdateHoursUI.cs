class UpdateHoursUI : UI
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
    public UpdateHoursUI(UI previousUI) : base(previousUI)
    {
    }
    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem("Change Monday"));
        MenuItems.Add(new MenuItem("Change Tuesday"));
        MenuItems.Add(new MenuItem("Change Wednesday"));
        MenuItems.Add(new MenuItem("Change Thursday"));
        MenuItems.Add(new MenuItem("Change Friday"));
        MenuItems.Add(new MenuItem("Change Saturday"));
        MenuItems.Add(new MenuItem("Change Sunday"));
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Change Monday":
                Console.WriteLine("What would you like to be the new opening hours on this day?");
                string Monday = Console.ReadLine();
                infoLogic.Restaurant.Monday = Monday;
                infoLogic.UpdateInfo();
                Console.WriteLine("The opening hours have been updated!");
                break;
            case "Change Tuesday":
                Console.WriteLine("What would you like to be the new opening hours on this day?");
                string Tuesday = Console.ReadLine();
                infoLogic.Restaurant.Tuesday = Tuesday;
                infoLogic.UpdateInfo();
                Console.WriteLine("The opening hours have been updated!");
                break;
            case "Change Wednesday":
                Console.WriteLine("What would you like to be the new opening hours on this day?");
                string Wednesday = Console.ReadLine();
                infoLogic.Restaurant.Wednesday = Wednesday;
                infoLogic.UpdateInfo();
                Console.WriteLine("The opening hours have been updated!");
                break;
            case "Change Thursday":
                Console.WriteLine("What would you like to be the new opening hours on this day?");
                string Thursday = Console.ReadLine();
                infoLogic.Restaurant.Thursday = Thursday;
                infoLogic.UpdateInfo();
                Console.WriteLine("The opening hours have been updated!");
                break;
            case "Change Friday":
                Console.WriteLine("What would you like to be the new opening hours on this day?");
                string Friday = Console.ReadLine();
                infoLogic.Restaurant.Friday = Friday;
                infoLogic.UpdateInfo();
                Console.WriteLine("The opening hours have been updated!");
                break;
            case "Change Saturday":
                Console.WriteLine("What would you like to be the new opening hours on this day?");
                string Saturday = Console.ReadLine();
                infoLogic.Restaurant.Saturday = Saturday;
                infoLogic.UpdateInfo();
                Console.WriteLine("The opening hours have been updated!");
                break;
            case "Change Sunday":
                Console.WriteLine("What would you like to be the new opening hours on this day?");
                string Sunday = Console.ReadLine();
                infoLogic.Restaurant.Sunday = Sunday;
                infoLogic.UpdateInfo();
                Console.WriteLine("The opening hours have been updated!");
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