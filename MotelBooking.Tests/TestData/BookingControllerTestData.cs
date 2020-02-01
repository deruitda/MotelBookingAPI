using MotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotelBooking.Tests.TestData
{
    internal static class BookingControllerTestData
    {
        internal static List<MotelRoom> GetListOfAllRooms()
        {
            List<MotelRoom> rooms = new List<MotelRoom>
            {
                //first floor test rooms
                new MotelRoom(100, 1, 1),
                new MotelRoom(101, 1, 1),
                new MotelRoom(102, 1, 2),
                new MotelRoom(103, 1, 2),
                new MotelRoom(104, 1, 3),
                new MotelRoom(104, 1, 3),

                new MotelRoom(200, 2, 1),
                new MotelRoom(201, 2, 1),
                new MotelRoom(202, 2, 2),
                new MotelRoom(203, 2, 2),
                new MotelRoom(204, 2, 3),
                new MotelRoom(205, 2, 3)
            };

            return rooms;
        }

        internal static List<MotelRoom> GetListOfNoRooms()
        {
            return new List<MotelRoom>();
        }

    }
}
