static class UI
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to See the menu {Still work in progress}");

        string input = Console.ReadLine();
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            // it is still all here bc i have to make an MVC format for the menu
            Menu menu = new Menu();
            menu.LoadMenu();

            while (true)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1: Show Menu");
                Console.WriteLine("2: Add a Dish");
                Console.WriteLine("3: Remove a Dish");
                Console.WriteLine("4: Change a Dish");
                Console.WriteLine("5: Save Menu");
                Console.WriteLine("6: Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        menu.ShowMenu();
                        break;
                    case 2:
                        menu.Add();
                        break;
                    case 3:
                        menu.Delete();
                        break;
                    case 4:
                        menu.Update();
                        break;
                    case 5:
                        menu.SaveMenu();
                        break;
                    case 6:
                        Console.WriteLine("Goodbye!");
                        Start();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                

                Console.WriteLine();

            }

            
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }
}