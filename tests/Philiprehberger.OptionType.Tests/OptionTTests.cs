using Xunit;
using Philiprehberger.OptionType;

namespace Philiprehberger.OptionType.Tests;

public class OptionTTests
{
    [Fact]
    public void Map_OnSome_TransformsValue()
    {
        var option = Option.Some(5);

        var result = option.Map(x => x * 2);

        Assert.True(result.IsSome);
        Assert.Equal(10, result.ValueOr(0));
    }

    [Fact]
    public void Map_OnNone_ReturnsNone()
    {
        var option = Option.None<int>();

        var result = option.Map(x => x * 2);

        Assert.True(result.IsNone);
    }

    [Fact]
    public void Bind_OnSome_ReturnsBinderResult()
    {
        var option = Option.Some(10);

        var result = option.Bind(x => x > 5 ? Option.Some(x.ToString()) : Option.None<string>());

        Assert.True(result.IsSome);
        Assert.Equal("10", result.ValueOr(""));
    }

    [Fact]
    public void Bind_OnNone_ReturnsNone()
    {
        var option = Option.None<int>();

        var result = option.Bind(x => Option.Some(x.ToString()));

        Assert.True(result.IsNone);
    }

    [Fact]
    public void Filter_OnSome_WhenPredicateTrue_ReturnsSome()
    {
        var option = Option.Some(42);

        var result = option.Filter(x => x > 10);

        Assert.True(result.IsSome);
    }

    [Fact]
    public void Filter_OnSome_WhenPredicateFalse_ReturnsNone()
    {
        var option = Option.Some(3);

        var result = option.Filter(x => x > 10);

        Assert.True(result.IsNone);
    }

    [Fact]
    public void Match_OnSome_CallsSomeFunction()
    {
        var option = Option.Some(42);

        var result = option.Match(v => $"got {v}", () => "nothing");

        Assert.Equal("got 42", result);
    }

    [Fact]
    public void Match_OnNone_CallsNoneFunction()
    {
        var option = Option.None<int>();

        var result = option.Match(v => $"got {v}", () => "nothing");

        Assert.Equal("nothing", result);
    }

    [Fact]
    public void ValueOr_OnSome_ReturnsValue()
    {
        var option = Option.Some(42);

        Assert.Equal(42, option.ValueOr(0));
    }

    [Fact]
    public void ValueOr_OnNone_ReturnsDefault()
    {
        var option = Option.None<int>();

        Assert.Equal(0, option.ValueOr(0));
    }

    [Fact]
    public void ValueOrThrow_OnSome_ReturnsValue()
    {
        var option = Option.Some(42);

        Assert.Equal(42, option.ValueOrThrow());
    }

    [Fact]
    public void ValueOrThrow_OnNone_ThrowsInvalidOperationException()
    {
        var option = Option.None<int>();

        Assert.Throws<InvalidOperationException>(() => option.ValueOrThrow());
    }

    [Fact]
    public void Equality_TwoSomeWithSameValue_AreEqual()
    {
        var a = Option.Some(42);
        var b = Option.Some(42);

        Assert.Equal(a, b);
        Assert.True(a == b);
    }

    [Fact]
    public void Equality_TwoNone_AreEqual()
    {
        var a = Option.None<int>();
        var b = Option.None<int>();

        Assert.Equal(a, b);
    }

    [Fact]
    public void ToString_OnSome_ReturnsSomeRepresentation()
    {
        var option = Option.Some(42);

        Assert.Equal("Some(42)", option.ToString());
    }

    [Fact]
    public void ToString_OnNone_ReturnsNone()
    {
        var option = Option.None<int>();

        Assert.Equal("None", option.ToString());
    }
}
