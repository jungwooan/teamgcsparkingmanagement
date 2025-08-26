using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Department { get; set; } = string.Empty;
        
        public string? Email { get; set; }
        
        public string? KakaoId { get; set; }
        
        public string? ProfileImageUrl { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public virtual ICollection<ParkingRecord> ParkingRecords { get; set; } = new List<ParkingRecord>();
        public virtual ICollection<ParkingUsage> ParkingUsages { get; set; } = new List<ParkingUsage>();
        public virtual ICollection<ParkingReturn> ParkingReturns { get; set; } = new List<ParkingReturn>();
        public virtual ICollection<ExternalParking> ExternalParkings { get; set; } = new List<ExternalParking>();
    }
}
