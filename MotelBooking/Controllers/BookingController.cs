﻿using System;
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
        public async Task<List<MotelRoom>> GetRoomsAvailable()
        {
            return await _dataAdapter.GetRoomsAvailableAsync();
        }

        // POST: api/Booking
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Booking/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}