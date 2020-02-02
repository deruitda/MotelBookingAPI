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

        public async Task<MotelRoom> FindRoomByNumber(int roomNum)
        {
            MotelRoom room = null;

            await Task.Run(() => room = _roomsAvailable.FirstOrDefault(r => r.RoomNum == roomNum));

            return room;
        }

        public async Task<MotelRoom> FindRoomByProperties(int numBeds, int numPets, bool needsAccessibility)
        {
            MotelRoom room = null;

            //fake some async operation like searching a DB of rooms available
            await Task.Run(() =>
            {
                //if they are not in need of special accomodations, check the second floor first so that we can save
                //rooms on the first floor for those who need them
                if(numPets == 0 && !needsAccessibility)
                {
                    //search for a free room on the second floor with the appropriate number of beds
                    room = _roomsAvailable.FirstOrDefault(r =>
                                                            r.Floor == 2 
                                                            && r.NumBeds == numBeds                                        
                                                          );
                }

                //if room  is still empty at this point, they either need special accomodations or we coulnd't find a room
                //on the second floor 
                if(room == null)
                {
                    //search for a room that has the number of beds requred, allows pets, and is accessible if needed
                    room = _roomsAvailable.FirstOrDefault(r =>
                                                            r.NumBeds == numBeds
                                                            && (numPets > 0 && r.AllowsPets() || numPets == 0)
                                                            && (needsAccessibility && r.IsHandicapAccessible() || !needsAccessibility)
                                                          );
                }
            });

            return room;
        }

        public void AddBookedRoom(MotelRoom room)
        {
            _roomsBooked.Add(room);
            _roomsAvailable.Remove(room);
        }
    }
}
