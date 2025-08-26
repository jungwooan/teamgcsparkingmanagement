using ParkingManagement.Models;

namespace ParkingManagement.Services
{
    public interface IDataService
    {
        // User operations
        Task<List<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByKakaoIdAsync(string kakaoId);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);

        // Parking operations
        Task<List<ParkingUsage>> GetParkingUsagesAsync();
        Task<List<ParkingUsage>> GetParkingUsagesByDateAsync(DateTime date);
        Task<List<ParkingUsage>> GetParkingUsagesByUserAsync(int userId);
        Task<ParkingUsage> CreateParkingUsageAsync(ParkingUsage usage);
        Task<ParkingUsage> AddParkingUsageAsync(ParkingUsage usage);
        Task<ParkingUsage> UpdateParkingUsageAsync(ParkingUsage usage);
        Task<bool> DeleteParkingUsageAsync(int id);

        Task<List<ParkingReturn>> GetParkingReturnsAsync();
        Task<List<ParkingReturn>> GetParkingReturnsByDateAsync(DateTime date);
        Task<List<ParkingReturn>> GetParkingReturnsByUserAsync(int userId);
        Task<ParkingReturn> CreateParkingReturnAsync(ParkingReturn returnRecord);
        Task<ParkingReturn> AddParkingReturnAsync(ParkingReturn returnRecord);
        Task<ParkingReturn> UpdateParkingReturnAsync(ParkingReturn returnRecord);
        Task<bool> DeleteParkingReturnAsync(int id);

        Task<List<ExternalParking>> GetExternalParkingsAsync();
        Task<List<ExternalParking>> GetExternalParkingsByDateAsync(DateTime date);
        Task<List<ExternalParking>> GetExternalParkingsByUserAsync(int userId);
        Task<ExternalParking> CreateExternalParkingAsync(ExternalParking parking);
        Task<ExternalParking> AddExternalParkingAsync(ExternalParking parking);
        Task<ExternalParking> UpdateExternalParkingAsync(ExternalParking parking);
        Task<bool> DeleteExternalParkingAsync(int id);

        // Statistics
        Task<object> GetUserStatisticsAsync(int userId, DateTime startDate, DateTime endDate);
        Task<object> GetMonthlyStatisticsAsync(DateTime month);
    }
}
