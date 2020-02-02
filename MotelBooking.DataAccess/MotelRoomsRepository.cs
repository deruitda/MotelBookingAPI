using MotelBooking.Controllers;
using MotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotelBooking.DataAccess
{
    /// <summary>
    /// This class will be used as a fake database / repository to hold all our booking data
    /// </summary>
    public class MotelRoomsRepository : IMotelRoomsRepository
    {
        private List<MotelRoom> _allRooms;
        private List<MotelRoom> _roomsAvailable;
        private List<MotelRoom> _roomsBooked;

        public List<MotelRoom> AllRooms { get => _allRooms; }
        public List<MotelRoom> RoomsAvailable { get => _roomsAvailable; }
        public List<MotelRoom> RoomsBooked { get => _roomsBooked; }

        public MotelRoomsRepository()
        {
            Initialize();
        }

        private void Initialize()
        {
            _allRooms = _roomsAvailable = new List<MotelRoom>()
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

            _roomsBooked = new List<MotelRoom>();
        }

        /// <summary>
        /// Fake some async data being retrieved from a database
        /// </summary>
        /// <returns></returns>
        public async Task<List<MotelRoom>> GetListOfAllRoomsAsync()
        {
            return await Task.Run(() => _allRooms);
        }

        public async Task<List<MotelRoom>> GetListOfAvailableRoomsAsync()
        {
            return await Task.Run(() => _roomsAvailable);
        }        

        public void AddBookedRoom(MotelRoom room)
        {
            _roomsBooked.Add(room);
            _roomsAvailable.Remove(room);
        }

        public async Task RemoveReservation(int roomNum)
        {
            MotelRoom room = _roomsBooked.FirstOrDefault(r => r.RoomNum == roomNum);

            if (room == null)
                throw new ReservationRemovalException($"Room {roomNum} is not booked or does not exist");

            _roomsAvailable.Add(room);
            _roomsBooked.Remove(room);
        }
    }
}
