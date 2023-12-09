namespace day_09;

public class Puzzle
{
    private readonly List<Stack<List<int>>> stacks;

    public Puzzle(string input)
    {
        stacks = input.Trim()
            .Split("\n")
            .Select(line => line.Trim().Split(" ").Select(int.Parse).ToList())
            .Select(GetStack)
            .ToList();
    }

    /// <summary>
    /// Get the stack of differences between each element in a list. The last 
    /// element is the one with all zeros.
    /// </summary>
    Stack<List<int>> GetStack(List<int> history)
    {
        var stack = new Stack<List<int>>();
        stack.Push(history);
        while (true)
        {
            var next = Differences(stack.Peek());
            stack.Push(next);
            if (next.All(n => n == 0))
            {
                break;
            }
        }
        return stack;
    }

    /// <summary>
    /// Get the difference between each element in a list
    /// </summary>
    List<int> Differences(List<int> history) => history.Zip(history.Skip(1), (a, b) => b - a).ToList();

    public int Part1() => stacks.Sum(stack => stack.Aggregate(0, (a, b) => a + b.Last()));

    public int Part2() => stacks.Sum(stack => stack.Aggregate(0, (a, b) => b.First() - a));
}
