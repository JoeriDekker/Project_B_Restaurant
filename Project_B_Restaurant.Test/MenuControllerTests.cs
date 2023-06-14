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
        public void AddDishToMenu()
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
        public void DeleteDishFromMenu()
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
        public void UpdateDishTest()
        {
            // Arrange
            _menuController.Add("Dish 1", new List<string> { "Ingredient 1" }, "Allergies 1", 10.99, "Main");
            _menuController.Add("Dish 2", new List<string> { "Ingredient 2" }, "Allergies 2", 15.99, "Main");

            var dishToUpdate = new Dish
            {
                Name = "Changed",
                Ingredients = new List<string> { "Updated Ingredient 1", "Updated Ingredient 2" },
                Allergies = "Updated Allergies",
                Price = 12.99,
                Type = "Test",
                InStock = false,
                PreOrderAmount = 0,
                MaxAmountPreOrder = 5
            };

            // Act
            _menuController.Update(dishToUpdate, "Dish 1");

            // Assert
            var updatedDish = _menuController.GetDishByName("Changed");
            Assert.AreEqual("Changed", updatedDish.Name);
            CollectionAssert.AreEqual(
                new List<string> { "Updated Ingredient 1", "Updated Ingredient 2" },
                updatedDish.Ingredients);
            Assert.AreEqual("Updated Allergies", updatedDish.Allergies);
            Assert.AreEqual(12.99, updatedDish.Price);
            Assert.AreEqual("Test", updatedDish.Type);
            Assert.IsFalse(updatedDish.InStock);
            Assert.AreEqual(0, updatedDish.PreOrderAmount);
            Assert.AreEqual(5, updatedDish.MaxAmountPreOrder);
        }
    }
}