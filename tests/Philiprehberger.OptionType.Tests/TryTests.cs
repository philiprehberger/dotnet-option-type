using Xunit;
using Philiprehberger.OptionType;

namespace Philiprehberger.OptionType.Tests;

public class TryTests
{
    [Fact]
    public void Try_WhenFunctionSucceeds_ReturnsSome()
    {
        var result = Option.Try(() => int.Parse("42"));

        Assert.True(result.IsSome);
        Assert.Equal(42, result.ValueOr(0));
    }

    [Fact]
    public void Try_WhenFunctionThrows_ReturnsNone()
    {
        var result = Option.Try(() => int.Parse("not a number"));

        Assert.True(result.IsNone);
    }

    [Fact]
    public void Try_WhenFunctionReturnsValue_WrapsInSome()
    {
        var result = Option.Try(() => "hello");

        Assert.True(result.IsSome);
        Assert.Equal("hello", result.ValueOr(""));
    }

    [Fact]
    public void Try_WhenFunctionThrowsSpecificException_ReturnsNone()
    {
        var result = Option.Try<int>(() => throw new InvalidOperationException("test"));

        Assert.True(result.IsNone);
    }

    [Fact]
    public async Task TryAsync_WhenFunctionSucceeds_ReturnsSome()
    {
        var result = await Option.TryAsync(async () =>
        {
            await Task.Delay(1);
            return 42;
        });

        Assert.True(result.IsSome);
        Assert.Equal(42, result.ValueOr(0));
    }

    [Fact]
    public async Task TryAsync_WhenFunctionThrows_ReturnsNone()
    {
        var result = await Option.TryAsync<int>(async () =>
        {
            await Task.Delay(1);
            throw new InvalidOperationException("test");
        });

        Assert.True(result.IsNone);
    }

    [Fact]
    public async Task TryAsync_WhenTaskFaults_ReturnsNone()
    {
        var result = await Option.TryAsync(() => Task.FromException<int>(new Exception("fail")));

        Assert.True(result.IsNone);
    }
}
