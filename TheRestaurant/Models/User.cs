using System.ComponentModel.DataAnnotations;

namespace TheRestaurant.Models;

public class User
{
    public int Id { get; set; }
        
    [StringLength(50)]
    public required string UserName { get; set; }    
    
    [StringLength(50)]
    public required string PasswordHash { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public virtual List<RefreshToken>? RefreshTokens { get; set; }
}