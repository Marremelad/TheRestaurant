using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TheRestaurant.Enums;

namespace TheRestaurant.Models;

[Index(nameof(Date), nameof(TimeSlot), nameof(TableNumber), IsUnique = true)]
public class ReservationHold
{
    [Key]
    public int Id { get; set; }
    
    public DateOnly Date { get; set; } 
        
    public TimeSlot TimeSlot { get; set; }
    
    public int TableNumber { get; set; }
    
    public int TableCapacity { get; set; }
}