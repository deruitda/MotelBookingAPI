using MotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MotelBooking.DataAccess.Interfaces
{
    public interface IBookingProcessor
    {
        Task<MotelRoom> BookRoomAsync(int roomNum, int numPets, bool needsAccessibility);
        Task<MotelRoom> BookAvailableRoomAsync(int numBeds, int numPets, bool needsAccessibility);
    }
}
