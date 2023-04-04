static class UI
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        if (UserLogin.loggedIn == true)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter 1 to login", Console.ForegroundColor);
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.WriteLine("Enter 1 to login");
        }   
        Console.WriteLine("Enter 2 to See the menu {Still work in progress}");
        Console.WriteLine("Enter 3 Reservation");
        string input = Console.ReadLine();
        if (input == "1")
        {
            if (UserLogin.loggedIn == true)
            {
                Console.WriteLine("You are already logged in.");
                Start();
            }
            else
            {
                UserLogin.Start();
            }
        }
        else if (input == "2")
        {
            Menu menu = new Menu();
            menu.Start();
        }
        else if(input == "3"){
            ReservationModule.initReserve();
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }
}