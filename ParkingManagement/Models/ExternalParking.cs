namespace ParkingManagement.Models
{
    public class ExternalParking : ParkingRecord
    {
        public string Location { get; set; } = string.Empty;
        
        public decimal? Cost { get; set; }
        
        public string? ParkingLotName { get; set; }
        
        public TimeSpan? Duration { get; set; }
    }
}
