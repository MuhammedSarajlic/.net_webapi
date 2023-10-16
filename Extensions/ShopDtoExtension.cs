using Dtos;
using Exceptions;
using Models;

namespace Extensions;

public static class ShopDtoExtension
{
    public static ShopDto AsDto(this Shop shop){

        var products = shop.Products?.Select(p => p.AsDto()).ToList();

        return new ShopDto(
            shop.Id,
            shop.Name,
            shop.Description,
            shop.ShopOwnerId,
            products,
            shop.ProfileImage,
            shop.CategoryId,
            shop.Category
        );
    } 
}