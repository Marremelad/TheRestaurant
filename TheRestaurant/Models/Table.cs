using System.ComponentModel.DataAnnotations;

namespace TheRestaurant.Models;

public class Table
{
    [Key]
    public int Id { get; set; }
    
    public required int Number { get; set; }
    
    public required int Capacity { get; set; }
    
    public virtual List<Reservation>? Reservations { get; set; }
}