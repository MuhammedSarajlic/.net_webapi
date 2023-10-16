using Dtos;
using Models;

namespace Extensions;

public static class UserDtoExtension
{
    public static UserDto AsDto(this User user, string token){
        return new UserDto(
            user.Username,
            token
        );
    }
}
