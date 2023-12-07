using System.Collections.Immutable;

namespace day_07;

public class Puzzle(string input)
{
    private readonly IEnumerable<Hand> hands =
        input.Trim().Split("\n").Select(line => new Hand(line));

    public int Part1() => hands
            .OrderBy(h => h.Type) // Order by hand type
            .ThenBy(h => h) // then by card ranking
            .Select((h, i) => (h, i)) // add index or 'rank'
            .Aggregate(0, (acc, hand) => acc + hand.h.Bid * (hand.i + 1));

    public int Part2() => hands
            .Select(h => { h.UseJokers = true; return h; })
            .OrderBy(h => h.Type)
            .ThenBy(h => h)
            .Select((h, i) => (h, i))
            .Aggregate(0, (acc, hand) => acc + hand.h.Bid * (hand.i + 1));

}

class Hand : IComparable<Hand>
{
    private static readonly string cardOrder = "23456789TJQKA";
    private static readonly string cardOrderWithJokers = "J23456789TQKA";
    public bool UseJokers { get; set; } = false;
    public string Cards { get; }
    public int Bid { get; }

    public int Type => UseJokers
                ? TypeWithJokers
                : CalculateType(Cards.ToImmutableDictionary(c => c, c => Cards.Count(c2 => c2 == c)));

    public int TypeWithJokers
    {
        get
        {
            // If all cards are Jokers, return Five of a kind
            if (Cards.All(c => c == 'J')) { return 7; }

            var numberOfJokers = Cards.Count(c => c == 'J');
            var handWithoutJokers = Cards.Replace("J", "");
            var countMap = handWithoutJokers
                .ToImmutableDictionary(c => c, c => handWithoutJokers.Count(c2 => c2 == c));

            return countMap.Aggregate(1, (acc, pair) =>
            {
                var tempCounts = new Dictionary<char, int>(countMap);
                tempCounts[pair.Key] += numberOfJokers;
                return Math.Max(acc, CalculateType(tempCounts.ToImmutableDictionary()));
            });
        }
    }

    public Hand(string str)
    {
        var parts = str.Trim().Split(" ");
        Cards = parts[0];
        Bid = int.Parse(parts[1]);
    }

    private int CalculateType(ImmutableDictionary<char, int> cardMap) => cardMap.Count switch
    {
        1 => 7, // Five of a kind
        2 => cardMap.ContainsValue(4) ? 6 : 5, // Four of a kind or Full house
        3 => cardMap.ContainsValue(3) ? 4 : 3, // Three of a kind or Two pair
        4 => cardMap.ContainsValue(2) ? 2 : 1, // One pair or High card
        _ => 1
    };

    public int CompareTo(Hand? other)
    {
        if (other == null) return 1;
        var order = UseJokers ? cardOrderWithJokers : cardOrder;
        return Cards.Zip(other.Cards)
            .Select(pair => order.IndexOf(pair.First).CompareTo(order.IndexOf(pair.Second)))
            .FirstOrDefault(diff => diff != 0);
    }
}
