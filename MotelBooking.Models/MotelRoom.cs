using System;

namespace MotelBooking.Models
{
    public class MotelRoom
    {
        public int RoomNum;
        public int Floor;
        public int NumBeds;

        public MotelRoom(int roomNum, int floor, int numBeds)
        {
            this.RoomNum = roomNum;
            this.Floor = floor;
            this.NumBeds = numBeds;
        }

        public bool IsHandicapAccessible()
        {
            return this.Floor == 0;
        }
    }
}
