using Microsoft.EntityFrameworkCore;
using EquipmentStatusAPI.Models;

namespace EquipmentStatusAPI.Data
{
    // Defining the DbContext for the EquipmentStatusAPI application.
    public class EquipmentStatusContext : DbContext
    {
        // Add a parameterless constructor for Moq
        public EquipmentStatusContext() { }

        public EquipmentStatusContext(DbContextOptions<EquipmentStatusContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        // This DbSet property will manage EquipmentStatus model entities, allowing CRUD operations on them.
        public virtual DbSet<EquipmentStatus> EquipmentStatuses { get; set; }
    }
}
