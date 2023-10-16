using Models;

namespace Dtos;

public record ShopDto(
    int Id,
    string Name,
    string Description,
    int ShopOwnerId,
    ICollection<ProductDto>? Products,
    string? ProfileImage,
    int? CategoryId,
    Category? Category
);