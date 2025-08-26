using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Models
{
    public abstract class ParkingRecord
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        public string? Notes { get; set; }
    }
}
