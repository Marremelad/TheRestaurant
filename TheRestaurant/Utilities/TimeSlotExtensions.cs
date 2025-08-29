using TheRestaurant.Enums;

namespace TheRestaurant.Utilities;

public static class TimeSlotExtensions
{
    private static readonly Dictionary<TimeSlot, (TimeSpan Start, TimeSpan End)> TimeSlotMappings = new()
    {
        { TimeSlot.Slot10To12, (new TimeSpan(10, 0, 0), new TimeSpan(12, 0, 0)) },
        { TimeSlot.Slot12To14, (new TimeSpan(12, 0, 0), new TimeSpan(14, 0, 0)) },
        { TimeSlot.Slot14To16, (new TimeSpan(14, 0, 0), new TimeSpan(16, 0, 0)) },
        { TimeSlot.Slot16To18, (new TimeSpan(16, 0, 0), new TimeSpan(18, 0, 0)) }
    };

    private static TimeSpan GetStartTime(this TimeSlot timeSlot) => 
        TimeSlotMappings[timeSlot].Start;

    private static TimeSpan GetEndTime(this TimeSlot timeSlot) => 
        TimeSlotMappings[timeSlot].End;
    
    private static bool IsTimeSlotStillAvailable(TimeSlot timeSlot, TimeSpan currentTime) =>
        currentTime < timeSlot.GetStartTime();
    
    public static IEnumerable<TimeSlot> GetValidTimeSlotsForDate(DateOnly requestedDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
    
        if (requestedDate > today)
            return Enum.GetValues<TimeSlot>();
        
        var currentTime = DateTime.Now.TimeOfDay;
        return Enum.GetValues<TimeSlot>()
            .Where(slot => IsTimeSlotStillAvailable(slot, currentTime));
    }
}