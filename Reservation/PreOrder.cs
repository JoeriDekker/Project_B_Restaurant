static class PreOrder
{
    public static void Start()
    {
        while (true)
        {
            Console.WriteLine("Do you want to pre order?");
            Console.WriteLine("yes");
            Console.WriteLine("no");
            string? answer = Console.ReadLine();
            if (answer == "yes")
            {
                while (true)
                {
                    Console.WriteLine("What would you like to pre order?");
                    Console.WriteLine("dishes");
                    Console.WriteLine("course menu");
                    Console.WriteLine("go back");
                    string? preOrder = Console.ReadLine();
                    if (preOrder == "dishes")
                    {
                        
                    }
                    else if (preOrder == "course menu")
                    {

                    }
                    else if (preOrder == "go back")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This is not available.");
                    }
                }
            }
            else if (answer == "no")
            {
            break;
            }
            else
            {
            Console.WriteLine("This is not available.");
            }
        }   
    }
    
}