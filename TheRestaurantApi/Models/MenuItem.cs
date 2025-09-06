using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheRestaurant.Models;

[Index(nameof(Name), IsUnique = true)]
public class MenuItem
{
    public int Id { get; set; }
    
    [StringLength(50)]
    public required string Name { get; set; }
    
    [Column(TypeName = "decimal(10, 2)")]
    public required decimal Price { get; set; }
    
    [StringLength(300)]
    public required string Description { get; set; }
    
    [StringLength(500)]
    public required string Image { get; set; }
}