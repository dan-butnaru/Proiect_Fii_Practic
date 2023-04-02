using MusicBase.Services.Models;

namespace MusicBase.Services
{
    public interface ICryptographyService
    {
        HashedPassword HashPasswordWithSaltGeneration(string password);
        HashedPassword HashPassword(string password, string salt);
    }
}
