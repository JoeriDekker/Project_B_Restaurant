public static class Program
{
    public static void Main()
    {
        // Needed to print '€'
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        OpeningUI opening = new(null);
        opening.Start();
    }
}
