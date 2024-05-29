namespace EquipmentStatusAPI.Models
{
    public class EquipmentStatus
    {
        // Id is a integer surrogate primary key useful for efficiency
        public int Id { get; set; }
        // EquipmentId is a integer natural key which is more meaningful for business logic
        public string EquipmentId { get; set; }
        // field to store equipment current status
        public string Status { get; set; }

    }
}
