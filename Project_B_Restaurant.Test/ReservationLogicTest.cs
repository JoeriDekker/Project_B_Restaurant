namespace Project_B_Restaurant.Test
{
    [TestClass]
    public class ReservationLogicTests
    {
        [TestMethod]
        public void CreateReservation_ValidInput_ReturnsReservationModel()
        {
            // Arrange
            ReservationLogic reservationLogic = new ReservationLogic();
            string contact = "John Doe";
            int partySize = 4;
            Dictionary<DateTime, List<TableModel>> availableTimes = new Dictionary<DateTime, List<TableModel>>();
            DateTime todayWithoutTime = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            List<Dish> preOrders = new List<Dish>();

            // Act
            ReservationModel reservation = reservationLogic.CreateReservation(contact, partySize, availableTimes, todayWithoutTime, preOrders);

            // Assert
            Assert.IsNotNull(reservation);
            Assert.AreEqual(contact, reservation.Contact);
            Assert.AreEqual(partySize, reservation.P_Amount);
            Assert.AreEqual(todayWithoutTime, reservation.R_Date);
        }

        [TestMethod]
        public void GetAvailableTimesToReserve_ValidInput_ReturnsDictionaryWithAvailableTimes()
        {
            // Arrange
            ReservationLogic reservationLogic = new ReservationLogic();
            DateTime date = DateTime.Now;
            int partySize = 4;

            // Act
            Dictionary<DateTime, List<TableModel>> availableTimes = reservationLogic.GetAvailableTimesToReserve(date, partySize);

            // Assert
            Assert.IsNotNull(availableTimes);
            // Perform additional assertions based on expected behavior
        }

        [TestMethod]
        public void DeleteReservationByID_ExistingReservation_ReturnsTrue()
        {
            // Arrange
            ReservationLogic reservationLogic = new ReservationLogic();
            string reservationId = "R123";

            // Act
            bool result = reservationLogic.DeleteReservationByID(reservationId);

            // Assert
            Assert.IsTrue(result);
            // Verify that the reservation is deleted from the list or storage
        }

    }
}