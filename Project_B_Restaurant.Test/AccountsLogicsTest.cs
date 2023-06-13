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
        public void GetById_ValidId_ReturnsAccount()
        {
            // Arrange
            int accountId = 1;
            AccountModel newaccount = new AccountModel(1, "test@test.com", "test", "test", AccountLevel.Customer);

            // Act
            _accountsLogic.UpdateList(newaccount);
            AccountModel account = _accountsLogic.GetById(accountId);

            // Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(accountId, account.Id);
        }


        [TestMethod]
        public void GetById_InvalidId_ReturnsNull()
        {
            // Arrange
            int accountId = 999; // Assuming this ID does not exist

            // Act
            AccountModel account = _accountsLogic.GetById(accountId);

            // Assert
            Assert.IsNull(account);
        }

        [TestMethod]
        public void GetByEmail_ValidEmail_ReturnsAccount()
        {
            // Arrange
            string email = "test@test.com";

            // Act
            AccountModel account = _accountsLogic.GetByEmail(email);

            // Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(email, account.EmailAddress);
        }

        [TestMethod]
        public void GetByEmail_InvalidEmail_ReturnsNull()
        {
            // Arrange
            string email = "invalid@example.com";

            // Act
            AccountModel account = _accountsLogic.GetByEmail(email);

            // Assert
            Assert.IsNull(account);
        }

        [TestMethod]
        public void CheckLogin_InvalidCredentials_ReturnsNull()
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
        public void UpdateList_NewAccount_AddsAccountToList()
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
        public void UpdateList_ExistingAccount_UpdatesAccountInList()
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

        [TestMethod]
        public void AccountModel_ShowInfo_ReturnsFormattedString()
        {
            // Arrange
            AccountModel account = new AccountModel(1, "test@example.com", "password", "Test User", AccountLevel.Admin);

            // Act
            string info = account.ShowInfo();

            // Assert
            
            string expectedInfo = "FullName: Test User\nEmail: test@example.com\nLevel: Admin";
            Assert.AreEqual(expectedInfo, info);
        }
    }
}