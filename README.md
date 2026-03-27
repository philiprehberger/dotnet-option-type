# Philiprehberger.OptionType

[![CI](https://github.com/philiprehberger/dotnet-option-type/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-option-type/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.OptionType.svg)](https://www.nuget.org/packages/Philiprehberger.OptionType)
[![License](https://img.shields.io/github/license/philiprehberger/dotnet-option-type)](LICENSE)
[![Sponsor](https://img.shields.io/badge/sponsor-GitHub%20Sponsors-ec6cb9)](https://github.com/sponsors/philiprehberger)

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

## API

### `Option` (static)

| Method | Description |
|--------|-------------|
| `Some<T>(T value)` | Create an Option containing a value |
| `None<T>()` | Create an empty Option |
| `From<T>(T? value)` | Create an Option from a nullable value |

### `Option<T>` (struct)

| Member | Description |
|--------|-------------|
| `IsSome` | True if the option contains a value |
| `IsNone` | True if the option is empty |
| `Map<U>(Func<T, U>)` | Transform the contained value |
| `Bind<U>(Func<T, Option<U>>)` | Chain option-returning functions |
| `Filter(Func<T, bool>)` | Keep value only if predicate holds |
| `Match<U>(Func<T, U>, Func<U>)` | Pattern match on the option |
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

## License

MIT
