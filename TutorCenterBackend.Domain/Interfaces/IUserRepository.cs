using TutorCenterBackend.Domain.Entities;

namespace TutorCenterBackend.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
        Task<User?> FindByEmailAsync(string email, CancellationToken ct = default);
        Task<User?> FindWithRoleByIdAsync(int userId, CancellationToken ct = default);
        Task<IEnumerable<string>> GetUserPermissionsAsync(int userId, CancellationToken ct = default);
        Task CreateUserAsync (User user, CancellationToken ct = default);
        Task<User?> FindByIdAsync(int userId, CancellationToken ct = default);
        Task<User> UpdateUserAsync(User user, CancellationToken ct = default);
    }
}
