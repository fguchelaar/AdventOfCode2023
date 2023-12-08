namespace AdventKit;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Cycle through the list indefinitely
    /// </summary>
    public static IEnumerable<T> Cycle<T>(this IEnumerable<T> list, int index = 0)
    {
        var count = list.Count();
        index = index % count;

        while (true)
        {
            yield return list.ElementAt(index);
            index = (index + 1) % count;
        }
    }
}
