using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MotelBooking.Controllers;
using MotelBooking.DataAccess;
using MotelBooking.Models;
using MotelBooking.Tests.TestData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotelBooking.Tests
{
    [TestClass]
    public class BookingControllerTests
    {
        [TestMethod]
        public async Task Rooms_Are_Available()
        {
            Mock<IMotelDataAdapter> mock = new Mock<IMotelDataAdapter>();
            mock.Setup(da => da.GetRoomsAvailableAsync()).ReturnsAsync(BookingControllerTestData.GetListOfAllRooms());

            BookingController controller = new BookingController(mock.Object);
            List<MotelRoom> rooms = await controller.GetRoomsAvailable();

            Assert.IsTrue(rooms.Count > 0);
        }

        [TestMethod]
        public async Task Rooms_Are_Not_Available()
        {
            Mock<IMotelDataAdapter> mock = new Mock<IMotelDataAdapter>();
            mock.Setup(da => da.GetRoomsAvailableAsync()).ReturnsAsync(BookingControllerTestData.GetListOfNoRooms());

            BookingController controller = new BookingController(mock.Object);
            List<MotelRoom> rooms = await controller.GetRoomsAvailable();

            Assert.IsTrue(rooms.Count == 0);
        }
    }
}
