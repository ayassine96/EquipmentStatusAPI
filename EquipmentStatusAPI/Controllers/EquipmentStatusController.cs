using Microsoft.AspNetCore.Mvc;
using EquipmentStatusAPI.Models;
using EquipmentStatusAPI.Data;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace EquipmentStatusAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentStatusController : ControllerBase
    {
        private readonly EquipmentStatusContext _context;

        public EquipmentStatusController(EquipmentStatusContext context) => _context = context;

        // POST /status
        [HttpPost("status")]
        public async Task<IActionResult> PostStatus([FromBody] EquipmentStatus equipmentStatus)
        {
            if (equipmentStatus == null || string.IsNullOrEmpty(equipmentStatus.EquipmentId))
            {
                return BadRequest("Invalid equipment status data.");
            }

            equipmentStatus.UpdateDate = DateTime.UtcNow;  // Set the current date and time as the update date
            _context.EquipmentStatuses.Add(equipmentStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStatus), new { equipmentId = equipmentStatus.EquipmentId }, equipmentStatus);
        }

        // GET /status/
        [HttpGet("status")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await _context.EquipmentStatuses
                .OrderByDescending(e => e.UpdateDate)  // Optional: Sort by the most recent updates
                .ToListAsync();

            if (statuses == null || !statuses.Any())
            {
                return NotFound("No equipment statuses found.");
            }

            return Ok(statuses);
        }

        // GET /status/{equipmentId}
        [HttpGet("status/{equipmentId}")]
        public async Task<IActionResult> GetStatus(string equipmentId)
        {
            var status = await _context.EquipmentStatuses
                .Where(e => e.EquipmentId == equipmentId)
                .OrderByDescending(e => e.UpdateDate) // Ensures the latest status is fetched
                .FirstOrDefaultAsync();

            if (status == null)
            {
                return NotFound($"Equipment with ID {equipmentId} not found.");
            }

            return Ok(status);
        }

        // PUT /status/{equipmentId}
        [HttpPut("status/{equipmentId}")]
        public async Task<IActionResult> UpdateStatus(string equipmentId, [FromBody] EquipmentStatus updateStatus)
        {
            var status = await _context.EquipmentStatuses
                .Where(e => e.EquipmentId == equipmentId)
                .FirstOrDefaultAsync();

            if (status == null)
            {
                return NotFound($"Equipment with ID {equipmentId} not found.");
            }

            status.Status = updateStatus.Status;
            status.UpdateDate = DateTime.UtcNow;  // Set the current date and time as the update date
            await _context.SaveChangesAsync();

            return Ok(status);
        }

        // DELETE /status/{equipmentId}
        [HttpDelete("status/{equipmentId}")]
        public async Task<IActionResult> DeleteStatus(string equipmentId)
        {
            var status = await _context.EquipmentStatuses
                .Where(e => e.EquipmentId == equipmentId)
                .FirstOrDefaultAsync();

            if (status == null)
            {
                return NotFound($"Equipment with ID {equipmentId} not found.");
            }

            _context.EquipmentStatuses.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent(); // Standard response for a successful delete operation
        }
    }
}



/*using Microsoft.AspNetCore.Mvc;
using EquipmentStatusAPI.Models;
using EquipmentStatusAPI.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentStatusAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentStatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly EquipmentStatusContext _context;

        public EquipmentStatusController(EquipmentStatusContext context)
        {
            _context = context;
        }
    }
}*/
