using Microsoft.EntityFrameworkCore;
using EquipmentStatusAPI.Models;

namespace EquipmentStatusAPI.Data
{
    // Defining the DbContext for the EquipmentStatusAPI application.
    public class EquipmentStatusContext(DbContextOptions<EquipmentStatusContext> options) : DbContext(options)
    {
        // This DbSet property will manage EquipmentStatus model entities, allowing CRUD operations on them.
        public DbSet<EquipmentStatus> EquipmentStatuses { get; set; }
    }
}
