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
        string input = string.Join(Environment.NewLine, startLogin, username, password);

        StringReader inputReader = new(input);
        Console.SetIn(inputReader);

        OpeningUI openingUI = new(null!);
        openingUI.Start();

        Console.OpenStandardInput();

        Assert.AreEqual(AccountsLogic.CurrentAccount.Level, AccountLevel.Admin);
    }
}