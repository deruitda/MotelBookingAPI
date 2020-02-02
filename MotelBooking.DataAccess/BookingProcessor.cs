using MotelBooking.DataAccess.Interfaces;
using MotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotelBooking.DataAccess
{
    public class BookingProcessor : IBookingProcessor
    {
        private readonly IMotelRoomsRepository _repo;
        private const float PRICE_ONE_BED = 50.00f;
        private const float PRICE_TWO_BED = 75.00f;
        private const float PRICE_THREE_BED = 90.00f;
        private const float PRICE_PER_PET = 20.00f;
        public BookingProcessor(IMotelRoomsRepository repo)
        {
            _repo = repo;
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

            _repo.AddBookedRoom(room);
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

            _repo.AddBookedRoom(room);
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

        public async Task<MotelRoom> FindRoomByNumber(int roomNum)
        {
            MotelRoom room = null;

            room = (await _repo.GetListOfAvailableRoomsAsync()).FirstOrDefault(r => r.RoomNum == roomNum);

            return room;
        }

        public async Task<MotelRoom> FindRoomByProperties(int numBeds, int numPets, bool needsAccessibility)
        {
            MotelRoom room = null;

            //if they are not in need of special accomodations, check the second floor first so that we can save
            //rooms on the first floor for those who need them
            if (numPets == 0 && !needsAccessibility)
            {
                //search for a free room on the second floor with the appropriate number of beds
                room = (await _repo.GetListOfAvailableRoomsAsync()).FirstOrDefault(r =>
                                                                                   r.Floor == 2
                                                                                   && r.NumBeds == numBeds
                                                                                   );
            }

            //if room  is still empty at this point, they either need special accomodations or we coulnd't find a room
            //on the second floor 
            if (room == null)
            {
                //search for a room that has the number of beds requred, allows pets, and is accessible if needed
                room = (await _repo.GetListOfAvailableRoomsAsync()).FirstOrDefault(r =>
                                                                                   r.NumBeds == numBeds
                                                                                   && (numPets > 0 && r.AllowsPets() || numPets == 0)
                                                                                   && (needsAccessibility && r.IsHandicapAccessible() || !needsAccessibility)
                                                                                   );
            }

            return room;
        }
    }
}
