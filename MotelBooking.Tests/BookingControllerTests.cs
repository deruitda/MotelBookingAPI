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
    public class BookingControllerTests
    {
        [TestMethod]
        public async Task Rooms_Are_Available()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);
            List<MotelRoom> rooms = await controller.GetRoomsAvailable();

            Assert.IsTrue(rooms.Count > 0);
        }

        [TestMethod]
        public async Task Rooms_Are_Not_Available()
        {
            //mock out that tere is no data
            Mock<IMotelDataAdapter> mock = new Mock<IMotelDataAdapter>();
            mock.Setup(da => da.GetRoomsAvailableAsync()).ReturnsAsync(new List<MotelRoom>());

            BookingController controller = new BookingController(mock.Object);
            List<MotelRoom> rooms = await controller.GetRoomsAvailable();

            Assert.IsTrue(rooms.Count == 0);
        }
        #region Book by room number

        [TestMethod]
        public async Task Book_Room_By_Number_No_Pets_No_Accessability_Available()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);
            IActionResult a = await controller.BookRoomByNumber(101, 0, false);

            Assert.AreEqual(((ObjectResult)a).StatusCode, 201);
        }

        [TestMethod]
        public async Task Book_Room_By_Number_With_Pets_No_Accessability_Available()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);
            IActionResult a = await controller.BookRoomByNumber(101, 2, false);

            Assert.AreEqual(((ObjectResult)a).StatusCode, 201);
        }

        [TestMethod]
        public async Task Book_Room_By_Number_With_Pets_With_Accessability_Available()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);
            IActionResult a = await controller.BookRoomByNumber(101, 1, true);

            Assert.AreEqual(((ObjectResult)a).StatusCode, 201);
        }
        
        [TestMethod]
        public async Task Book_Room_By_Number_With_Pets_With_Accessability_Not_Available()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);

            //book the whole first floor
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 6; i++)
            {
                tasks.Add(controller.BookRoomByNumber(100 + i, 1, true));
            }

            //wait for all the bookings to finish
            await Task.WhenAll(tasks);

            IActionResult a = await controller.BookRoomByNumber(101, 1, true);

            Assert.AreEqual(((ObjectResult)a).StatusCode, 400);
        }

        [TestMethod]
        public async Task Book_Room_By_Number_With_Pets_With_Accessability_Room_Number_Not_Valid()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);
            IActionResult a = await controller.BookRoomByNumber(10000, 1, true);

            Assert.AreEqual(((ObjectResult)a).StatusCode, 400);
        }
        #endregion

        #region Book by room properties
        [TestMethod]
        public async Task Book_Room_By_Props_1_Bed_No_Pets_No_Accessability_Available()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);
            IActionResult a = await controller.BookRoomByProperties(1, 0, false);

            Assert.AreEqual(((ObjectResult)a).StatusCode, 201);
        }
        
        [TestMethod]
        public async Task Book_Room_By_Props_2_Bed_With_Pets_No_Accessability_Available()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);
            IActionResult a = await controller.BookRoomByProperties(2, 2, false);

            Assert.AreEqual(((ObjectResult)a).StatusCode, 201);
        }

        [TestMethod]
        public async Task Book_Room_By_Props_3_Bed_No_Pets_No_Accessability_Available()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);
            IActionResult a = await controller.BookRoomByProperties(2, 0, true);

            Assert.AreEqual(((ObjectResult)a).StatusCode, 201);
        }

        [TestMethod]
        public async Task Book_Room_By_Props_1_Bed_With_Pets_No_Accessability_Not_Available()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 6; i++)
            {
                //book the first floor entirely
                tasks.Add(controller.BookRoomByNumber(100 + i, 1, true));
            }

            //wait for all the bookings to finish
            await Task.WhenAll(tasks);

            IActionResult a = await controller.BookRoomByProperties(1, 1, false);

            Assert.AreEqual(((ObjectResult)a).StatusCode, 400);
        }
        #endregion

        #region Remove Reservations
        [TestMethod]
        public async Task Remove_Reservation_Booked()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);
            await controller.BookRoomByNumber(101, 0, false); //book room 101
            IActionResult a = await controller.RemoveReservation(101); //remove its reservation

            Assert.AreEqual(((ObjectResult)a).StatusCode, 200); //check for OK status
        }

        [TestMethod]
        public async Task Remove_Reservation_Not_Booked()
        {
            IMotelRoomsRepository repo = new MotelRoomsRepository();
            IMotelDataAdapter da = new MotelDataAdapter(new BookingProcessor(repo), repo);

            BookingController controller = new BookingController(da);
            IActionResult a = await controller.RemoveReservation(101); //remove reservation from room 101, ex will be thrown which will be caught by controller

            Assert.AreEqual(((ObjectResult)a).StatusCode, 400); //check for OK status
        }
        #endregion

        
    }

}
