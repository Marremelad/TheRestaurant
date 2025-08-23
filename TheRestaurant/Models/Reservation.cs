using System.ComponentModel.DataAnnotations.Schema;

namespace TheRestaurant.Models;

public class Reservation
{
    public int Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    public int TimeSlot { get; set; }
    
    [ForeignKey("Table")]
    public required int TableId { get; set; }
    public virtual Table? Table { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
}