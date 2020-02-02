using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotelBooking.DataAccess;
using MotelBooking.Models;

namespace MotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IMotelDataAdapter _dataAdapter;
        public BookingController(IMotelDataAdapter dataAdapter)
        {
            this._dataAdapter = dataAdapter;
        }
        // GET: api/Booking
        [HttpGet]
        [Route("GetRoomsAvailable")]
        public async Task<List<MotelRoom>> GetRoomsAvailable()
        {                        
            return await _dataAdapter.GetRoomsAvailableAsync();
        }

        // POST: api/Booking
        [HttpPost]
        [Route("BookRoomByNumber")]
        public async Task<IActionResult> BookRoomByNumber(int roomNum, int numPets, bool needsAccessibility)
        {
            try
            {
                MotelRoom room = await _dataAdapter.BookRoomAsync(roomNum, numPets, needsAccessibility);

                return Created("api/Booking/BookRoom", $"Successfully booked room {room.RoomNum}. Final cost is: {room.TotalCost}"); //return a 201 indicating the room was booked successfully
            }
            catch (RoomBookingException ex)
            {
                //just return 400 indicating there was an issue with the request they sent in
                return BadRequest(ex.BookingMessage);
            }
        }

        [HttpPost]
        [Route("BookRoomByCriteria")]
        public async Task<IActionResult> BookRoomByProperties(int numBeds, int numPets, bool needsAccessibility)
        {
            try
            {
                MotelRoom room = await _dataAdapter.BookAvailableRoomAsync(numBeds, numPets, needsAccessibility);

                return Created("api/Booking/BookRoom", $"Successfully booked room {room.RoomNum}. Final cost is: {room.TotalCost}"); //return a 201 indicating the room was booked successfully
            }
            catch (RoomBookingException ex)
            {
                //just return 400 indicating there was an issue with the request they sent in
                return BadRequest(ex.BookingMessage);
            }
        }

        [HttpDelete]
        [Route("RemoveReservation")]
        public async Task<IActionResult> RemoveReservation(int roomNum)
        {
            try
            {
                await _dataAdapter.RemoveReservationAsync(roomNum);

                return Ok($"Successfully removed reservation for room {roomNum}");
            }
            catch (ReservationRemovalException ex)
            {
                return BadRequest(ex.RemovalMessage);
            }
        }
    }
}
