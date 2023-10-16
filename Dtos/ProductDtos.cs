using Models;

namespace Dtos;

public record ProductDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    List<string> ProductImages,
    int ShopId,
    string ShopName
);