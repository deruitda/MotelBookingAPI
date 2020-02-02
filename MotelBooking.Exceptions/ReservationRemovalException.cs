using System;
using System.Runtime.Serialization;

namespace MotelBooking.Controllers
{
    [Serializable]
    public class ReservationRemovalException : Exception
    {
        public string RemovalMessage;
        public ReservationRemovalException(string msg)
        {
            RemovalMessage = msg;
        }
    }
}