using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public class VehiclePhotoesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;

        public VehiclePhotoesController(DataContext context, IBlobHelper blobHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
        }

        [HttpPost]
        public async Task<ActionResult<VehiclePhoto>> PostVehiclePhoto(VehiclePhotoRequest request)
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

            Guid imageId = await _blobHelper.UploadBlobAsync(request.Image, "vehiclephotos");
            VehiclePhoto vehiclePhoto = new()
            {
                ImageId = imageId,
                Vehicle = vehicle
            };

            _context.VehiclePhotos.Add(vehiclePhoto);
            await _context.SaveChangesAsync();
            return Ok(vehicle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiclePhoto(int id)
        {
            VehiclePhoto vehiclePhoto = await _context.VehiclePhotos.FindAsync(id);
            if (vehiclePhoto == null)
            {
                return NotFound();
            }

            await _blobHelper.DeleteBlobAsync(vehiclePhoto.ImageId, "vehiclephotos");
            _context.VehiclePhotos.Remove(vehiclePhoto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
