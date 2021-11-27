using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ParametersController : Controller
    {
        private readonly DataContext _context;

        public ParametersController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Edit()
        {
            Parameters parameters = await _context.Parameters.FirstOrDefaultAsync();
            if (parameters == null)
            {
                return NotFound();
            }

            return View(parameters);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Parameters parameters)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parameters);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), "Home");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(parameters);
        }

    }
}
