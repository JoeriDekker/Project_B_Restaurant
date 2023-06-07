using Project_B_Restaurant;

namespace Project_B_Restaurant.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetAccountByID()
        {
            // Arrange
            AccountsLogic accController = new AccountsLogic();
            AccountModel actual = accController.GetById(2);

            int id = 2;
            string emailAddress = "test";
            string password = "test2";
            string fullName = "kip2";
            AccountLevel level = (AccountLevel)2;
            AccountModel expected = new AccountModel(id, emailAddress, password, fullName, level);

            // Act
            string actualInfo = actual.ShowInfo();
            string expectedInfo = expected.ShowInfo();

            // Assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.EmailAddress, actual.EmailAddress);
            Assert.AreEqual(expectedInfo, actualInfo);
        }
    }
}