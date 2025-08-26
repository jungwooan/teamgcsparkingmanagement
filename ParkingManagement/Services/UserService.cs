using Microsoft.EntityFrameworkCore;
using ParkingManagement.Data;
using ParkingManagement.Models;

namespace ParkingManagement.Services
{
    public class UserService : IUserService
    {
        private readonly ParkingDbContext _context;

        public UserService(ParkingDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.ParkingUsages)
                .Include(u => u.ParkingReturns)
                .Include(u => u.ExternalParkings)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.ParkingUsages)
                .Include(u => u.ParkingReturns)
                .Include(u => u.ExternalParkings)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByKakaoIdAsync(string kakaoId)
        {
            return await _context.Users
                .Include(u => u.ParkingUsages)
                .Include(u => u.ParkingReturns)
                .Include(u => u.ExternalParkings)
                .FirstOrDefaultAsync(u => u.KakaoId == kakaoId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.ParkingUsages)
                .Include(u => u.ParkingReturns)
                .Include(u => u.ExternalParkings)
                .Where(u => u.IsActive)
                .ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            user.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.IsActive);
        }

        public async Task<bool> KakaoUserExistsAsync(string kakaoId)
        {
            return await _context.Users.AnyAsync(u => u.KakaoId == kakaoId && u.IsActive);
        }
    }
}
