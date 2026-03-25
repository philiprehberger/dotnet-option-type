using Xunit;
using Philiprehberger.OptionType;

namespace Philiprehberger.OptionType.Tests;

public class OptionExtensionsTests
{
    [Fact]
    public void Select_OnSome_MapsValue()
    {
        var option = Option.Some(5);

        var result = option.Select(x => x * 3);

        Assert.Equal(15, result.ValueOr(0));
    }

    [Fact]
    public void Where_OnSome_FiltersValue()
    {
        var option = Option.Some(10);

        var passing = option.Where(x => x > 5);
        var failing = option.Where(x => x > 20);

        Assert.True(passing.IsSome);
        Assert.True(failing.IsNone);
    }

    [Fact]
    public void ToOption_OnNonNullReference_ReturnsSome()
    {
        string value = "hello";

        var option = value.ToOption();

        Assert.True(option.IsSome);
        Assert.Equal("hello", option.ValueOr(""));
    }

    [Fact]
    public void ToOption_OnNullReference_ReturnsNone()
    {
        string? value = null;

        var option = value.ToOption();

        Assert.True(option.IsNone);
    }

    [Fact]
    public void ToOption_OnNullableStructWithValue_ReturnsSome()
    {
        int? value = 42;

        var option = value.ToOption();

        Assert.True(option.IsSome);
        Assert.Equal(42, option.ValueOr(0));
    }

    [Fact]
    public void ToOption_OnNullableStructWithoutValue_ReturnsNone()
    {
        int? value = null;

        var option = value.ToOption();

        Assert.True(option.IsNone);
    }

    [Fact]
    public void Flatten_OnNestedSome_ReturnsInnerOption()
    {
        var nested = Option.Some(Option.Some(42));

        var result = nested.Flatten();

        Assert.True(result.IsSome);
        Assert.Equal(42, result.ValueOr(0));
    }
}
