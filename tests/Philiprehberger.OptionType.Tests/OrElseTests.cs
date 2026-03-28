using Xunit;
using Philiprehberger.OptionType;

namespace Philiprehberger.OptionType.Tests;

public class OrElseTests
{
    [Fact]
    public void OrElse_OnSome_ReturnsOriginal()
    {
        var option = Option.Some(42);
        var fallback = Option.Some(99);

        var result = option.OrElse(fallback);

        Assert.True(result.IsSome);
        Assert.Equal(42, result.ValueOr(0));
    }

    [Fact]
    public void OrElse_OnNone_ReturnsFallback()
    {
        var option = Option.None<int>();
        var fallback = Option.Some(99);

        var result = option.OrElse(fallback);

        Assert.True(result.IsSome);
        Assert.Equal(99, result.ValueOr(0));
    }

    [Fact]
    public void OrElse_OnNone_WithNoneFallback_ReturnsNone()
    {
        var option = Option.None<int>();
        var fallback = Option.None<int>();

        var result = option.OrElse(fallback);

        Assert.True(result.IsNone);
    }

    [Fact]
    public void OrElseFactory_OnSome_DoesNotCallFactory()
    {
        var option = Option.Some(42);
        var factoryCalled = false;

        var result = option.OrElse(() =>
        {
            factoryCalled = true;
            return Option.Some(99);
        });

        Assert.False(factoryCalled);
        Assert.Equal(42, result.ValueOr(0));
    }

    [Fact]
    public void OrElseFactory_OnNone_CallsFactory()
    {
        var option = Option.None<int>();

        var result = option.OrElse(() => Option.Some(99));

        Assert.True(result.IsSome);
        Assert.Equal(99, result.ValueOr(0));
    }

    [Fact]
    public void OrElseFactory_OnNone_FactoryReturnsNone_ReturnsNone()
    {
        var option = Option.None<int>();

        var result = option.OrElse(() => Option.None<int>());

        Assert.True(result.IsNone);
    }
}
