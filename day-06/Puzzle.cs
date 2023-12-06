namespace day_06;

public class Puzzle
{
    private readonly int[] times;
    private readonly int[] distances;

    public Puzzle(string input)
    {
        var parts = input.Trim().Split("\n");
        times = parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
        distances = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
    }

    public int Part1() => times.Zip(distances)
        .Select(race =>
        {
            var range = Enumerable.Range(0, race.First);
            var first = range.First(ms => (race.First - ms) * ms > race.Second);
            var last = range.Last(ms => (race.First - ms) * ms > race.Second);
            return last - first + 1;
        })
        .Aggregate(1, (a, b) => a * b);

    public int Part2()
    {
        var totalTime = int.Parse(times
            .Select(t => t.ToString())
            .Aggregate("", (a, b) => a + b));

        var totalDistance = long.Parse(distances
                .Select(t => t.ToString())
                .Aggregate("", (a, b) => a + b));

        var range = Enumerable.Range(0, totalTime);
        var first = range.First(ms => (totalTime - ms) * ms > totalDistance);
        var last = range.Last(ms => (totalTime - ms) * ms > totalDistance);
        return last - first + 1;
    }
}
