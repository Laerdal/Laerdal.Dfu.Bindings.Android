## Summary

Describe the change in 2-6 lines.

## Why

Explain the reason for this change.

## Change Type

- [ ] Feature
- [ ] Bug fix
- [ ] Refactor (`refa`)
- [ ] Docs only
- [ ] CI/build/tooling

## Affected Areas

- [ ] Bindings (`Laerdal.Dfu.Bindings.Android.csproj` / `Laerdal.targets`)
- [ ] Native library version bump (Nordic `dfu-*.aar` / Gson `.jar`)
- [ ] Metadata transforms (`Transforms/*.xml`)
- [ ] CI/release pipeline
- [ ] Documentation

## Behavior And Compatibility

- [ ] Bound public API changed
- [ ] Wrapped Nordic library version changed
- [ ] No externally visible behavior change

If any box above is checked, describe impact:

## Tests

- [ ] Manual validation performed (against real hardware where applicable)
- [ ] Not applicable (explain)

Validation notes:

## Documentation

- [ ] Docs updated in same PR
- [ ] Not applicable (explain)

## Risks And Follow-ups

Risk level:
- [ ] Low
- [ ] Medium
- [ ] High

Known limitations or deferred follow-ups:
-

## Checklist

- [ ] Commit header follows `type(scope): short imperative` and is <= 72 chars
- [ ] Commit type is one of: feat, fix, refa, perf, docs, ci, chore, test, build
- [ ] Commit body is 1-2 factual sentences (what/why), no emojis, refs, or co-authors
