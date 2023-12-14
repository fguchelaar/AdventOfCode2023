using System.Text;

namespace day_13;

public class Puzzle
{
    private readonly IEnumerable<string> maps;

    public Puzzle(string input)
    {
        maps = input.Trim().Split("\n\n");
    }

    public int Part1()
    {
        var sum = 0;
        foreach (var map in maps)
        {
            var rows = RowsToNumbers(map);
            var horizontal = FindMirror(rows);
            if (horizontal.didFind)
            {
                sum += horizontal.number * 100;
                continue;
            }

            var columns = ColumnsToNumbers(map);
            var vertical = FindMirror(columns);
            if (vertical.didFind)
            {
                sum += vertical.number;
                continue;
            }
        }
        return sum;
    }

    private (bool didFind, int number) FindMirror(IEnumerable<int> rows, int ignore = -2)
    {
        var rowsAndIndex = rows.Select((row, index) => (row, index));
        var zippedRows = rowsAndIndex.Zip(rowsAndIndex.Skip(1), (a, b) => ((a.row, a.index), (b.row, b.index)));
        foreach (var zippedRow in zippedRows)
        {
            if (zippedRow.Item1.row == zippedRow.Item2.row)
            {
                var part1 = rows.Take(zippedRow.Item1.index + 1).ToArray();
                var part2 = rows.Skip(zippedRow.Item1.index + 1).ToArray();
                var mirrored = part1.Reverse().Zip(part2);
                var isMirrored = mirrored.All(pair => pair.First == pair.Second);
                if (isMirrored && (zippedRow.Item1.index + 1) != ignore)
                {
                    return (true, zippedRow.Item1.index + 1);
                }
            }
        }

        return (false, -1);
    }

    private IEnumerable<int> RowsToNumbers(string map)
    {
        var rows = map.Split('\n');
        foreach (var row in rows)
        {
            var bits = row.Replace("#", "1").Replace(".", "0");
            yield return Convert.ToInt32(bits, 2);
        }
    }

    private IEnumerable<int> ColumnsToNumbers(string map)
    {
        var rows = map.Split('\n');
        for (int c = 0; c < rows[0].Length; c++) // loop over the columns
        {
            var column = rows.Select(row => row[c]);
            var bits = string.Join("", column).Replace("#", "1").Replace(".", "0");
            yield return Convert.ToInt32(bits, 2);
        }
    }

    public int Part2()
    {
        var sum = 0;
        foreach (var originalMap in maps)
        {
            var orgHorizontal = FindMirror(RowsToNumbers(originalMap));
            var orgVertical = FindMirror(ColumnsToNumbers(originalMap));

            foreach (var map in Unsmudged(originalMap))
            {

                var rows = RowsToNumbers(map);
                var horizontal = FindMirror(rows, orgHorizontal.number);
                if (horizontal.didFind)
                {
                    sum += horizontal.number * 100;
                    break;
                }

                var columns = ColumnsToNumbers(map);
                var vertical = FindMirror(columns, orgVertical.number);
                if (vertical.didFind)
                {
                    sum += vertical.number;
                    break;
                }
            }
        }
        return sum;
    }
    IEnumerable<string> Unsmudged(string map)
    {
        var rows = map.Split('\n').Length;
        var columns = map.Split('\n')[0].Length;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                StringBuilder sb = new StringBuilder(map);
                int pos = r * (columns + 1) + c;
                var current = map[pos];
                sb[pos] = current == '#' ? '.' : '#';
                yield return string.Join('\n', sb.ToString());
            }
        }
    }
}
