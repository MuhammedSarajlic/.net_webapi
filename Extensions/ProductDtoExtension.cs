using Dtos;
using Models;

namespace Extensions;

public static class ProductDtoExtension
{
    public static ProductDto AsDto(this Product product){

        List<string> ProductImages = product.ProductImages?.ToList() ?? new();
        // ShopDto Shop = product.Shop.AsDto();
        
        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            ProductImages,
            product.ShopId,
            product.Shop.Name
        );
    }
}