using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotelBooking.DataAccess.Interfaces;
using MotelBooking.Models;

namespace MotelBooking.DataAccess
{
    public class MotelDataAdapter : IMotelDataAdapter
    {
        private IBookingProcessor _processor;
        private IMotelRoomsRepository _repo;
        public MotelDataAdapter(IBookingProcessor processor, IMotelRoomsRepository repo)
        {
            _processor = processor;
            _repo = repo;
        }

        public async Task<MotelRoom> BookRoomAsync(int roomNum, int numPets, bool needsAccessibility)
        {
            return await _processor.BookRoomAsync(roomNum, numPets, needsAccessibility);
        }

        public async Task<MotelRoom> BookAvailableRoomAsync(int numBeds, int numPets, bool needsAccessibility)
        {
            return await _processor.BookAvailableRoomAsync(numBeds, numPets, needsAccessibility);
        }

        public async Task<List<MotelRoom>> GetAllRoomsAsync()
        {
            return await _repo.GetListOfAllRoomsAsync();
        }

        public async Task<List<MotelRoom>> GetRoomsAvailableAsync()
        {
            return await _repo.GetListOfAvailableRoomsAsync();
        }

        public async Task RemoveReservationAsync(int roomNum)
        {
            await _repo.RemoveReservation(roomNum);
        }
    }
}
