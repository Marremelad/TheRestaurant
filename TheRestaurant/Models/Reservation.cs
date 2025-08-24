using System.ComponentModel.DataAnnotations;
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
    
    [StringLength(50)]
    public required string FirstName { get; set; }
    
    [StringLength(50)]
    public required string LastName { get; set; }
    
    [StringLength(254)]
    public required string Email { get; set; }
}