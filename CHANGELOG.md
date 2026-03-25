# Changelog

## 0.1.1 (2026-03-24)

- Add unit tests
- Add test step to CI workflow

## 0.1.0 (2026-03-22)

- Initial release
- `Option<T>` readonly struct with `IsSome` and `IsNone` properties
- `Map`, `Bind`, `Filter`, `Match`, `ValueOr`, `ValueOrThrow` methods
- Static factory methods: `Some<T>`, `None<T>`, `From<T>`
- LINQ support via `Select`, `SelectMany`, `Where`
- `ToOption()` extension for nullable types
- `Flatten()` for nested options
