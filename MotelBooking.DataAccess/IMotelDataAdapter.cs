using MotelBooking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotelBooking.DataAccess
{
    public interface IMotelDataAdapter
    {
        Task<List<MotelRoom>> GetRoomsAvailableAsync();
        Task<List<MotelRoom>> GetAllRoomsAsync();
    }
}