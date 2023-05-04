public class program
{
    public static void Main()
    {
        MenuAccess.LoadMenu();
        OpeningUI opening = new(null);
        opening.Start();
    }
}
