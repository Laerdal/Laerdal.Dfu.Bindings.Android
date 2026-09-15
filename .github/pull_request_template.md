## Summary

Describe the change in 2-6 lines.

## Why

Explain the reason for this change.

## Change Type

- [ ] Feature
- [ ] Bug fix
- [ ] Refactor
- [ ] Docs only
- [ ] CI/build/tooling

## Affected Areas

- [ ] Native binding (`Laerdal.Dfu.Bindings.Android.csproj`)
- [ ] Shared build config (`Laerdal.targets`)
- [ ] Central package management (`Directory.Packages.props`)
- [ ] Nordic DFU `.aar` version bump
- [ ] Gson dependency handling
- [ ] Transforms (`Transforms/`)
- [ ] Documentation

## Behavior And Compatibility

- [ ] Public API changed
- [ ] Native library version bumped (Nordic DFU / Gson)
- [ ] No externally visible behavior change

If any box above is checked, describe impact:

## Tests

- [ ] Manual validation performed (against real hardware/emulator)
- [ ] Not applicable (explain)

Validation notes:

## Documentation

- [ ] README "Known issues" / version notes updated
- [ ] Not applicable (explain)

## Checklist

- [ ] Commit header follows `type(scope): short imperative` and is <= 72 chars
- [ ] Commit type is one of: feat, fix, refa, perf, docs, ci, chore, test, build
- [ ] Commit body is 1-2 factual sentences (what/why), no emojis, refs, or co-authors
- [ ] CI passes
- [ ] Change is scoped to one logical unit of work
