namespace Project_B_Restaurant.Test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestLogin()
    {
        string startLogin = "1";
        string username = "admin";
        string password = "admin";

        OpeningUI openingUI = new(null!);
        Console.SetIn(new StringReader(startLogin));
        Console.SetIn(new StringReader(username));
        Console.SetIn(new StringReader(password));
        openingUI.Start();
    }
}