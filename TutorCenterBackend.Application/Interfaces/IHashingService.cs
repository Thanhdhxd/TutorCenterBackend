namespace TutorCenterBackend.Application.Interfaces
{
    public interface IHashingService
    {
        Task<string> HashPassword(string password);
        Task<bool> VerifyPassword(string password, string hashedPassword);
    }
}
