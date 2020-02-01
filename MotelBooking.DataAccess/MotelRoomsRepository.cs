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
        private const float PRICE_ONE_BED = 50.00f;
        private const float PRICE_TWO_BED = 75.00f;
        private const float PRICE_THREE_BED = 90.00f;
        private const float PRICE_PER_PET = 20.00f;

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
                //search for a room that has the number of beds requred, allows pets, and is accessible if needed
                room = _roomsAvailable.FirstOrDefault(r =>
                                                        r.NumBeds == numBeds
                                                        && (numPets > 0 && r.AllowsPets() || numPets == 0)
                                                        && (needsAccessibility && r.IsHandicapAccessible() || !needsAccessibility)
                                                      );
            });

            return room;
        }

        public async Task<MotelRoom> BookRoomAsync(int roomNum, int numPets, bool needsAccessibility)
        {
            MotelRoom room = await FindRoomByNumber(roomNum);

            //if we get null here, the room is not available in the repository
            if (room == null)
                throw new RoomBookingException($"Room {roomNum} is not available for booking");

            if (needsAccessibility && !room.IsHandicapAccessible())
                throw new RoomBookingException($"Room {roomNum} is not handicap accessible");

            if (numPets > 0)
            {
                if (room.AllowsPets())
                    room.AddPets(numPets);
                else 
                    throw new RoomBookingException($"Room {roomNum} does now allow pets");
            }

            _roomsBooked.Add(room);
            _roomsAvailable.Remove(room);
            room.TotalCost = CalculateCost(room);
            return room;
        }

        /// <summary>
        /// Searches and books for any available room that fit the criteria specified
        /// </summary>
        /// <param name="numBeds"></param>
        /// <param name="numPets"></param>
        /// <param name="needsAccessibility"></param>
        /// <returns></returns>
        public async Task<MotelRoom> BookAvailableRoomAsync(int numBeds, int numPets, bool needsAccessibility)
        {
            MotelRoom room = await FindRoomByProperties(numBeds, numPets, needsAccessibility);

            //if we get null here, the room is not available in the repository
            if (room == null)
                throw new RoomBookingException($"Could not find available room that met the booking criteria");

            if (numPets > 0)
            {
                if (room.AllowsPets())
                    room.AddPets(numPets);
                else
                    throw new RoomBookingException($"Room {room.RoomNum} does now allow pets");
            }

            _roomsBooked.Add(room);
            _roomsAvailable.Remove(room);
            room.TotalCost = CalculateCost(room);
            return room;
        }


        private float CalculateCost(MotelRoom room)
        {
            float bedCost = GetCostForBeds(room.NumBeds); //calculate cost per bed
            float petCost = GetCostForPets(room.NumPets); //calculate cost per pet

            return bedCost + petCost;
        }

        private float GetCostForPets(int numPets)
        {
            return numPets * PRICE_PER_PET;
        }

        private float GetCostForBeds(int numBeds)
        {
            switch (numBeds)
            {
                case 1:
                    return PRICE_ONE_BED;
                case 2:
                    return PRICE_TWO_BED;
                case 3:
                    return PRICE_THREE_BED;
                default:
                    throw new RoomBookingException($"Number of beds: {numBeds}, does not have a price point set up");
            }
        }
    }
}
