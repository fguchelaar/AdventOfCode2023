namespace day_05;

public class Puzzle
{
    private readonly IEnumerable<long> seeds;
    private readonly IList<Map> almanac;

    public Puzzle(string input)
    {
        // let the parsing begin...
        var parts = input.Trim().Split($"\n\n");
        seeds = parts[0].Split(" ").Skip(1).Select(long.Parse);

        almanac = new List<Map>();
        foreach (var map in parts.Skip(1))
        {
            var lines = map.Split("\n");
            var sourceCategory = lines[0].Split(" ")[0].Split("-")[0];
            var destinationCategory = lines[0].Split(" ")[0].Split("-")[2];
            var newMap = new Map(sourceCategory, destinationCategory);
            foreach (var line in lines.Skip(1))
            {
                newMap.Ranges.Add(Range.Parse(line));
            }
            almanac.Add(newMap);
        }
    }

    public long Part1()
    {
        var closestLocation = long.MaxValue;

        foreach (var seed in seeds)
        {
            closestLocation = Math.Min(closestLocation, almanac.Aggregate(seed, (location, map) => map.Convert(location)));
        }

        return closestLocation;
    }

    public long Part2()
    {
        var closestLocation = long.MaxValue;
        var seeds = new List<long>(this.seeds);
        Console.WriteLine($"Total ranges: {seeds.Count() / 2}");
        for (int i = 0; i < seeds.Count(); i += 2)
        {

            var start = seeds[i];
            var range = seeds[i + 1];

            Console.WriteLine($"Range {start} - {start + range} (#{range})");

            for (long seed = start; seed < start + range; seed++)
            {
                closestLocation = Math.Min(closestLocation, almanac.Aggregate(seed, (location, map) => map.Convert(location)));
            }
        }

        return closestLocation;
    }
}

class Map
{
    string SourceCategory { get; init; }
    string DestinationCategory { get; init; }

    public IList<Range> Ranges { get; init; }

    public Map(string sourceCategory, string destinationCategory)
    {
        SourceCategory = sourceCategory;
        DestinationCategory = destinationCategory;
        Ranges = new List<Range>();
    }

    public long Convert(long value)
    {
        foreach (var range in Ranges)
        {
            if (range.Contains(value))
            {
                return range.Map(value);
            }
        }
        return value;
    }

}

record Range(long destination, long source, long length)
{

    public bool Contains(long value) => value >= source && value < source + length;

    public long Map(long value) => Contains(value) ? value - source + destination : value;

    public static Range Parse(string line)
    {
        var parts = line.Split(" ");
        return new Range(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
    }

}
