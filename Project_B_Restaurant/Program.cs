public class program
{
    public static void Main()
    {
        MenuAccess.LoadMenuPreOder();
        OpeningUI opening = new(null);
        opening.Start();
    }
}
