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
                newMap.RangeMaps.Add(RangeMap.Parse(line));
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
        var ranges = new List<Range>();
        var seeds = new List<long>(this.seeds);
        for (int i = 0; i < seeds.Count(); i += 2)
        {
            var testRange = new Range(seeds[i], seeds[i] + seeds[i + 1] - 1);
            var tempRanges = new List<Range>() { testRange };
            foreach (var map in almanac)
            {
                tempRanges = map.Cut(tempRanges.ToArray()).ToList();
            }
            ranges.AddRange(tempRanges);
        }
        return ranges.Min(r => r.Start);
    }
}

class Map
{
    string SourceCategory { get; init; }
    string DestinationCategory { get; init; }

    public IList<RangeMap> RangeMaps { get; init; }

    public Map(string sourceCategory, string destinationCategory)
    {
        SourceCategory = sourceCategory;
        DestinationCategory = destinationCategory;
        RangeMaps = new List<RangeMap>();
    }

    public long Convert(long value)
    {
        foreach (var range in RangeMaps)
        {
            if (range.Contains(value))
            {
                return range.Map(value);
            }
        }
        return value;
    }

    public Range[] Cut(Range[] ranges)
    {
        var result = new List<Range>();
        var tempRanges = new List<Range>();
        tempRanges.AddRange(ranges);
        while (tempRanges.Any())
        {
            var range = tempRanges.First();
            tempRanges.RemoveAt(0);

            var didCut = true;
            foreach (var map in RangeMaps)
            {
                didCut = true;
                var mask = new Range(map.source, map.source + map.length - 1);
                var delta = map.destination - map.source;
                // map outside of range
                if (!mask.Contains(range.Start) && !mask.Contains(range.End))
                {
                    didCut = false;
                    continue;
                }
                // mask starts inside of range
                else if (mask.Contains(range.Start) && !mask.Contains(range.End))
                {
                    var newRange = new Range(range.Start + delta, mask.End + delta);
                    result.Add(newRange);
                    tempRanges.Add(new Range(mask.End + 1, range.End));
                    break;
                }
                // mask completely consumes range
                else if (mask.Contains(range.Start) && mask.Contains(range.End))
                {
                    var newRange = new Range(range.Start + delta, range.End + delta);
                    result.Add(newRange);
                    break;
                }
                // map in the middle of range
                else if (range.Start < mask.Start && range.End > mask.End)
                {
                    var newRange = new Range(mask.Start + delta, mask.End + delta);
                    result.Add(newRange);

                    tempRanges.Add(new Range(range.Start, mask.Start));
                    tempRanges.Add(new Range(mask.End + 1, range.End));
                    break;
                }
                // map end inside of range
                else if (!mask.Contains(range.Start) && mask.Contains(range.End))
                {
                    var newRange = new Range(mask.Start + delta, range.End + delta);
                    result.Add(newRange);
                    tempRanges.Add(new Range(range.Start, mask.Start - 1));
                    break;
                }
            }
            if (!didCut)
            {
                result.Add(range);
            }

        }
        return result.ToArray();
    }
}

record Range(long Start, long End)
{
    public bool Contains(long value) => value >= Start && value <= End;
}

record RangeMap(long destination, long source, long length)
{
    public bool Contains(long value) => value >= source && value < source + length;

    public long Map(long value) => Contains(value) ? value - source + destination : value;

    public static RangeMap Parse(string line)
    {
        var parts = line.Split(" ");
        return new RangeMap(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
    }

}
