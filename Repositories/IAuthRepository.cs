using Dtos;
using Models;

namespace Repositories;

public interface IAuthRepository
{
    Task UserRegisterAsync(User user);
    Task<UserDto> UserLoginAsync(User user);
    Task<UserDto> GetUserAsync(string username);
}