using System.ComponentModel.DataAnnotations;

namespace Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public decimal Price { get; set; }
    public List<string>? ProductImages { get; set; }

    //Reference to shop where this product is saved
    public int ShopId { get; set; }
    public Shop? Shop { get; set; }

    //Reference to category
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}