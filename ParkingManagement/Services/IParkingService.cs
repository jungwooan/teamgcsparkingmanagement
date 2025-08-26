using ParkingManagement.Models;

namespace ParkingManagement.Services
{
    public interface IParkingService
    {
        // Parking Usage
        Task<ParkingUsage> CreateParkingUsageAsync(ParkingUsage usage);
        Task<ParkingUsage?> GetParkingUsageByIdAsync(int id);
        Task<IEnumerable<ParkingUsage>> GetParkingUsagesByDateAsync(DateTime date);
        Task<IEnumerable<ParkingUsage>> GetParkingUsagesByUserAsync(int userId);
        Task<ParkingUsage> UpdateParkingUsageAsync(ParkingUsage usage);
        Task<bool> DeleteParkingUsageAsync(int id);

        // Parking Return
        Task<ParkingReturn> CreateParkingReturnAsync(ParkingReturn returnRecord);
        Task<ParkingReturn?> GetParkingReturnByIdAsync(int id);
        Task<IEnumerable<ParkingReturn>> GetParkingReturnsByDateAsync(DateTime date);
        Task<IEnumerable<ParkingReturn>> GetParkingReturnsByUserAsync(int userId);
        Task<ParkingReturn> UpdateParkingReturnAsync(ParkingReturn returnRecord);
        Task<bool> DeleteParkingReturnAsync(int id);

        // External Parking
        Task<ExternalParking> CreateExternalParkingAsync(ExternalParking parking);
        Task<ExternalParking?> GetExternalParkingByIdAsync(int id);
        Task<IEnumerable<ExternalParking>> GetExternalParkingsByDateAsync(DateTime date);
        Task<IEnumerable<ExternalParking>> GetExternalParkingsByUserAsync(int userId);
        Task<ExternalParking> UpdateExternalParkingAsync(ExternalParking parking);
        Task<bool> DeleteExternalParkingAsync(int id);

        // Statistics
        Task<object> GetUserStatisticsAsync(int userId, DateTime startDate, DateTime endDate);
        Task<object> GetMonthlyStatisticsAsync(DateTime month);
    }
}
