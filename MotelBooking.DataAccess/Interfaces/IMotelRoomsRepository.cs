using MotelBooking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotelBooking.DataAccess
{
    public interface IMotelRoomsRepository
    {
        Task<List<MotelRoom>> GetListOfAllRoomsAsync();
        Task<List<MotelRoom>> GetListOfAvailableRoomsAsync();
        void AddBookedRoom(MotelRoom room);
        Task RemoveReservation(int roomNum);
    }
}