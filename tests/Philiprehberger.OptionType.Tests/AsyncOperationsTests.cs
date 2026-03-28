using Xunit;
using Philiprehberger.OptionType;

namespace Philiprehberger.OptionType.Tests;

public class AsyncOperationsTests
{
    [Fact]
    public async Task MapAsync_OnSome_TransformsValue()
    {
        var option = Option.Some(5);

        var result = await option.MapAsync(async x =>
        {
            await Task.Delay(1);
            return x * 2;
        });

        Assert.True(result.IsSome);
        Assert.Equal(10, result.ValueOr(0));
    }

    [Fact]
    public async Task MapAsync_OnNone_ReturnsNone()
    {
        var option = Option.None<int>();

        var result = await option.MapAsync(async x =>
        {
            await Task.Delay(1);
            return x * 2;
        });

        Assert.True(result.IsNone);
    }

    [Fact]
    public async Task MapAsync_OnSome_TransformsToString()
    {
        var option = Option.Some(42);

        var result = await option.MapAsync(x => Task.FromResult(x.ToString()));

        Assert.True(result.IsSome);
        Assert.Equal("42", result.ValueOr(""));
    }

    [Fact]
    public async Task BindAsync_OnSome_ReturnsBinderResult()
    {
        var option = Option.Some(10);

        var result = await option.BindAsync(async x =>
        {
            await Task.Delay(1);
            return x > 5 ? Option.Some(x.ToString()) : Option.None<string>();
        });

        Assert.True(result.IsSome);
        Assert.Equal("10", result.ValueOr(""));
    }

    [Fact]
    public async Task BindAsync_OnNone_ReturnsNone()
    {
        var option = Option.None<int>();

        var result = await option.BindAsync(x => Task.FromResult(Option.Some(x * 2)));

        Assert.True(result.IsNone);
    }

    [Fact]
    public async Task BindAsync_OnSome_WhenBinderReturnsNone_ReturnsNone()
    {
        var option = Option.Some(3);

        var result = await option.BindAsync(async x =>
        {
            await Task.Delay(1);
            return x > 5 ? Option.Some(x) : Option.None<int>();
        });

        Assert.True(result.IsNone);
    }

    [Fact]
    public async Task MatchAsync_OnSome_CallsSomeFunction()
    {
        var option = Option.Some(42);

        var result = await option.MatchAsync(
            some: v => Task.FromResult($"got {v}"),
            none: () => Task.FromResult("nothing")
        );

        Assert.Equal("got 42", result);
    }

    [Fact]
    public async Task MatchAsync_OnNone_CallsNoneFunction()
    {
        var option = Option.None<int>();

        var result = await option.MatchAsync(
            some: v => Task.FromResult($"got {v}"),
            none: () => Task.FromResult("nothing")
        );

        Assert.Equal("nothing", result);
    }

    [Fact]
    public async Task MatchAsync_OnSome_ExecutesAsyncWork()
    {
        var option = Option.Some(10);

        var result = await option.MatchAsync(
            some: async v =>
            {
                await Task.Delay(1);
                return v + 5;
            },
            none: () => Task.FromResult(0)
        );

        Assert.Equal(15, result);
    }
}
