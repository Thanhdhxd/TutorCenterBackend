using TutorCenterBackend.Application.Interfaces;

namespace TutorCenterBackend.Infrastructure.ExternalServices
{
    public class HashingService : IHashingService
    {
        private readonly int saltRounds = 12;

        public async Task<string> HashPassword(string password)
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password, saltRounds));
        }

        public async Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.Verify(password, hashedPassword));
        }
    }
}
