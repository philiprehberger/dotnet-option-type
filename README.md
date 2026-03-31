# Philiprehberger.OptionType

[![CI](https://github.com/philiprehberger/dotnet-option-type/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-option-type/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.OptionType.svg)](https://www.nuget.org/packages/Philiprehberger.OptionType)
[![Last updated](https://img.shields.io/github/last-commit/philiprehberger/dotnet-option-type)](https://github.com/philiprehberger/dotnet-option-type/commits/main)

Option/Maybe monad for explicit null handling with Map, Bind, Match, and LINQ support.

## Installation

```bash
dotnet add package Philiprehberger.OptionType
```

## Usage

### Creating Options

```csharp
using Philiprehberger.OptionType;

var some = Option.Some(42);
var none = Option.None<int>();
var fromNullable = Option.From<string>(null); // None
var fromValue = Option.From("hello");         // Some("hello")
```

### Transforming Values

```csharp
var doubled = Option.Some(21).Map(x => x * 2);  // Some(42)

var parsed = Option.Some("123")
    .Bind(s => int.TryParse(s, out var n) ? Option.Some(n) : Option.None<int>());

var positive = Option.Some(42).Filter(x => x > 0);  // Some(42)
var negative = Option.Some(-1).Filter(x => x > 0);  // None
```

### Pattern Matching

```csharp
string message = Option.Some(42).Match(
    some: v => $"Got {v}",
    none: () => "Nothing"
);

int value = none.ValueOr(0);           // 0
int must = some.ValueOrThrow();        // 42
```

### LINQ Query Syntax

```csharp
var result = from x in Option.Some(10)
             from y in Option.Some(20)
             select x + y;  // Some(30)

var filtered = from x in Option.Some(42)
               where x > 0
               select x;  // Some(42)
```

### Nullable Conversion

```csharp
int? nullable = 42;
var option = nullable.ToOption();  // Some(42)

int? nullValue = null;
var noneOption = nullValue.ToOption();  // None
```

### Async Operations

```csharp
var result = await Option.Some(42)
    .MapAsync(async x => await FetchMultiplierAsync(x));

var bound = await Option.Some("user-123")
    .BindAsync(async id => await FindUserAsync(id));

var message = await Option.Some(42)
    .MatchAsync(
        some: async v => await FormatAsync(v),
        none: () => Task.FromResult("not found")
    );
```

### Fallback with OrElse

```csharp
var primary = Option.None<int>();
var result = primary.OrElse(Option.Some(99));  // Some(99)

var lazy = primary.OrElse(() => Option.Some(ComputeDefault()));  // factory only called when None
```

### Safe Execution with Try

```csharp
var parsed = Option.Try(() => int.Parse("42"));         // Some(42)
var failed = Option.Try(() => int.Parse("not a number")); // None

var fetched = await Option.TryAsync(async () => await FetchDataAsync());
```

### Side Effects with Tap

```csharp
var result = Option.Some(42)
    .Tap(v => Console.WriteLine($"Processing: {v}"))
    .Map(x => x * 2)
    .TapNone(() => Console.WriteLine("No value found"));
```

## API

### `Option` (static)

| Method | Description |
|--------|-------------|
| `Some<T>(T value)` | Create an Option containing a value |
| `None<T>()` | Create an empty Option |
| `From<T>(T? value)` | Create an Option from a nullable value |
| `Try<T>(Func<T>)` | Wrap a function that may throw into Some or None |
| `TryAsync<T>(Func<Task<T>>)` | Wrap an async function that may throw into Some or None |

### `Option<T>` (struct)

| Member | Description |
|--------|-------------|
| `IsSome` | True if the option contains a value |
| `IsNone` | True if the option is empty |
| `Map<U>(Func<T, U>)` | Transform the contained value |
| `Bind<U>(Func<T, Option<U>>)` | Chain option-returning functions |
| `Filter(Func<T, bool>)` | Keep value only if predicate holds |
| `Match<U>(Func<T, U>, Func<U>)` | Pattern match on the option |
| `MapAsync<U>(Func<T, Task<U>>)` | Asynchronously transform the contained value |
| `BindAsync<U>(Func<T, Task<Option<U>>>)` | Asynchronously chain option-returning functions |
| `MatchAsync<U>(Func<T, Task<U>>, Func<Task<U>>)` | Asynchronously pattern match on the option |
| `OrElse(Option<T>)` | Return this option or the fallback |
| `OrElse(Func<Option<T>>)` | Return this option or invoke the fallback factory |
| `Tap(Action<T>)` | Execute a side effect when Some |
| `TapNone(Action)` | Execute a side effect when None |
| `ValueOr(T)` | Get value or default |
| `ValueOrThrow()` | Get value or throw InvalidOperationException |
| `ToString()` | String representation |
| `Equals(Option<T>)` | Value equality |
| `GetHashCode()` | Hash code |

### Extension Methods

| Method | Description |
|--------|-------------|
| `Select` | LINQ select support (maps value) |
| `SelectMany` | LINQ multi-from support (binds values) |
| `Where` | LINQ where support (filters value) |
| `ToOption()` | Convert nullable to Option |
| `Flatten()` | Unwrap nested `Option<Option<T>>` |

## Development

```bash
dotnet build src/Philiprehberger.OptionType.csproj --configuration Release
```

## Support

If you find this project useful:

⭐ [Star the repo](https://github.com/philiprehberger/dotnet-option-type)

🐛 [Report issues](https://github.com/philiprehberger/dotnet-option-type/issues?q=is%3Aissue+is%3Aopen+label%3Abug)

💡 [Suggest features](https://github.com/philiprehberger/dotnet-option-type/issues?q=is%3Aissue+is%3Aopen+label%3Aenhancement)

❤️ [Sponsor development](https://github.com/sponsors/philiprehberger)

🌐 [All Open Source Projects](https://philiprehberger.com/open-source-packages)

💻 [GitHub Profile](https://github.com/philiprehberger)

🔗 [LinkedIn Profile](https://www.linkedin.com/in/philiprehberger)

## License

[MIT](LICENSE)
