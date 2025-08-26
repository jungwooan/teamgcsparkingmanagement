namespace ParkingManagement.Models
{
    public class ParkingUsage : ParkingRecord
    {
        public DateTime UsedAt { get; set; } = DateTime.UtcNow;
        
        public string? Location { get; set; }
        
        public bool IsReturned { get; set; } = false;
    }
}
