using System;

internal static class PipeExtensions
{
    public static B Pipe<A, B>(this A input, Func<A, B> func) => func(input);

    public static A Tap<A>(this A input, Action<A> action)
    {
        action(input);
        return input;
    }
}