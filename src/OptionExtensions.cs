namespace Philiprehberger.OptionType;

/// <summary>
/// LINQ and conversion extension methods for <see cref="Option{T}"/>.
/// </summary>
public static class OptionExtensions
{
    /// <summary>
    /// LINQ Select support. Maps the contained value using the specified selector.
    /// </summary>
    /// <typeparam name="T">The source value type.</typeparam>
    /// <typeparam name="U">The result value type.</typeparam>
    /// <param name="option">The source option.</param>
    /// <param name="selector">The mapping function.</param>
    /// <returns>An Option containing the mapped value, or None.</returns>
    public static Option<U> Select<T, U>(this Option<T> option, Func<T, U> selector)
    {
        return option.Map(selector);
    }

    /// <summary>
    /// LINQ SelectMany support. Enables multi-from queries over options.
    /// </summary>
    /// <typeparam name="T">The source value type.</typeparam>
    /// <typeparam name="U">The intermediate value type.</typeparam>
    /// <typeparam name="V">The result value type.</typeparam>
    /// <param name="option">The source option.</param>
    /// <param name="binder">The function that returns an intermediate Option.</param>
    /// <param name="projector">The function that combines source and intermediate values.</param>
    /// <returns>An Option containing the projected value, or None.</returns>
    public static Option<V> SelectMany<T, U, V>(
        this Option<T> option,
        Func<T, Option<U>> binder,
        Func<T, U, V> projector)
    {
        return option.Bind(t => binder(t).Map(u => projector(t, u)));
    }

    /// <summary>
    /// LINQ Where support. Filters the contained value using the specified predicate.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="option">The source option.</param>
    /// <param name="predicate">The predicate to test against.</param>
    /// <returns>This option if the predicate holds; otherwise None.</returns>
    public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate)
    {
        return option.Filter(predicate);
    }

    /// <summary>
    /// Converts a nullable value to an Option.
    /// Returns <c>Some</c> if the value is non-null; otherwise <c>None</c>.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The nullable value.</param>
    /// <returns>An Option wrapping the value.</returns>
    public static Option<T> ToOption<T>(this T? value) where T : class
    {
        return Option.From(value);
    }

    /// <summary>
    /// Converts a nullable value type to an Option.
    /// Returns <c>Some</c> if the value has a value; otherwise <c>None</c>.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The nullable value.</param>
    /// <returns>An Option wrapping the value.</returns>
    public static Option<T> ToOption<T>(this T? value) where T : struct
    {
        return value.HasValue ? Option.Some(value.Value) : Option.None<T>();
    }

    /// <summary>
    /// Flattens a nested <c>Option&lt;Option&lt;T&gt;&gt;</c> into a single <c>Option&lt;T&gt;</c>.
    /// </summary>
    /// <typeparam name="T">The inner value type.</typeparam>
    /// <param name="option">The nested option.</param>
    /// <returns>The flattened option.</returns>
    public static Option<T> Flatten<T>(this Option<Option<T>> option)
    {
        return option.Bind(inner => inner);
    }
}
