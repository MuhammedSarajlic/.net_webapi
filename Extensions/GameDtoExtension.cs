using Dtos;
using Models;

namespace Extensions;

public static class GameDtoExtension
{
    public static GameDto AsDto(this Game game){
        return new GameDto(
            game.Name,
            game.Genre,
            game.Price
        );
    } 
}