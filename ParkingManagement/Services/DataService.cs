using ParkingManagement.Models;

namespace ParkingManagement.Services
{
    public class DataService : IDataService
    {
        private readonly ILocalStorageService _localStorage;
        private int _nextUserId = 1;
        private int _nextParkingUsageId = 1;
        private int _nextParkingReturnId = 1;
        private int _nextExternalParkingId = 1;

        public DataService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            InitializeSampleDataAsync().ConfigureAwait(false);
        }

        private async Task InitializeSampleDataAsync()
        {
            var users = await _localStorage.GetItemAsync<List<User>>("users");
            if (users == null || !users.Any())
            {
                var sampleUsers = new List<User>
                {
                    new User { Id = 1, Name = "김철수", Department = "개발팀", Email = "kim@example.com", CreatedAt = DateTime.UtcNow },
                    new User { Id = 2, Name = "이영희", Department = "인사팀", Email = "lee@example.com", CreatedAt = DateTime.UtcNow },
                    new User { Id = 3, Name = "박민수", Department = "영업팀", Email = "park@example.com", CreatedAt = DateTime.UtcNow }
                };
                await _localStorage.SetItemAsync("users", sampleUsers);
                _nextUserId = 4;
            }

            // Add sample parking data
            var parkingUsages = await _localStorage.GetItemAsync<List<ParkingUsage>>("parkingUsages");
            if (parkingUsages == null || !parkingUsages.Any())
            {
                var sampleUsages = new List<ParkingUsage>
                {
                    new ParkingUsage { Id = 1, UserId = 1, Date = DateTime.Today, UsedAt = DateTime.Today.AddHours(9), CreatedAt = DateTime.UtcNow },
                    new ParkingUsage { Id = 2, UserId = 2, Date = DateTime.Today.AddDays(-1), UsedAt = DateTime.Today.AddDays(-1).AddHours(8), CreatedAt = DateTime.UtcNow },
                    new ParkingUsage { Id = 3, UserId = 1, Date = DateTime.Today.AddDays(-2), UsedAt = DateTime.Today.AddDays(-2).AddHours(9), CreatedAt = DateTime.UtcNow }
                };
                await _localStorage.SetItemAsync("parkingUsages", sampleUsages);
                _nextParkingUsageId = 4;
            }

            var parkingReturns = await _localStorage.GetItemAsync<List<ParkingReturn>>("parkingReturns");
            if (parkingReturns == null || !parkingReturns.Any())
            {
                var sampleReturns = new List<ParkingReturn>
                {
                    new ParkingReturn { Id = 1, UserId = 1, Date = DateTime.Today, ReturnedAt = DateTime.Today.AddHours(18), CreatedAt = DateTime.UtcNow },
                    new ParkingReturn { Id = 2, UserId = 2, Date = DateTime.Today.AddDays(-1), ReturnedAt = DateTime.Today.AddDays(-1).AddHours(17), CreatedAt = DateTime.UtcNow }
                };
                await _localStorage.SetItemAsync("parkingReturns", sampleReturns);
                _nextParkingReturnId = 3;
            }

            var externalParkings = await _localStorage.GetItemAsync<List<ExternalParking>>("externalParkings");
            if (externalParkings == null || !externalParkings.Any())
            {
                var sampleExternalParkings = new List<ExternalParking>
                {
                    new ExternalParking { Id = 1, UserId = 3, Date = DateTime.Today, Location = "롯데마트 주차장", Cost = 5000, CreatedAt = DateTime.UtcNow },
                    new ExternalParking { Id = 2, UserId = 1, Date = DateTime.Today.AddDays(-1), Location = "홈플러스 주차장", Cost = 3000, CreatedAt = DateTime.UtcNow }
                };
                await _localStorage.SetItemAsync("externalParkings", sampleExternalParkings);
                _nextExternalParkingId = 3;
            }
        }

        // User operations
        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _localStorage.GetItemAsync<List<User>>("users") ?? new List<User>();
            return users.Where(u => u.IsActive).ToList();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var users = await GetUsersAsync();
            return users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var users = await GetUsersAsync();
            return users.FirstOrDefault(u => u.Email == email);
        }

        public async Task<User?> GetUserByKakaoIdAsync(string kakaoId)
        {
            var users = await GetUsersAsync();
            return users.FirstOrDefault(u => u.KakaoId == kakaoId);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var users = await _localStorage.GetItemAsync<List<User>>("users") ?? new List<User>();
            user.Id = _nextUserId++;
            user.CreatedAt = DateTime.UtcNow;
            users.Add(user);
            await _localStorage.SetItemAsync("users", users);
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var users = await _localStorage.GetItemAsync<List<User>>("users") ?? new List<User>();
            var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                user.UpdatedAt = DateTime.UtcNow;
                var index = users.IndexOf(existingUser);
                users[index] = user;
                await _localStorage.SetItemAsync("users", users);
            }
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var users = await _localStorage.GetItemAsync<List<User>>("users") ?? new List<User>();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsActive = false;
                await _localStorage.SetItemAsync("users", users);
                return true;
            }
            return false;
        }

        // Parking Usage operations
        public async Task<List<ParkingUsage>> GetParkingUsagesAsync()
        {
            return await _localStorage.GetItemAsync<List<ParkingUsage>>("parkingUsages") ?? new List<ParkingUsage>();
        }

        public async Task<List<ParkingUsage>> GetParkingUsagesByDateAsync(DateTime date)
        {
            var usages = await GetParkingUsagesAsync();
            return usages.Where(u => u.Date.Date == date.Date).ToList();
        }

        public async Task<List<ParkingUsage>> GetParkingUsagesByUserAsync(int userId)
        {
            var usages = await GetParkingUsagesAsync();
            return usages.Where(u => u.UserId == userId).OrderByDescending(u => u.Date).ToList();
        }

        public async Task<ParkingUsage> CreateParkingUsageAsync(ParkingUsage usage)
        {
            var usages = await GetParkingUsagesAsync();
            usage.Id = _nextParkingUsageId++;
            usage.CreatedAt = DateTime.UtcNow;
            usages.Add(usage);
            await _localStorage.SetItemAsync("parkingUsages", usages);
            return usage;
        }

        public async Task<ParkingUsage> AddParkingUsageAsync(ParkingUsage usage)
        {
            return await CreateParkingUsageAsync(usage);
        }

        public async Task<ParkingUsage> UpdateParkingUsageAsync(ParkingUsage usage)
        {
            var usages = await GetParkingUsagesAsync();
            var existingUsage = usages.FirstOrDefault(u => u.Id == usage.Id);
            if (existingUsage != null)
            {
                usage.UpdatedAt = DateTime.UtcNow;
                var index = usages.IndexOf(existingUsage);
                usages[index] = usage;
                await _localStorage.SetItemAsync("parkingUsages", usages);
            }
            return usage;
        }

        public async Task<bool> DeleteParkingUsageAsync(int id)
        {
            var usages = await GetParkingUsagesAsync();
            var usage = usages.FirstOrDefault(u => u.Id == id);
            if (usage != null)
            {
                usages.Remove(usage);
                await _localStorage.SetItemAsync("parkingUsages", usages);
                return true;
            }
            return false;
        }

        // Parking Return operations
        public async Task<List<ParkingReturn>> GetParkingReturnsAsync()
        {
            return await _localStorage.GetItemAsync<List<ParkingReturn>>("parkingReturns") ?? new List<ParkingReturn>();
        }

        public async Task<List<ParkingReturn>> GetParkingReturnsByDateAsync(DateTime date)
        {
            var returns = await GetParkingReturnsAsync();
            return returns.Where(r => r.Date.Date == date.Date).ToList();
        }

        public async Task<List<ParkingReturn>> GetParkingReturnsByUserAsync(int userId)
        {
            var returns = await GetParkingReturnsAsync();
            return returns.Where(r => r.UserId == userId).OrderByDescending(r => r.Date).ToList();
        }

        public async Task<ParkingReturn> CreateParkingReturnAsync(ParkingReturn returnRecord)
        {
            var returns = await GetParkingReturnsAsync();
            returnRecord.Id = _nextParkingReturnId++;
            returnRecord.CreatedAt = DateTime.UtcNow;
            returns.Add(returnRecord);
            await _localStorage.SetItemAsync("parkingReturns", returns);
            return returnRecord;
        }

        public async Task<ParkingReturn> AddParkingReturnAsync(ParkingReturn returnRecord)
        {
            return await CreateParkingReturnAsync(returnRecord);
        }

        public async Task<ParkingReturn> UpdateParkingReturnAsync(ParkingReturn returnRecord)
        {
            var returns = await GetParkingReturnsAsync();
            var existingReturn = returns.FirstOrDefault(r => r.Id == returnRecord.Id);
            if (existingReturn != null)
            {
                returnRecord.UpdatedAt = DateTime.UtcNow;
                var index = returns.IndexOf(existingReturn);
                returns[index] = returnRecord;
                await _localStorage.SetItemAsync("parkingReturns", returns);
            }
            return returnRecord;
        }

        public async Task<bool> DeleteParkingReturnAsync(int id)
        {
            var returns = await GetParkingReturnsAsync();
            var returnRecord = returns.FirstOrDefault(r => r.Id == id);
            if (returnRecord != null)
            {
                returns.Remove(returnRecord);
                await _localStorage.SetItemAsync("parkingReturns", returns);
                return true;
            }
            return false;
        }

        // External Parking operations
        public async Task<List<ExternalParking>> GetExternalParkingsAsync()
        {
            return await _localStorage.GetItemAsync<List<ExternalParking>>("externalParkings") ?? new List<ExternalParking>();
        }

        public async Task<List<ExternalParking>> GetExternalParkingsByDateAsync(DateTime date)
        {
            var parkings = await GetExternalParkingsAsync();
            return parkings.Where(p => p.Date.Date == date.Date).ToList();
        }

        public async Task<List<ExternalParking>> GetExternalParkingsByUserAsync(int userId)
        {
            var parkings = await GetExternalParkingsAsync();
            return parkings.Where(p => p.UserId == userId).OrderByDescending(p => p.Date).ToList();
        }

        public async Task<ExternalParking> CreateExternalParkingAsync(ExternalParking parking)
        {
            var parkings = await GetExternalParkingsAsync();
            parking.Id = _nextExternalParkingId++;
            parking.CreatedAt = DateTime.UtcNow;
            parkings.Add(parking);
            await _localStorage.SetItemAsync("externalParkings", parkings);
            return parking;
        }

        public async Task<ExternalParking> AddExternalParkingAsync(ExternalParking parking)
        {
            return await CreateExternalParkingAsync(parking);
        }

        public async Task<ExternalParking> UpdateExternalParkingAsync(ExternalParking parking)
        {
            var parkings = await GetExternalParkingsAsync();
            var existingParking = parkings.FirstOrDefault(p => p.Id == parking.Id);
            if (existingParking != null)
            {
                parking.UpdatedAt = DateTime.UtcNow;
                var index = parkings.IndexOf(existingParking);
                parkings[index] = parking;
                await _localStorage.SetItemAsync("externalParkings", parkings);
            }
            return parking;
        }

        public async Task<bool> DeleteExternalParkingAsync(int id)
        {
            var parkings = await GetExternalParkingsAsync();
            var parking = parkings.FirstOrDefault(p => p.Id == id);
            if (parking != null)
            {
                parkings.Remove(parking);
                await _localStorage.SetItemAsync("externalParkings", parkings);
                return true;
            }
            return false;
        }

        // Statistics
        public async Task<object> GetUserStatisticsAsync(int userId, DateTime startDate, DateTime endDate)
        {
            var usages = await GetParkingUsagesByUserAsync(userId);
            var returns = await GetParkingReturnsByUserAsync(userId);
            var externalParkings = await GetExternalParkingsByUserAsync(userId);

            var filteredUsages = usages.Where(u => u.Date >= startDate && u.Date <= endDate).Count();
            var filteredReturns = returns.Where(r => r.Date >= startDate && r.Date <= endDate).Count();
            var filteredExternalParkings = externalParkings.Where(p => p.Date >= startDate && p.Date <= endDate).Count();
            var totalCost = externalParkings.Where(p => p.Date >= startDate && p.Date <= endDate && p.Cost.HasValue).Sum(p => p.Cost ?? 0);

            return new
            {
                ParkingUsages = filteredUsages,
                ParkingReturns = filteredReturns,
                ExternalParkings = filteredExternalParkings,
                TotalCost = totalCost
            };
        }

        public async Task<object> GetMonthlyStatisticsAsync(DateTime month)
        {
            var startDate = new DateTime(month.Year, month.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var usages = await GetParkingUsagesAsync();
            var returns = await GetParkingReturnsAsync();
            var externalParkings = await GetExternalParkingsAsync();
            var users = await GetUsersAsync();

            var filteredUsages = usages.Where(u => u.Date >= startDate && u.Date <= endDate).Count();
            var filteredReturns = returns.Where(r => r.Date >= startDate && r.Date <= endDate).Count();
            var filteredExternalParkings = externalParkings.Where(p => p.Date >= startDate && p.Date <= endDate).Count();
            var totalCost = externalParkings.Where(p => p.Date >= startDate && p.Date <= endDate && p.Cost.HasValue).Sum(p => p.Cost ?? 0);

            var userStats = users.Select(u => new
            {
                UserId = u.Id,
                UserName = u.Name,
                ParkingUsages = usages.Count(p => p.UserId == u.Id && p.Date >= startDate && p.Date <= endDate),
                ParkingReturns = returns.Count(p => p.UserId == u.Id && p.Date >= startDate && p.Date <= endDate),
                ExternalParkings = externalParkings.Count(p => p.UserId == u.Id && p.Date >= startDate && p.Date <= endDate),
                TotalCost = externalParkings.Where(p => p.UserId == u.Id && p.Date >= startDate && p.Date <= endDate && p.Cost.HasValue).Sum(p => p.Cost ?? 0)
            }).ToList();

            return new
            {
                Month = month.ToString("yyyy-MM"),
                TotalParkingUsages = filteredUsages,
                TotalParkingReturns = filteredReturns,
                TotalExternalParkings = filteredExternalParkings,
                TotalCost = totalCost,
                UserStatistics = userStats
            };
        }
    }
}
