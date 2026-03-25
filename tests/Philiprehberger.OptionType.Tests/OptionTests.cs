using Xunit;
using Philiprehberger.OptionType;

namespace Philiprehberger.OptionType.Tests;

public class OptionTests
{
    [Fact]
    public void Some_WithValue_ReturnsSomeOption()
    {
        var option = Option.Some(42);

        Assert.True(option.IsSome);
        Assert.False(option.IsNone);
    }

    [Fact]
    public void Some_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Option.Some<string>(null!));
    }

    [Fact]
    public void None_ReturnsNoneOption()
    {
        var option = Option.None<int>();

        Assert.True(option.IsNone);
        Assert.False(option.IsSome);
    }

    [Fact]
    public void From_WithNonNullValue_ReturnsSome()
    {
        var option = Option.From<string>("hello");

        Assert.True(option.IsSome);
    }

    [Fact]
    public void From_WithNull_ReturnsNone()
    {
        var option = Option.From<string>(null);

        Assert.True(option.IsNone);
    }
}
