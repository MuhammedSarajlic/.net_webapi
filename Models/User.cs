using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<Shop>? OwnedShops { get; set; }
}