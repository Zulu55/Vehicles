using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;
using Vehicles.API.Helpers;
using Vehicles.API.Models.Request;

namespace Vehicles.API.Controllers.API
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class HistoriesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public HistoriesController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<History>> GetHistory(int id)
        {
            History history = await _context.Histories
                .Include(x => x.Details)
                .ThenInclude(x => x.Procedure)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (history == null)
            {
                return NotFound();
            }

            return history;
        }

        [HttpPost]
        public async Task<ActionResult<History>> PostHistory(HistoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Vehicle vehicle = await _context.Vehicles.FindAsync(request.VehicleId);
            if (vehicle == null)
            {
                return BadRequest("El vehículo no existe.");
            }

            string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                return BadRequest("El usuario no existe.");
            }

            History history = new()
            {
                Date = DateTime.UtcNow,
                Details = new List<Detail>(),
                Mileage = request.Mileage,
                Remarks = request.Remarks,
                User = user,
                Vehicle = vehicle,
            };

            _context.Histories.Add(history);
            await _context.SaveChangesAsync();
            return Ok(history);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistory(int id, HistoryRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            History history = await _context.Histories.FindAsync(request.Id);
            if (history == null)
            {
                return BadRequest("La historia no existe.");
            }

            history.Mileage = request.Mileage;
            history.Remarks = request.Remarks;

            _context.Histories.Update(history);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            History history = await _context.Histories
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (history == null)
            {
                return NotFound();
            }

            _context.Histories.Remove(history);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
