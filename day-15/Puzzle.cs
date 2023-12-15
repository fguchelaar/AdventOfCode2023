using System.Collections.Specialized;

namespace day_15;

public class Puzzle(string input)
{
    private readonly string input = input.Trim();

    private readonly Dictionary<int, OrderedDictionary> boxes =
        Enumerable.Range(0, 256).ToDictionary(i => i, i => new OrderedDictionary());

    int Hash(string str) => str.Aggregate(0, (acc, ch) => (acc + ch) * 17 % 256);

    public int Part1() => input.Split(',').Select(Hash).Sum();

    public int Part2()
    {
        foreach (var step in input.Split(','))
        {
            var parts = step.Split(new char[] { '-', '=' }, StringSplitOptions.RemoveEmptyEntries);
            var label = parts[0];
            var box = Hash(label);

            if (parts.Length > 1)
            {
                boxes[box][label] = int.Parse(parts[1]);
            }
            else
            {
                boxes[box].Remove(label);
            }
        }

        var sum = 0;
        foreach (var box in boxes)
        {
            var lenses = box.Value.Keys
                .Cast<string>()
                .Select((label, slot) => (label, slot))
                .Zip(box.Value.Values.Cast<int>());

            foreach (var lens in lenses)
            {
                var lensValue = (box.Key + 1) * (lens.First.slot + 1) * lens.Second;
                sum += lensValue;
            }
        }
        return sum;
    }
}
