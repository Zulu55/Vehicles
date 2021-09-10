using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Controllers.API
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProceduresController : ControllerBase
    {
        private readonly DataContext _context;

        public ProceduresController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Procedure>>> GetProcedures()
        {
            return await _context.Procedures.OrderBy(x => x.Description).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Procedure>> GetProcedure(int id)
        {
            Procedure procedure = await _context.Procedures.FindAsync(id);

            if (procedure == null)
            {
                return NotFound();
            }

            return procedure;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcedure(int id, Procedure procedure)
        {
            if (id != procedure.Id)
            {
                return BadRequest();
            }

            _context.Entry(procedure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe este procedimiento.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Procedure>> PostProcedure(Procedure procedure)
        {
            _context.Procedures.Add(procedure);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProcedure", new { id = procedure.Id }, procedure);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe este procedimiento.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcedure(int id)
        {
            Procedure procedure = await _context.Procedures.FindAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }

            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
