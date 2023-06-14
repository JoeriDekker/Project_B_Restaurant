namespace Project_B_Restaurant.Test
{
    [TestClass]
    public class AccountsLogicTests : IUnitTest
    {
        private AccountsLogic _accountsLogic;
        private string accountsDataPath;

        [TestInitialize]
        public void Initialize()
        {
            // Create a new instance of AccountsLogic before each test
            _accountsLogic = new AccountsLogic();

            // Set up the accounts data path for testing
            accountsDataPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));

            // Create a backup of the original accounts data file
            File.Copy(accountsDataPath, accountsDataPath + ".bak", true);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Delete the accounts data file created during the test
            File.Delete(accountsDataPath);

            // Restore the original accounts data file
            File.Copy(accountsDataPath + ".bak", accountsDataPath, true);

            // Delete the accounts data file backup
            File.Delete(accountsDataPath + ".bak");
        }

        [TestMethod]
        public void LoginTest()
        {
            // Arrange
            string email = "test@example.com";
            string password = "invalidpassword";

            // Act
            AccountModel account = _accountsLogic.CheckLogin(email, password);

            // Assert
            Assert.IsNull(account);
        }

        [TestMethod]
        public void AddNewAccountTest()
        {
            // Arrange
            AccountModel account = new AccountModel(5, "new@example.com", "newpassword", "New User", AccountLevel.Customer);

            // Act
            _accountsLogic.UpdateList(account);

            // Assert
            AccountModel retrievedAccount = _accountsLogic.GetById(5);
            Assert.IsNotNull(retrievedAccount);
            Assert.AreEqual(account.Id, retrievedAccount.Id);
        }

        [TestMethod]
        public void UpdateUserName()
        {
            // Arrange
            AccountModel existingAccount = _accountsLogic.GetById(1);
            string newFullName = "Updated User";
            existingAccount.FullName = newFullName;

            // Act
            _accountsLogic.UpdateList(existingAccount);

            // Assert
            AccountModel updatedAccount = _accountsLogic.GetById(1);
            Assert.IsNotNull(updatedAccount);
            Assert.AreEqual(newFullName, updatedAccount.FullName);
        }
    }
}