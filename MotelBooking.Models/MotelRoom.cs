using MotelBooking.DataAccess;
using System;

namespace MotelBooking.Models
{
    public class MotelRoom
    {
        public int RoomNum { get; set; }
        public int Floor { get; set; }
        public int NumBeds { get; set; }
        public int NumPets { get; set; }
        public float TotalCost { get; set; }

        private const int MAX_PET_COUNT = 2;

        public MotelRoom(int roomNum, int floor, int numBeds, int numPets = 0)
        {
            this.RoomNum = roomNum;
            this.Floor = floor;
            this.NumBeds = numBeds;
            this.NumPets = numPets;
        }

        public void AddPets(int numPets)
        {
            //if the number of pets being added is to much, return false
            if ((this.NumPets + numPets) > MAX_PET_COUNT)
                throw new RoomBookingException($"Room {this.RoomNum} currently has {this.NumPets} pets booked. Adding {numPets} pets would exceed the {MAX_PET_COUNT} limit");
            else
                this.NumPets += numPets;
        }

        public bool IsHandicapAccessible()
        {
            return this.Floor == 1;
        }

        public bool AllowsPets()
        {
            return this.Floor == 1;
        }
    }
}
