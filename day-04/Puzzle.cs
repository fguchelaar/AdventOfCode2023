namespace day_04;

public class Puzzle
{
    private readonly IEnumerable<Card> cards;

    public Puzzle(string input)
    {
        cards = input.Trim().Split("\n").Select(line => new Card(line));
    }

    public int Part1() => cards
        .Select(card => card.Points)
        .Sum();

    public int Part2() => cards
            .Aggregate(new Dictionary<int, int>(), (winnings, card) =>
            {
                // Add the card itself to the winnings
                if (!winnings.ContainsKey(card.Id)) winnings[card.Id] = 0;
                winnings[card.Id] += 1;

                // Add the copies of the cards we won
                for (int i = 1; i <= card.CountOfMatching && i < cards.Count(); i++)
                {
                    var newCard = card.Id + i;
                    if (!winnings.ContainsKey(newCard)) winnings[newCard] = 0;
                    winnings[newCard] += winnings[card.Id];
                }
                return winnings;
            })
            .Sum(w => w.Value);
}

class Card
{
    private IEnumerable<int> winningNumbers;
    private IEnumerable<int> numbersYouHave;

    public int Id { get; }
    public int CountOfMatching => winningNumbers.Intersect(numbersYouHave).Count();
    public int Points => CountOfMatching > 0 ? (int)Math.Pow(2, CountOfMatching - 1) : 0;

    public Card(string str)
    {
        // parse "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53" into id, winningNumbers and numbersYouHave
        var parts = str.Split(":");
        Id = int.Parse(parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);

        var allNumbers = parts[1].Split("|");
        winningNumbers = allNumbers[0]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);
        numbersYouHave = allNumbers[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);
    }
}
