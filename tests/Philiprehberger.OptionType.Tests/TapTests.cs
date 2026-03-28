using Xunit;
using Philiprehberger.OptionType;

namespace Philiprehberger.OptionType.Tests;

public class TapTests
{
    [Fact]
    public void Tap_OnSome_ExecutesAction()
    {
        var captured = 0;
        var option = Option.Some(42);

        var result = option.Tap(v => captured = v);

        Assert.Equal(42, captured);
        Assert.True(result.IsSome);
        Assert.Equal(42, result.ValueOr(0));
    }

    [Fact]
    public void Tap_OnNone_DoesNotExecuteAction()
    {
        var executed = false;
        var option = Option.None<int>();

        var result = option.Tap(_ => executed = true);

        Assert.False(executed);
        Assert.True(result.IsNone);
    }

    [Fact]
    public void Tap_ReturnsSameOption()
    {
        var option = Option.Some(42);

        var result = option.Tap(_ => { });

        Assert.Equal(option, result);
    }

    [Fact]
    public void TapNone_OnNone_ExecutesAction()
    {
        var executed = false;
        var option = Option.None<int>();

        var result = option.TapNone(() => executed = true);

        Assert.True(executed);
        Assert.True(result.IsNone);
    }

    [Fact]
    public void TapNone_OnSome_DoesNotExecuteAction()
    {
        var executed = false;
        var option = Option.Some(42);

        var result = option.TapNone(() => executed = true);

        Assert.False(executed);
        Assert.True(result.IsSome);
    }

    [Fact]
    public void Tap_ChainsCorrectly()
    {
        var tapCalled = false;
        var tapNoneCalled = false;

        var result = Option.Some(10)
            .Tap(v => tapCalled = true)
            .TapNone(() => tapNoneCalled = true)
            .Map(x => x * 2);

        Assert.True(tapCalled);
        Assert.False(tapNoneCalled);
        Assert.Equal(20, result.ValueOr(0));
    }
}
