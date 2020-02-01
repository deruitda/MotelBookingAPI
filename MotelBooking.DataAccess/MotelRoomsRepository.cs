using MotelBooking.Models;
using System;
using System.Collections.Generic;
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
    }
}
