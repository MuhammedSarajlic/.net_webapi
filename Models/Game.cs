using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;
public class Game
{
    [Key]
    public int Id{get; set;}
    public string Name { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public decimal Price { get; set; }

    [Column(TypeName="date")]
    public DateTime ReleaseDate {get; set;}
}