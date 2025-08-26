using TheRestaurant.Enums;

namespace TheRestaurant.Utilities;

public class TableTimeSlotMatrix(DbSetTracker tracker)
{
    public List<(int, int)> Matrix = [];
    
    public void PopulateMatrix()
    {
        var tables = tracker.Tables.Select(table => table.Number).ToList();
        var enums = Enum.GetValues<TimeSlot>().Cast<int>().ToList();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(tables);
        Console.WriteLine(enums);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Matrix = tables.SelectMany(t => enums.Select(e => (t, e))).ToList();
    }
}