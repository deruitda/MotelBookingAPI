using System;
using System.Collections.Generic;
using System.Text;

namespace MotelBooking.DataAccess
{
    public class RoomBookingException : Exception
    {
        public string BookingMessage;
        public RoomBookingException(string message)
        {
            BookingMessage = message;
        }
    }
}
