# Changelog

## 0.2.0 (2026-03-28)

- Add async operations: `MapAsync`, `BindAsync`, `MatchAsync` for async pipeline composition
- Add `OrElse` with eager value and lazy factory overloads for fallback options
- Add `Try` and `TryAsync` static factories to wrap functions that may throw into `Option<T>`
- Add `Tap` and `TapNone` for executing side effects without changing the option value
- Add GitHub issue templates, dependabot configuration, and PR template
- Add missing README badges (GitHub release, Last updated, Bug Reports, Feature Requests)
- Add Support section to README

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
