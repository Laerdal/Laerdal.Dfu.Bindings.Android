# GitHub Copilot Instructions for Laerdal.Dfu.Bindings.Android

## Project Overview

**Laerdal.Dfu.Bindings.Android** is a thin, largely 1:1 .NET MAUI/Xamarin binding over Nordic
Semiconductor's native Android [`Android-DFU-Library`](https://github.com/NordicSemiconductor/Android-DFU-Library)
(currently `2.11.0`). Most consumers should depend on
[`Laerdal.Dfu`](https://github.com/Laerdal/Laerdal.Dfu) instead, which wraps this binding (plus
its iOS/MacCatalyst counterpart) behind a single cross-platform C# API — reach for this package
directly only when the raw Nordic Java API surface is actually needed.

## Technology Stack

- **Framework:** `net10.0-android` (SDK `10.0.100`, see `global.json`)
- **Type:** Xamarin/MAUI binding library (`IsBindingLibrary=true`), not ordinary C#
- **CI/CD:** GitHub Actions, including a dependency-submission workflow

## Project Structure

```
Laerdal.Dfu.Bindings.Android/
├── .github/
│   ├── workflows/ci.yml
│   ├── workflows/dependency-submission.yml
│   └── copilot-instructions.md          # This file
└── Laerdal.Dfu.Bindings.Android/
    ├── Laerdal.Dfu.Bindings.Android.csproj
    ├── Laerdal.targets                   # Shared build infra (download + binding config)
    └── Jars/                             # Downloaded at restore time, not checked in
        ├── dfu-<version>.aar             # Nordic's actual native library — the binding target
        └── gson-<version>.jar            # Classpath-only, excluded from the binding itself
```

## Build Mechanics (read before touching the `.csproj`)

- **The native `.aar` is downloaded, not vendored.** `_DownloadAndroidNativeFiles`
  (`BeforeTargets="Restore"`) pulls `dfu-$(Nordic_Package_Version).aar` from Maven Central.
  `AndroidLibrary`/`AndroidJavaLibrary` items glob at MSBuild *evaluation* time, before any
  target runs — any new download logic must hook `BeforeTargets="Restore"`, never
  `PrepareForBuild`, or the glob will miss the file on a clean checkout.
- **`Nordic_Package_Version` is the single source of truth for the version.** It drives the
  `.aar` download URL *and* the package version (`Laerdal_Version_Full =
  $(Nordic_Package_Version).$(Laerdal_Revision)`). `.github/workflows/ci.yml`'s version job reads
  this value directly out of the `.csproj` via `sed` — bumping it anywhere else won't be picked
  up by CI.
- **`TargetPlatformVersion` is deliberately NOT pinned.** It must float to whatever the
  building SDK/workload resolves as default, matching every consumer (`Laerdal.Dfu`,
  `Laerdal.Bluetooth.Firmware`, `fds-mobile-app`, ...). A hardcoded value here previously drifted
  ahead of what consumers resolve by default, which made this package's Android-specific `lib`
  folder invisible to those consumers under normal NuGet compatibility rules — NuGet silently
  fell back away from this package's assets with **no build warning**, shipping a non-functional
  Android build. Never reintroduce a hardcoded `TargetPlatformVersion` here.
- **`gson` is classpath-only.** It's needed to compile against Nordic's `.aar` but is explicitly
  `Remove`d from both `AndroidLibrary` and `AndroidJavaLibrary` so it doesn't get bound/packaged.
- **`Xamarin.AndroidX.LocalBroadcastManager` is pinned for a real reason.** Around December 2025,
  an `android16-sdk-build-tools` upgrade started producing missing-symbol errors for
  `Laerdal.Dfu` consumers (specifically Resuscis) without it, even though nothing else in the
  Nordic libs or build system had changed. Don't remove it as "unused."

## Coding Standards

Follow this repo's `.editorconfig`. Prefer clarity over cleverness; avoid unnecessary
abstraction — this is a firm preference, not a suggestion.

## Commit Message Format

`type (scope): short imperative`, <= 72 characters, matching the sibling repos in this DFU
family (`Laerdal.Dfu`, `Laerdal.Dfu.Bindings.iOS`).

## Useful Resources

- **Repository:** https://github.com/Laerdal/Laerdal.Dfu.Bindings.Android
- **NuGet Package:** https://www.nuget.org/packages/Laerdal.Dfu.Bindings.Android
- **Nordic Android-DFU-Library:** https://github.com/NordicSemiconductor/Android-DFU-Library
