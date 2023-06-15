class UpdateInfoUI : UI
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
    public UpdateInfoUI(UI previousUI) : base(previousUI)
    {
    }
    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem("Change restaurant name"));
        MenuItems.Add(new MenuItem("Change address"));
        MenuItems.Add(new MenuItem("Change postal code"));
        MenuItems.Add(new MenuItem("Change city"));
        MenuItems.Add(new MenuItem("Change telephone number"));
        MenuItems.Add(new MenuItem("Change e-mailaddress"));
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Change restaurant name":
                Console.WriteLine("What would you like to be the new name?");
                string Name = Console.ReadLine() ?? string.Empty;
                infoLogic.Restaurant.Name = Name;
                infoLogic.UpdateInfo();
                Console.WriteLine("The restaurant name has been updated!");
                break;
            case "Change address":
                Console.WriteLine("What would you like to be the new address?");
                string Address = Console.ReadLine() ?? string.Empty;
                infoLogic.Restaurant.Address = Address;
                infoLogic.UpdateInfo();
                Console.WriteLine("The restaurant address has been updated!");
                break;
            case "Change postal code":
                Console.WriteLine("What would you like to be the new postal code?");
                string PostalCode = Console.ReadLine() ?? string.Empty;
                infoLogic.Restaurant.PostalCode = PostalCode;
                infoLogic.UpdateInfo();
                Console.WriteLine("The postal code has been updated!");
                break;
            case "Change city":
                Console.WriteLine("What would you like to be the new city?");
                string City = Console.ReadLine() ?? string.Empty;
                infoLogic.Restaurant.City = City;
                infoLogic.UpdateInfo();
                Console.WriteLine("The city has been updated!");
                break;
            case "Change telephone number":
                Console.WriteLine("What would you like to be the new telephone number?");
                string Telephone = Console.ReadLine() ?? string.Empty;
                infoLogic.Restaurant.Telephone = Telephone;
                infoLogic.UpdateInfo();
                Console.WriteLine("The telephone number has been updated!");
                break;
            case "Change e-mailaddress":
                Console.WriteLine("What would you like to be the new e-mailaddress?");
                string Email = Console.ReadLine() ?? string.Empty;
                infoLogic.Restaurant.EmailAddress = Email;
                infoLogic.UpdateInfo();
                Console.WriteLine("The e-mailaddress has been updated!");
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