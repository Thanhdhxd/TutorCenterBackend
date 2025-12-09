using Microsoft.EntityFrameworkCore;
using TutorCenterBackend.Domain.Entities;
using TutorCenterBackend.Domain.Interfaces;
using TutorCenterBackend.Infrastructure.DataAccess;

namespace TutorCenterBackend.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email, ct);
    }

    public async Task<User?> FindByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email, ct);
    }

    public async Task<User?> FindWithRoleByIdAsync(int userId, CancellationToken ct = default)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId, ct);
    }

    public async Task<IEnumerable<string>> GetUserPermissionsAsync(int userId, CancellationToken ct = default)
    {
        return await _context.Users
            .Where(u => u.UserId == userId && u.IsActive)
            .Select(u => u.Role)
            .SelectMany(r => r.Permissions)
            .Select(p => p.PermissionName)
            .Distinct()
            .ToListAsync(ct);
    }

    public async Task CreateUserAsync(User user, CancellationToken ct = default)
    {
        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<User?> FindByIdAsync(int userId, CancellationToken ct = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == userId, ct);
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken ct = default)
    {
        return await Task.Run(() =>
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }, ct);
    }
}
