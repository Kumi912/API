public static class FuncExtensions
{
    public static Func<T, bool> And<T>(this Func<T, bool> left, Func<T, bool> right)
    {
        return x => left(x) && right(x);
    }
    public static Func<T, bool> Or<T>(this Func<T, bool> left, Func<T, bool> right)
    {
        return x => left(x) || right(x);
    }
    public static Func<T, bool> Not<T>(this Func<T, bool> func)
    {
        return x => !func(x);
    }
}