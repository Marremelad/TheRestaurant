using TheRestaurant.Enums;

namespace TheRestaurant.Utilities;

/// <summary>
/// Provides utility methods for working with time slots, including time mappings and availability filtering.
/// </summary>
public static class TimeSlotExtensions
{
    // Maps each time slot enum to its corresponding start and end times for the restaurant's operating schedule.
    private static readonly Dictionary<TimeSlot, (TimeSpan Start, TimeSpan End)> TimeSlotMappings = new()
    {
        { TimeSlot.Slot10To12, (new TimeSpan(10, 0, 0), new TimeSpan(12, 0, 0)) },
        { TimeSlot.Slot12To14, (new TimeSpan(12, 0, 0), new TimeSpan(14, 0, 0)) },
        { TimeSlot.Slot14To16, (new TimeSpan(14, 0, 0), new TimeSpan(16, 0, 0)) },
        { TimeSlot.Slot16To18, (new TimeSpan(16, 0, 0), new TimeSpan(18, 0, 0)) }
    };

    /// <summary>
    /// Gets the start time for a specific time slot from the predefined mappings.
    /// </summary>
    private static TimeSpan GetStartTime(this TimeSlot timeSlot) => 
        TimeSlotMappings[timeSlot].Start;

    /// <summary>
    /// Gets the end time for a specific time slot from the predefined mappings.
    /// </summary>
    private static TimeSpan GetEndTime(this TimeSlot timeSlot) => 
        TimeSlotMappings[timeSlot].End;
    
    /// <summary>
    /// Determines if a time slot is still available for booking by checking if current time is before the slot's start time.
    /// </summary>
    private static bool IsTimeSlotStillAvailable(TimeSlot timeSlot, TimeSpan currentTime) =>
        currentTime < timeSlot.GetStartTime();
    
    /// <summary>
    /// Returns all valid time slots for a given date, filtering out slots in the past for today's date.
    /// </summary>
    public static IEnumerable<TimeSlot> GetValidTimeSlotsForDate(DateOnly requestedDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
    
        // For future dates, all time slots are available.
        if (requestedDate > today)
            return Enum.GetValues<TimeSlot>();
        
        // For today's date, filter out time slots that have already started.
        var currentTime = DateTime.Now.TimeOfDay;
        return Enum.GetValues<TimeSlot>()
            .Where(slot => IsTimeSlotStillAvailable(slot, currentTime));
    }
}