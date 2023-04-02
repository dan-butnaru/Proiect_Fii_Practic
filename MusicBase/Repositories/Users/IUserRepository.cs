using MusicBase.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBase.Repositories.Users
{
    public interface IUserRepository
    {
        void CreateUser(UserDto userDto);
        IEnumerable<UserDto> SearchByName(string searchTerm);
        void DeleteUser(int userId);
        List<UserDto> GetAll();
        int GetUserIdFromEmail (string email);
        void UpdateUser(UserDto userDto);
        UserDto? GetUser(int userId);
        UserDto? GetUserByEmail(string email);
        void AddSongs(int? userId, int songId);
    }
}
