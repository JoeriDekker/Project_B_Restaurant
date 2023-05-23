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
        Console.SetIn(new StringReader($"{startLogin}\n{username}\n{password}"));
        openingUI.Start();

        
    }
}