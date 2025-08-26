using Microsoft.EntityFrameworkCore;
using ParkingManagement.Data;
using ParkingManagement.Models;

namespace ParkingManagement.Services
{
    public class ParkingService : IParkingService
    {
        private readonly ParkingDbContext _context;

        public ParkingService(ParkingDbContext context)
        {
            _context = context;
        }

        // Parking Usage methods
        public async Task<ParkingUsage> CreateParkingUsageAsync(ParkingUsage usage)
        {
            _context.ParkingUsages.Add(usage);
            await _context.SaveChangesAsync();
            return usage;
        }

        public async Task<ParkingUsage?> GetParkingUsageByIdAsync(int id)
        {
            return await _context.ParkingUsages
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ParkingUsage>> GetParkingUsagesByDateAsync(DateTime date)
        {
            return await _context.ParkingUsages
                .Include(p => p.User)
                .Where(p => p.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<ParkingUsage>> GetParkingUsagesByUserAsync(int userId)
        {
            return await _context.ParkingUsages
                .Include(p => p.User)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
        }

        public async Task<ParkingUsage> UpdateParkingUsageAsync(ParkingUsage usage)
        {
            usage.UpdatedAt = DateTime.UtcNow;
            _context.ParkingUsages.Update(usage);
            await _context.SaveChangesAsync();
            return usage;
        }

        public async Task<bool> DeleteParkingUsageAsync(int id)
        {
            var usage = await _context.ParkingUsages.FindAsync(id);
            if (usage == null)
                return false;

            _context.ParkingUsages.Remove(usage);
            await _context.SaveChangesAsync();
            return true;
        }

        // Parking Return methods
        public async Task<ParkingReturn> CreateParkingReturnAsync(ParkingReturn returnRecord)
        {
            _context.ParkingReturns.Add(returnRecord);
            await _context.SaveChangesAsync();
            return returnRecord;
        }

        public async Task<ParkingReturn?> GetParkingReturnByIdAsync(int id)
        {
            return await _context.ParkingReturns
                .Include(p => p.User)
                .Include(p => p.RelatedUsage)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ParkingReturn>> GetParkingReturnsByDateAsync(DateTime date)
        {
            return await _context.ParkingReturns
                .Include(p => p.User)
                .Include(p => p.RelatedUsage)
                .Where(p => p.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<ParkingReturn>> GetParkingReturnsByUserAsync(int userId)
        {
            return await _context.ParkingReturns
                .Include(p => p.User)
                .Include(p => p.RelatedUsage)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
        }

        public async Task<ParkingReturn> UpdateParkingReturnAsync(ParkingReturn returnRecord)
        {
            returnRecord.UpdatedAt = DateTime.UtcNow;
            _context.ParkingReturns.Update(returnRecord);
            await _context.SaveChangesAsync();
            return returnRecord;
        }

        public async Task<bool> DeleteParkingReturnAsync(int id)
        {
            var returnRecord = await _context.ParkingReturns.FindAsync(id);
            if (returnRecord == null)
                return false;

            _context.ParkingReturns.Remove(returnRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        // External Parking methods
        public async Task<ExternalParking> CreateExternalParkingAsync(ExternalParking parking)
        {
            _context.ExternalParkings.Add(parking);
            await _context.SaveChangesAsync();
            return parking;
        }

        public async Task<ExternalParking?> GetExternalParkingByIdAsync(int id)
        {
            return await _context.ExternalParkings
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ExternalParking>> GetExternalParkingsByDateAsync(DateTime date)
        {
            return await _context.ExternalParkings
                .Include(p => p.User)
                .Where(p => p.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<ExternalParking>> GetExternalParkingsByUserAsync(int userId)
        {
            return await _context.ExternalParkings
                .Include(p => p.User)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
        }

        public async Task<ExternalParking> UpdateExternalParkingAsync(ExternalParking parking)
        {
            parking.UpdatedAt = DateTime.UtcNow;
            _context.ExternalParkings.Update(parking);
            await _context.SaveChangesAsync();
            return parking;
        }

        public async Task<bool> DeleteExternalParkingAsync(int id)
        {
            var parking = await _context.ExternalParkings.FindAsync(id);
            if (parking == null)
                return false;

            _context.ExternalParkings.Remove(parking);
            await _context.SaveChangesAsync();
            return true;
        }

        // Statistics methods
        public async Task<object> GetUserStatisticsAsync(int userId, DateTime startDate, DateTime endDate)
        {
            var usages = await _context.ParkingUsages
                .Where(p => p.UserId == userId && p.Date >= startDate && p.Date <= endDate)
                .CountAsync();

            var returns = await _context.ParkingReturns
                .Where(p => p.UserId == userId && p.Date >= startDate && p.Date <= endDate)
                .CountAsync();

            var externalParkings = await _context.ExternalParkings
                .Where(p => p.UserId == userId && p.Date >= startDate && p.Date <= endDate)
                .CountAsync();

            var totalCost = await _context.ExternalParkings
                .Where(p => p.UserId == userId && p.Date >= startDate && p.Date <= endDate && p.Cost.HasValue)
                .SumAsync(p => p.Cost ?? 0);

            return new
            {
                ParkingUsages = usages,
                ParkingReturns = returns,
                ExternalParkings = externalParkings,
                TotalCost = totalCost
            };
        }

        public async Task<object> GetMonthlyStatisticsAsync(DateTime month)
        {
            var startDate = new DateTime(month.Year, month.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var usages = await _context.ParkingUsages
                .Where(p => p.Date >= startDate && p.Date <= endDate)
                .CountAsync();

            var returns = await _context.ParkingReturns
                .Where(p => p.Date >= startDate && p.Date <= endDate)
                .CountAsync();

            var externalParkings = await _context.ExternalParkings
                .Where(p => p.Date >= startDate && p.Date <= endDate)
                .CountAsync();

            var totalCost = await _context.ExternalParkings
                .Where(p => p.Date >= startDate && p.Date <= endDate && p.Cost.HasValue)
                .SumAsync(p => p.Cost ?? 0);

            var userStats = await _context.Users
                .Where(u => u.IsActive)
                .Select(u => new
                {
                    UserId = u.Id,
                    UserName = u.Name,
                    ParkingUsages = u.ParkingUsages.Count(p => p.Date >= startDate && p.Date <= endDate),
                    ParkingReturns = u.ParkingReturns.Count(p => p.Date >= startDate && p.Date <= endDate),
                    ExternalParkings = u.ExternalParkings.Count(p => p.Date >= startDate && p.Date <= endDate),
                    TotalCost = u.ExternalParkings.Where(p => p.Date >= startDate && p.Date <= endDate && p.Cost.HasValue).Sum(p => p.Cost ?? 0)
                })
                .ToListAsync();

            return new
            {
                Month = month.ToString("yyyy-MM"),
                TotalParkingUsages = usages,
                TotalParkingReturns = returns,
                TotalExternalParkings = externalParkings,
                TotalCost = totalCost,
                UserStatistics = userStats
            };
        }
    }
}
