using System.ComponentModel.DataAnnotations;

namespace EquipmentStatusAPI.Models
{
    public class EquipmentStatus
    {
        // Id is a integer surrogate primary key useful for efficiency
        public int Id { get; set; }

        // EquipmentId is a integer natural key which is more meaningful for business logic
        public required string EquipmentId { get; set; }

        // Status field to store each equipment current status
        public required string Status { get; set; }

        // store date of last Status update
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }

    }
}
