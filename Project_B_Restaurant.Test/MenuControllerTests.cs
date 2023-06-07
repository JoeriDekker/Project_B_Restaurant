namespace Project_B_Restaurant.Test
{
    [TestClass]
    public class MenuControllerTests
    {
        private MenuController _menuController;
        private string menuPath;

        [TestInitialize]
        public void Initialize()
        {
            // Create a new instance of MenuController before each test
            _menuController = new MenuController();

            // Set up the menu path for testing
            menuPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/menu.json"));

            // Create a backup of the original menu file
            File.Copy(menuPath, menuPath + ".bak", true);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Delete the menu file created during the test
            File.Delete(menuPath);

            // Restore the original menu file
            File.Copy(menuPath + ".bak", menuPath, true);

            // Delete the menu file backup
            File.Delete(menuPath + ".bak");
        }

        [TestMethod]
        public void Add_AddsNewDishToMenu()
        {
            // Arrange
            string dishName = "Test Dish2";
            List<string> dishIngredients = new List<string> { "Ingredient 1", "Ingredient 2" };
            string dishAllergies = "Allergies";
            double dishPrice = 10.99;
            string dishType = "Main";

            // Act
            _menuController.Add(dishName, dishIngredients, dishAllergies, dishPrice, dishType);
            List<Dish> menuDishes = _menuController.Dishes;

            // Assert
            Assert.AreEqual(1, menuDishes.Count);
            Assert.AreEqual(dishName, menuDishes[0].Name);
            // Assert other properties as needed
        }
    
        [TestMethod]
        public void Delete_RemovesDishFromMenu_WhenDishIdExists()
        {
            _menuController.Save();
            // Arrange
            string dishName = "Test Dish";
            List<string> dishIngredients = new List<string> { "Ingredient 1", "Ingredient 2" };
            string dishAllergies = "Allergies";
            double dishPrice = 10.99;
            string dishType = "Main";
            Dish dishToRemove = new Dish(dishName, dishIngredients, dishAllergies, dishPrice, dishType);
            _menuController.Dishes.Add(dishToRemove);

            // Act
            bool result = _menuController.Delete(dishToRemove.ID);
            List<Dish> menuDishes = _menuController.Dishes;

            // Assert
            Assert.AreEqual(0, menuDishes.Count);
        }

        [TestMethod]
        public void Delete_ReturnsFalse_WhenDishIdDoesNotExist()
        {
            // Arrange
            int dishIdToDelete = 1;

            // Act
            bool result = _menuController.Delete(dishIdToDelete);
            List<Dish> menuDishes = _menuController.Dishes;

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, menuDishes.Count);
        }

        [TestMethod]
        public void FindDishByName_ReturnsTrue_WhenDishNameExists()
        {
            // Arrange
            Dish dish = new Dish
            {
                Name = "Test Dish",
                // Set other properties accordingly
            };
            _menuController.Dishes.Add(dish);

            // Act
            bool result = _menuController.FindDishByName(dish.Name);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FindDishByName_ReturnsFalse_WhenDishNameDoesNotExist()
        {
            // Arrange
            string dishName = "Test test Dish";

            // Act
            bool result = _menuController.FindDishByName(dishName);

            // Assert
            Assert.IsFalse(result);
        }

        // Add more unit tests for the remaining methods
    }
}