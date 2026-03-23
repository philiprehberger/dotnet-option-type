namespace Philiprehberger.OptionType;

/// <summary>
/// Static factory methods for creating <see cref="Option{T}"/> instances.
/// </summary>
public static class Option
{
    /// <summary>
    /// Creates an Option containing the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to wrap.</param>
    /// <returns>An Option containing the value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <c>null</c>.</exception>
    public static Option<T> Some<T>(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new Option<T>(value, isSome: true);
    }

    /// <summary>
    /// Creates an empty Option of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the absent value.</typeparam>
    /// <returns>An empty Option.</returns>
    public static Option<T> None<T>()
    {
        return new Option<T>(default!, isSome: false);
    }

    /// <summary>
    /// Creates an Option from a nullable value. Returns <c>Some</c> if the value is non-null,
    /// otherwise returns <c>None</c>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The nullable value.</param>
    /// <returns>Some if value is non-null; None otherwise.</returns>
    public static Option<T> From<T>(T? value)
    {
        return value is null ? None<T>() : Some(value);
    }
}
