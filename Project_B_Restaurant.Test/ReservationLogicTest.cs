using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project_B_Restaurant.Test
{
    [TestClass]
    public class ReservationLogicTests : IUnitTest
    {
        ReservationLogic reservationLogic = new();
        private static TableLogic tables = new();
        private static int totalPeopleInReservationWindow = 0;

        public void Initialize()
        {
            List<ReservationModel> reservations = new();
            ReservationAccess.WriteAll(reservations);
        }

        [TestCleanup]
        public void Cleanup()
        {
            List<ReservationModel> reservations = new();
            ReservationAccess.WriteAll(reservations);
        }

        [TestMethod]
        [DataRow("John Doe", 4)]
        [DataRow("John Doe", 5)]
        [DataRow("John Doe", 8)]
        [DataRow("John Doe", 3)]
        [DataRow("John Doe", 2)]
        [DataRow("John Doe", 2)]
        public void CreateReservationSuccesfully(string contact, int partySize)
        {
            // Arrange
            DateTime date = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 0, 0);
            Dictionary<DateTime, List<TableModel>> availableTimes =
                reservationLogic.GetAvailableTimesToReserve(date, partySize);
            List<Dish> preOrders = new List<Dish>();

            // Act
            DateTime chosenTime = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 0, 0);
            ReservationModel reservation =
                reservationLogic.CreateReservation(contact, partySize, availableTimes, chosenTime, preOrders);
            // Assert
            Assert.IsNotNull(reservation);
            Assert.IsTrue(reservationLogic.Reservation.Contains((reservation)));
            Assert.AreEqual(contact, reservation.Contact);
            Assert.AreEqual(partySize, reservation.P_Amount);
            Assert.AreEqual(chosenTime, reservation.R_Date);
        }

        [DataRow("John Doe")]
        [TestMethod]
        public void CreateReservationMaxSpots(string contact)
        {
            // Arrange
            DateTime date = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 0, 0);
            int partySize = tables.Tables.Sum(t => t.T_Seats);
            Dictionary<DateTime, List<TableModel>> availableTimes =
                reservationLogic.GetAvailableTimesToReserve(date, partySize);
            List<Dish> preOrders = new List<Dish>();

            // Act
            DateTime chosenTime = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 0, 0);
            ReservationModel reservation =
                reservationLogic.CreateReservation(contact, partySize, availableTimes, chosenTime, preOrders);
            // Assert
            Assert.IsNotNull(reservation);
            Assert.IsTrue(reservationLogic.Reservation.Contains((reservation)));
            Assert.AreEqual(contact, reservation.Contact);
            Assert.AreEqual(partySize, reservation.P_Amount);
            Assert.AreEqual(chosenTime, reservation.R_Date);
            Assert.IsFalse(availableTimes[chosenTime].Count == 0);
            Assert.IsFalse(availableTimes[chosenTime].Count == 0);
        }

        [DataRow("John Doe")]

        [DataRow("John Doe")]
        [TestMethod]
        public void CreateReservationNotEnoughSpots(string contact)
        {
            // Arrange
            DateTime date = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 0, 0);
            int partySize = tables.Tables.Sum(t => t.T_Seats) + 1;

            // Act
            Dictionary<DateTime, List<TableModel>> availableTimes =
                reservationLogic.GetAvailableTimesToReserve(date, partySize);
            DateTime chosenTime = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 0, 0);


            // Assert that there is nothing available for a partySize larger than amount of seats.
            Assert.IsTrue(availableTimes[chosenTime].Count == 0);


            // Assert that there is nothing available for a partySize larger than amount of seats.
            Assert.IsTrue(availableTimes[chosenTime].Count == 0);

        }

        [TestMethod]
        public void GetAvailableTimesToReserve_ValidInput_ReturnsDictionaryWithAvailableTimes()
        {
            // Arrange
            ReservationLogic reservationLogic = new ReservationLogic();
            DateTime date = DateTime.Now;
            int partySize = 4;

            // Act
            Dictionary<DateTime, List<TableModel>> availableTimes =
                reservationLogic.GetAvailableTimesToReserve(date, partySize);

            // Assert
            Assert.IsNotNull(availableTimes);
            // Perform additional assertions based on expected behavior
        }

        [DataRow("Jane Doe")]
        [TestMethod]
        public void DeleteReservationWithRCodeTest(string contact)
        {
            // Arrange
            DateTime date = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 0, 0);
            int partySize = tables.Tables.Sum(t => t.T_Seats);
            Dictionary<DateTime, List<TableModel>> availableTimes =
                reservationLogic.GetAvailableTimesToReserve(date, partySize);
            List<Dish> preOrders = new List<Dish>();

            // Act
            DateTime chosenTime = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 0, 0);
            ReservationModel reservation =
                reservationLogic.CreateReservation(contact, partySize, availableTimes, chosenTime, preOrders);
            bool result = reservationLogic.DeleteReservationByRCode(reservation.R_Code);
            // Assert
            Assert.IsTrue(result);
            // Verify that the reservation is deleted from the list or storage
        }
    }
}