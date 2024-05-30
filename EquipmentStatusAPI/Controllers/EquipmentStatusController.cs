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

        /// <summary>
        /// Retrieves all equipment statuses, ordered by the most recent update.
        /// </summary>
        /// <returns>A list of all equipment statuses if found; otherwise, a NotFound result.</returns>
        [HttpGet("status")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await _context.EquipmentStatuses
                .OrderByDescending(e => e.UpdateDate)  // Sort by the most recent updates
                .ToListAsync();

            if (statuses == null || statuses.Count == 0)
            {
                return NotFound("No equipment statuses found.");
            }

            return Ok(statuses);
        }

        /// <summary>
        /// Retrieves the current status of specified equipment.
        /// </summary>
        /// <param name="equipmentId">The ID of the equipment to retrieve status for.</param>
        /// <returns>A list of status entries for the specified equipment or NotFound if none exist.</returns>
        [HttpGet("status/{equipmentId}")]
        public async Task<IActionResult> GetStatus(string equipmentId)
        {
            var status = await _context.EquipmentStatuses
                .Where(e => e.EquipmentId == equipmentId)
                .OrderByDescending(e => e.UpdateDate) // Sort by the most recent updates
                .ToListAsync();

            if (status == null || !status.Any())
            {
                return NotFound($"Equipment with ID {equipmentId} not found.");
            }

            return Ok(status);
        }

        /// <summary>
        /// Posts a new status for a piece of equipment.
        /// </summary>
        /// <param name="equipmentStatus">The status information to be added.</param>
        /// <returns>A CreatedAtActionResult with the newly created status entry.</returns>
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

        /// <summary>
        /// Updates the status of an existing piece of equipment.
        /// </summary>
        /// <param name="Id">The ID of the equipment status to update.</param>
        /// <param name="updateStatus">The new status information.</param>
        /// <returns>OK if the update is successful; NotFound if the specified status does not exist.</returns>
        [HttpPut("status/{Id}")]
        public async Task<IActionResult> UpdateStatus(int Id, [FromBody] EquipmentStatus updateStatus)
        {
            var status = await _context.EquipmentStatuses
                .Where(e => e.Id == Id)
                .FirstOrDefaultAsync();

            if (status == null)
            {
                return NotFound($"Equipment with ID {Id} not found.");
            }

            status.Status = updateStatus.Status;
            status.UpdateDate = DateTime.UtcNow;  // Set the current date and time as the update date
            await _context.SaveChangesAsync();

            return Ok(status);
        }

        /// <summary>
        /// Deletes the status of a specified piece of equipment.
        /// </summary>
        /// <param name="Id">The ID of the equipment status to delete.</param>
        /// <returns>NoContent if the deletion is successful; NotFound if the specified status does not exist.</returns>
        [HttpDelete("status/{Id}")]
        public async Task<IActionResult> DeleteStatus(int Id)
        {
            var status = await _context.EquipmentStatuses
                .Where(e => e.Id == Id)
                .FirstOrDefaultAsync();

            if (status == null)
            {
                return NotFound($"Equipment with ID {Id} not found.");
            }

            _context.EquipmentStatuses.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
