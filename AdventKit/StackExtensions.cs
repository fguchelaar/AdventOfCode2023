namespace AdventKit;

public static class StackExtensions
{
    /// <summary>
    /// Clone a stack
    /// </summary>
    public static Stack<T> Clone<T>(this Stack<T> stack)
    {
        var array = new T[stack.Count];
        stack.CopyTo(array, 0);
        Array.Reverse(array);
        return new Stack<T>(array);
    }
}
