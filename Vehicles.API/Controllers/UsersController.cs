using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.Common.Enums;

namespace Vehicles.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(x => x.DocumentType)
                .Include(x => x.Vehicles)
                .Where(x => x.UserType == UserType.User)
                .ToListAsync());
        }
    }
}
