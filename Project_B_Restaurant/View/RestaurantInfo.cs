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
        MenuItems.Add(new MenuItem("See Restaurant Layout", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Change Restaurant Info", AccountLevel.Admin));
        MenuItems.Add(new MenuItem("Change Opening Hours", AccountLevel.Admin));
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "See Restaurant Layout":
                RestaurantLayout();
                break;
            case "Change Restaurant Info":
                UpdateInfoUI updateInfo = new(this);
                updateInfo.Start();
                break;
            case "Change Opening Hours":
                UpdateHoursUI updateHours = new(this);
                updateHours.Start();
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

    public void RestaurantLayout(){
        Console.WriteLine("\nRESTAURANT LAYOUT");
        Console.WriteLine("=================");
        Console.WriteLine(@$"
┌────┐    ┌────┐         ┌────┐
│ 3A │    │ 1A │         │ 2A │
│(2) │    │(2) │         │(2) │
└────┘    └────┘         └────┘

┌────┐                   ┌────┐
│ 6A │                   │ 4A │
│(2) │                   │(4) │
└────┘                   └────┘

┌────┐                   ┌────┐
│ 5A │                   │ 7A │
│(4) │                   │(2) │
└────┘                   └────┘

┌────┐                   ┌────┐
│ 2B │                   │ 3B │
│(4) │                   │(4) │
└────┘                   └────┘

┌────┐                   ┌────┐
│ 5B │                   │ 4B │
│(4) │                   │(2) │
└────┘                   └────┘

┌────┐       ┌────┐      ┌────┐
│ 6B │       │ 1C │      │ 1B │
│(2) │       │(6) │      │(6) │
└────┘       └────┘      └────┘
");
    }
}