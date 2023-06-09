namespace Project_B_Restaurant.Test
{
    [TestClass]
    public class ReservationLogicTests
    {
        private ReservationLogic _reservationLogic;

        [TestInitialize]
        public void Initialize()
        {
            _reservationLogic = new ReservationLogic();
        }

        [TestMethod]
        public void GetReservationByID_WithExistingID_ReturnsReservationModel()
        {
            // Arrange
            int id = 1;
            ReservationModel expectedReservation = new ReservationModel(id, "R001", "John Doe", new List<string>(), 4, new List<Dish>(), DateTime.Now);
            _reservationLogic.GetAllReservations().Add(expectedReservation);

            // Act
            ReservationModel? actualReservation = _reservationLogic.getReservationByID(id);

            // Assert
            Assert.IsNotNull(actualReservation);
            Assert.AreEqual(expectedReservation, actualReservation);
        }

        [TestMethod]
        public void GetReservationByID_WithNonExistingID_ReturnsNull()
        {
            // Arrange
            int id = 1;

            // Act
            ReservationModel? actualReservation = _reservationLogic.getReservationByID(id);

            // Assert
            Assert.IsNull(actualReservation);
        }

        [TestMethod]
        public void GetReservationByTableID_WithExistingTableID_ReturnsReservationModel()
        {
            // Arrange
            string tableID = "T001";
            ReservationModel expectedReservation = new ReservationModel(1, "R001", "John Doe", new List<string>() { tableID }, 4, new List<Dish>(), DateTime.Now);
            _reservationLogic.GetAllReservations().Add(expectedReservation);

            // Act
            ReservationModel? actualReservation = _reservationLogic.getReservationByTableID(tableID);

            // Assert
            Assert.IsNotNull(actualReservation);
            Assert.AreEqual(expectedReservation, actualReservation);
        }

        [TestMethod]
        public void GetReservationByTableID_WithNonExistingTableID_ReturnsNull()
        {
            // Arrange
            string tableID = "T001";

            // Act
            ReservationModel? actualReservation = _reservationLogic.getReservationByTableID(tableID);

            // Assert
            Assert.IsNull(actualReservation);
        }

        [TestMethod]
        public void DeleteReservationByID_WithExistingID_RemovesReservation()
        {
            // Arrange
            int id = 1;
            ReservationModel reservation = new ReservationModel(id, "R001", "John Doe", new List<string>(), 4, new List<Dish>(), DateTime.Now);
            _reservationLogic.GetAllReservations().Add(reservation);

            // Act
            bool result = _reservationLogic.DeleteReservationByID(id);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.DoesNotContain(_reservationLogic.GetAllReservations(), reservation);
        }

        [TestMethod]
        public void DeleteReservationByID_WithNonExistingID_ReturnsFalse()
        {
            // Arrange
            int id = 1;

            // Act
            bool result = _reservationLogic.DeleteReservationByID(id);

            // Assert
            Assert.IsFalse(result);
        }

    }
}