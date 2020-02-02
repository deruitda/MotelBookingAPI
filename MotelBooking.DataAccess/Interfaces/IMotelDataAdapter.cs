using MotelBooking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotelBooking.DataAccess
{
    public interface IMotelDataAdapter
    {
        Task<List<MotelRoom>> GetRoomsAvailableAsync();
        Task<List<MotelRoom>> GetAllRoomsAsync();
        Task<MotelRoom> BookRoomAsync(int roomNum, int numPets, bool needsAccessibility);
        Task<MotelRoom> BookAvailableRoomAsync(int numBeds, int numPets, bool needsAccessibility);
        Task RemoveReservationAsync(int roomNum);
    }
}