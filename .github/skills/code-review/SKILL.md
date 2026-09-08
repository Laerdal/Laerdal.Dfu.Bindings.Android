---
name: code-review
description: Use for reviewing pull requests on Laerdal.Dfu.Bindings.Android — checks the floating-vs-pinned TargetPlatformVersion gotcha, download-target glob-timing correctness, the single-source-of-truth Nordic version, and the LocalBroadcastManager pin.
---

# Code Review — Laerdal.Dfu.Bindings.Android

## Checklist for changes in this repo

- [ ] **`TargetPlatformVersion` must stay unpinned.** Any PR that adds a hardcoded
      `TargetPlatformVersion` to `Laerdal.Dfu.Bindings.Android.csproj` is reintroducing a real,
      previously-shipped bug: it silently makes this package's Android `lib` folder invisible to
      consumers whose own `TargetPlatformVersion` resolves differently, with **no build
      warning** — NuGet just falls back away from the package. Reject this change outright unless
      there's a very deliberate, documented reason.
- [ ] **`Nordic_Package_Version` stays the single source of truth.** If a PR bumps the Nordic DFU
      library version, it should only touch this one property — check that
      `.github/workflows/ci.yml`'s version-reading `sed` command still matches, since it reads
      the value directly from the `.csproj` rather than from a shared variable.
- [ ] **New download/native-file logic hooks `BeforeTargets="Restore"`, not
      `PrepareForBuild`.** `AndroidLibrary`/`AndroidJavaLibrary` items glob at MSBuild evaluation
      time, before any target runs — a download hung off the wrong target will build fine
      incrementally but fail on a clean checkout/CI run.
- [ ] **`Xamarin.AndroidX.LocalBroadcastManager` stays.** It looks unused but fixed a real
      missing-symbol regression for Resusci/`Laerdal.Dfu` consumers after an
      `android16-sdk-build-tools` upgrade. Don't let a "remove unused package" cleanup PR drop it
      without checking this history first.
- [ ] **`gson` stays classpath-only.** It should remain `Remove`d from `AndroidLibrary`/
      `AndroidJavaLibrary` — it's there to compile against Nordic's `.aar`, not to be bound or
      shipped.

## What to flag as a real risk, not a nit

- This is a thin binding layer — behavior changes here ripple straight into `Laerdal.Dfu` and
  every consumer of it (`Laerdal.Bluetooth.Firmware`, `fds-mobile-app`). Treat any change to the
  bound API surface as a breaking-change candidate, not a routine tweak.
