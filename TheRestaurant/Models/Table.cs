using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheRestaurant.Models;

public class Table
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public required int Number { get; set; }
    
    public required int Capacity { get; set; }
    
    public virtual List<Reservation>? Reservations { get; set; }
}