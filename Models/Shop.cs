using System.ComponentModel.DataAnnotations;

namespace Models;

public class Shop
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ProfileImage { get; set; }

    //Reference for User as owners of shop
    public int ShopOwnerId { get; set; }
    public User? ShopOwner { get; set; }

    //Reference to porducts for this shop
    public ICollection<Product>? Products { get; set; }

    //Reference to category
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

}