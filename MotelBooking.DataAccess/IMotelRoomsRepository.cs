using MotelBooking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotelBooking.DataAccess
{
    public interface IMotelRoomsRepository
    {
        Task<List<MotelRoom>> GetListOfAllRoomsAsync();
        Task<MotelRoom> FindRoomByNumber(int roomNum);
        Task<List<MotelRoom>> GetListOfAvailableRoomsAsync();
        Task<MotelRoom> BookRoomAsync(int roomNum, int numPets, bool needsAccessibility);
        Task<MotelRoom> BookAvailableRoomAsync(int numBeds, int numPets, bool needsAccessibility);
    }
}