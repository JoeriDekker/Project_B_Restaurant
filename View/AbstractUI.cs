abstract class UI
{
    // Idea of design for an abstract UI. Check openingUI for example.
    public string[] MenuItems;

    public UI(string[] menuItems)
    {
        MenuItems = menuItems; 
    }

    public abstract void ShowMenu();

    public abstract string GetInput();
    
    public static string Header(string type)
    {
        // Makes use of ASCII font "Big", you can add more if necessary.
        switch (type)
        {
            case "reservation":
                return @"
_____                                _   _                 
|  __ \                              | | (_)                
| |__) |___  ___  ___ _ ____   ____ _| |_ _  ___  _ __  ___ 
|  _  // _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \/ __|
| | \ \  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | \__ \
|_|  \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|___/
============================================================                                          
";
            case "restaurant":
                return @"
  _____           _                              _   
 |  __ \         | |                            | |  
 | |__) |___  ___| |_ __ _ _   _ _ __ __ _ _ __ | |_ 
 |  _  // _ \/ __| __/ _` | | | | '__/ _` | '_ \| __|
 | | \ \  __/\__ \ || (_| | |_| | | | (_| | | | | |_ 
 |_|  \_\___||___/\__\__,_|\__,_|_|  \__,_|_| |_|\__|
 ====================================================
";
            case "menu":
                return @"
  __  __                  
 |  \/  |                 
 | \  / | ___ _ __  _   _ 
 | |\/| |/ _ \ '_ \| | | |
 | |  | |  __/ | | | |_| |
 |_|  |_|\___|_| |_|\__,_|
                          
==========================
";
            default:
                return @"
  _    _       _                              
 | |  | |     | |                             
 | |  | |_ __ | | ___ __   _____      ___ __  
 | |  | | '_ \| |/ / '_ \ / _ \ \ /\ / / '_ \ 
 | |__| | | | |   <| | | | (_) \ V  V /| | | |
  \____/|_| |_|_|\_\_| |_|\___/ \_/\_/ |_| |_|
==============================================                                              
";
        }
    }
}