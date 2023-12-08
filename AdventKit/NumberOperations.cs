namespace AdventKit;

public static class NumberOperations
{
    /// <summary>
    /// Least Common Multiple for two integers
    /// </summary>
    public static int LCM(int a, int b)
    {
        return a * b / GCD(a, b);
    }

    /// <summary>
    /// Least Common Multiple for two longs
    /// </summary>
    public static long LCM(long a, long b)
    {
        return a * b / GCD(a, b);
    }

    /// <summary>
    /// Greatest Common Divisor for two integers
    /// </summary>
    public static int GCD(int a, int b)
    {
        while (b != 0)
        {
            var t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    /// <summary>
    /// Greatest Common Divisor for two longs
    /// </summary>
    public static long GCD(long a, long b)
    {
        while (b != 0)
        {
            var t = b;
            b = a % b;
            a = t;
        }
        return a;
    }
}
