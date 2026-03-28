namespace Philiprehberger.OptionType;

/// <summary>
/// Represents an optional value. An instance of <see cref="Option{T}"/> either contains a value
/// (<c>Some</c>) or is empty (<c>None</c>).
/// </summary>
/// <typeparam name="T">The type of the contained value.</typeparam>
public readonly struct Option<T> : IEquatable<Option<T>>
{
    private readonly T _value;
    private readonly bool _isSome;

    internal Option(T value, bool isSome)
    {
        _value = value;
        _isSome = isSome;
    }

    /// <summary>
    /// Gets a value indicating whether this option contains a value.
    /// </summary>
    public bool IsSome => _isSome;

    /// <summary>
    /// Gets a value indicating whether this option is empty.
    /// </summary>
    public bool IsNone => !_isSome;

    /// <summary>
    /// Transforms the contained value using the specified mapping function.
    /// Returns <c>None</c> if this option is empty.
    /// </summary>
    /// <typeparam name="U">The type of the transformed value.</typeparam>
    /// <param name="mapper">The function to apply to the contained value.</param>
    /// <returns>An Option containing the transformed value, or None.</returns>
    public Option<U> Map<U>(Func<T, U> mapper)
    {
        return _isSome ? Option.Some(mapper(_value)) : Option.None<U>();
    }

    /// <summary>
    /// Chains an option-returning function on the contained value.
    /// Returns <c>None</c> if this option is empty.
    /// </summary>
    /// <typeparam name="U">The type of the value in the returned Option.</typeparam>
    /// <param name="binder">The function that returns an Option.</param>
    /// <returns>The result of the binder function, or None.</returns>
    public Option<U> Bind<U>(Func<T, Option<U>> binder)
    {
        return _isSome ? binder(_value) : Option.None<U>();
    }

    /// <summary>
    /// Filters the contained value using the specified predicate.
    /// Returns <c>None</c> if this option is empty or the predicate returns <c>false</c>.
    /// </summary>
    /// <param name="predicate">The predicate to test the value against.</param>
    /// <returns>This option if the predicate holds; otherwise None.</returns>
    public Option<T> Filter(Func<T, bool> predicate)
    {
        return _isSome && predicate(_value) ? this : Option.None<T>();
    }

    /// <summary>
    /// Pattern matches on this option, returning the result of the appropriate function.
    /// </summary>
    /// <typeparam name="U">The return type.</typeparam>
    /// <param name="some">The function to call if this option contains a value.</param>
    /// <param name="none">The function to call if this option is empty.</param>
    /// <returns>The result of the matched function.</returns>
    public U Match<U>(Func<T, U> some, Func<U> none)
    {
        return _isSome ? some(_value) : none();
    }

    /// <summary>
    /// Returns the contained value or the specified default.
    /// </summary>
    /// <param name="defaultValue">The default value to return if this option is empty.</param>
    /// <returns>The contained value or the default.</returns>
    public T ValueOr(T defaultValue)
    {
        return _isSome ? _value : defaultValue;
    }

    /// <summary>
    /// Returns the contained value or throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <returns>The contained value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when this option is empty.</exception>
    public T ValueOrThrow()
    {
        if (!_isSome)
        {
            throw new InvalidOperationException("Cannot get value from None.");
        }

        return _value;
    }

    /// <summary>
    /// Returns a string representation of this option.
    /// </summary>
    /// <returns>"Some(value)" or "None".</returns>
    public override string ToString()
    {
        return _isSome ? $"Some({_value})" : "None";
    }

    /// <summary>
    /// Determines whether this option is equal to another option.
    /// </summary>
    /// <param name="other">The other option to compare with.</param>
    /// <returns><c>true</c> if both options are equal; otherwise <c>false</c>.</returns>
    public bool Equals(Option<T> other)
    {
        if (_isSome != other._isSome)
        {
            return false;
        }

        return !_isSome || EqualityComparer<T>.Default.Equals(_value, other._value);
    }

    /// <summary>
    /// Determines whether this option is equal to the specified object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><c>true</c> if equal; otherwise <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Option<T> other && Equals(other);
    }

    /// <summary>
    /// Returns a hash code for this option.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        return _isSome ? HashCode.Combine(true, _value) : HashCode.Combine(false);
    }

    /// <summary>
    /// Asynchronously transforms the contained value using the specified mapping function.
    /// Returns <c>None</c> if this option is empty.
    /// </summary>
    /// <typeparam name="U">The type of the transformed value.</typeparam>
    /// <param name="mapper">The async function to apply to the contained value.</param>
    /// <returns>A task that resolves to an Option containing the transformed value, or None.</returns>
    public async Task<Option<U>> MapAsync<U>(Func<T, Task<U>> mapper)
    {
        return _isSome ? Option.Some(await mapper(_value).ConfigureAwait(false)) : Option.None<U>();
    }

    /// <summary>
    /// Asynchronously chains an option-returning function on the contained value.
    /// Returns <c>None</c> if this option is empty.
    /// </summary>
    /// <typeparam name="U">The type of the value in the returned Option.</typeparam>
    /// <param name="binder">The async function that returns an Option.</param>
    /// <returns>A task that resolves to the result of the binder function, or None.</returns>
    public async Task<Option<U>> BindAsync<U>(Func<T, Task<Option<U>>> binder)
    {
        return _isSome ? await binder(_value).ConfigureAwait(false) : Option.None<U>();
    }

    /// <summary>
    /// Asynchronously pattern matches on this option, returning the result of the appropriate function.
    /// </summary>
    /// <typeparam name="U">The return type.</typeparam>
    /// <param name="some">The async function to call if this option contains a value.</param>
    /// <param name="none">The async function to call if this option is empty.</param>
    /// <returns>A task that resolves to the result of the matched function.</returns>
    public async Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<Task<U>> none)
    {
        return _isSome
            ? await some(_value).ConfigureAwait(false)
            : await none().ConfigureAwait(false);
    }

    /// <summary>
    /// Returns this option if it contains a value; otherwise returns the specified fallback option.
    /// </summary>
    /// <param name="fallback">The fallback option to return if this option is empty.</param>
    /// <returns>This option if Some; otherwise the fallback.</returns>
    public Option<T> OrElse(Option<T> fallback)
    {
        return _isSome ? this : fallback;
    }

    /// <summary>
    /// Returns this option if it contains a value; otherwise invokes the factory and returns its result.
    /// </summary>
    /// <param name="fallbackFactory">The factory function that produces a fallback option.</param>
    /// <returns>This option if Some; otherwise the result of the factory.</returns>
    public Option<T> OrElse(Func<Option<T>> fallbackFactory)
    {
        return _isSome ? this : fallbackFactory();
    }

    /// <summary>
    /// Executes a side effect when this option contains a value, then returns the option unchanged.
    /// </summary>
    /// <param name="action">The action to execute on the contained value.</param>
    /// <returns>This option, unchanged.</returns>
    public Option<T> Tap(Action<T> action)
    {
        if (_isSome)
        {
            action(_value);
        }

        return this;
    }

    /// <summary>
    /// Executes a side effect when this option is empty, then returns the option unchanged.
    /// </summary>
    /// <param name="action">The action to execute when None.</param>
    /// <returns>This option, unchanged.</returns>
    public Option<T> TapNone(Action action)
    {
        if (!_isSome)
        {
            action();
        }

        return this;
    }

    /// <summary>
    /// Determines whether two options are equal.
    /// </summary>
    public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);

    /// <summary>
    /// Determines whether two options are not equal.
    /// </summary>
    public static bool operator !=(Option<T> left, Option<T> right) => !left.Equals(right);
}
