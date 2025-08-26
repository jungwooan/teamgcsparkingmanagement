namespace ParkingManagement.Models
{
    public class ParkingReturn : ParkingRecord
    {
        public DateTime ReturnedAt { get; set; } = DateTime.UtcNow;
        
        public int? RelatedUsageId { get; set; }
        public virtual ParkingUsage? RelatedUsage { get; set; }
        
        public string? ReturnLocation { get; set; }
    }
}
