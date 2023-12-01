namespace day_01;

public class Puzzle
{
    private readonly string[] input;

    public Puzzle(string input)
    {
        this.input = input.Trim().Split("\n");
    }

    public int Part1()
    {
        return input
            .Select(row => row.First(c => char.IsDigit(c)) + "" + row.Last(c => char.IsDigit(c)))
            .Select(int.Parse)
            .Sum();
    }

    public int Part2()
    {
        return input
            .Select(Normalize)
            .Select(row => row.First(c => char.IsDigit(c)) + "" + row.Last(c => char.IsDigit(c)))
            .Select(int.Parse)
            .Sum();
    }

    string Normalize(string str)
    {
        var result = "";
        for (var i = 0; i < str.Length; i++)
        {
            var c = str[i];
            if (char.IsDigit(c))
            {
                result += c;
            }
            else
            {
                if (str.Remove(0, i).StartsWith("one")) { result += "1"; }
                if (str.Remove(0, i).StartsWith("two")) { result += "2"; }
                if (str.Remove(0, i).StartsWith("three")) { result += "3"; }
                if (str.Remove(0, i).StartsWith("four")) { result += "4"; }
                if (str.Remove(0, i).StartsWith("five")) { result += "5"; }
                if (str.Remove(0, i).StartsWith("six")) { result += "6"; }
                if (str.Remove(0, i).StartsWith("seven")) { result += "7"; }
                if (str.Remove(0, i).StartsWith("eight")) { result += "8"; }
                if (str.Remove(0, i).StartsWith("nine")) { result += "9"; }
            }
        }
        return result;
    }
}
