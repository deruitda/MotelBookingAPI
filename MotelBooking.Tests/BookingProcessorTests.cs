using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MotelBooking.Controllers;
using MotelBooking.DataAccess;
using MotelBooking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotelBooking.Tests
{
    [TestClass]
    public class BookingProcessorTests
    {
        #region Check prices
        [TestMethod]
        public async Task One_Bed_Zero_Pets()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            BookingProcessor proc = new BookingProcessor(repo);

            MotelRoom room = await proc.BookAvailableRoomAsync(1, 0, false);

            Assert.AreEqual(room.TotalCost, 50);
        }

        [TestMethod]
        public async Task Two_Bed_Zero_Pets()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            BookingProcessor proc = new BookingProcessor(repo);

            MotelRoom room = await proc.BookAvailableRoomAsync(2, 0, false);

            Assert.AreEqual(room.TotalCost, 75);
        }

        [TestMethod]
        public async Task Three_Bed_Zero_Pets()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            BookingProcessor proc = new BookingProcessor(repo);

            MotelRoom room = await proc.BookAvailableRoomAsync(3, 0, false);

            Assert.AreEqual(room.TotalCost, 90);
        }

        public async Task One_Bed_One_Pets()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            BookingProcessor proc = new BookingProcessor(repo);

            MotelRoom room = await proc.BookAvailableRoomAsync(1, 1, false);

            Assert.AreEqual(room.TotalCost, 70);
        }

        [TestMethod]
        public async Task Two_Bed_One_Pets()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            BookingProcessor proc = new BookingProcessor(repo);

            MotelRoom room = await proc.BookAvailableRoomAsync(2, 1, false);

            Assert.AreEqual(room.TotalCost, 95);
        }

        [TestMethod]
        public async Task Three_Bed_One_Pets()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            BookingProcessor proc = new BookingProcessor(repo);

            MotelRoom room = await proc.BookAvailableRoomAsync(3, 1, false);

            Assert.AreEqual(room.TotalCost, 110);
        }

        public async Task One_Bed_Two_Pets()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            BookingProcessor proc = new BookingProcessor(repo);

            MotelRoom room = await proc.BookAvailableRoomAsync(1, 2, false);

            Assert.AreEqual(room.TotalCost, 90);
        }

        [TestMethod]
        public async Task Two_Bed_Two_Pets()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            BookingProcessor proc = new BookingProcessor(repo);

            MotelRoom room = await proc.BookAvailableRoomAsync(2, 2, false);

            Assert.AreEqual(room.TotalCost, 115);
        }

        [TestMethod]
        public async Task Three_Bed_Two_Pets()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            BookingProcessor proc = new BookingProcessor(repo);

            MotelRoom room = await proc.BookAvailableRoomAsync(3, 2, false);

            Assert.AreEqual(room.TotalCost, 130);
        }

        #endregion
    }

}
