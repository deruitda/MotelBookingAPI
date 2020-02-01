using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotelBooking.Models;

namespace MotelBooking.DataAccess
{
    public class MotelDataAdapter : IMotelDataAdapter
    {
        private IMotelRoomsRepository _repo;
        public MotelDataAdapter(IMotelRoomsRepository repo)
        {
            this._repo = repo;
        }

        public async Task<MotelRoom> BookRoomAsync(int roomNum, int numPets, bool needsAccessibility)
        {
            return await _repo.BookRoomAsync(roomNum, numPets, needsAccessibility);
        }

        public async Task<MotelRoom> BookAvailableRoomAsync(int numBeds, int numPets, bool needsAccessibility)
        {
            return await _repo.BookAvailableRoomAsync(numBeds, numPets, needsAccessibility);
        }

        public async Task<List<MotelRoom>> GetAllRoomsAsync()
        {
            return await _repo.GetListOfAllRoomsAsync();
        }

        public async Task<List<MotelRoom>> GetRoomsAvailableAsync()
        {
            return await _repo.GetListOfAvailableRoomsAsync();
        }
    }
}
