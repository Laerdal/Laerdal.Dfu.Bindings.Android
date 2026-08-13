# Laerdal.Dfu.Bindings.Android

[![CI](https://img.shields.io/github/actions/workflow/status/Laerdal/Laerdal.Dfu.Bindings.Android/ci.yml?branch=main&logo=github&label=build)](https://github.com/Laerdal/Laerdal.Dfu.Bindings.Android/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Laerdal.Dfu.Bindings.Android?logo=nuget&color=004880)](https://www.nuget.org/packages/Laerdal.Dfu.Bindings.Android/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Laerdal.Dfu.Bindings.Android?logo=nuget&color=004880)](https://www.nuget.org/packages/Laerdal.Dfu.Bindings.Android/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/github/license/Laerdal/Laerdal.Dfu.Bindings.Android?color=blue)](LICENSE)

A .NET MAUI/Xamarin binding library over Nordic Semiconductor's native Android DFU (Device
Firmware Update) SDK, letting .NET code drive firmware updates on Nordic-based Bluetooth Low
Energy devices over Android.

Native library wrapped: [Android-DFU-Library](https://github.com/NordicSemiconductor/Android-DFU-Library) (currently `2.11.0`).

This package is a thin, largely 1:1 binding of Nordic's Java API — most consumers should use
[`Laerdal.Dfu`](https://github.com/Laerdal/Laerdal.Dfu) instead, which wraps this binding (plus
its iOS/MacCatalyst counterpart) behind a single cross-platform C# API. Reach for this package
directly only if you need the raw Nordic API surface without `Laerdal.Dfu`'s abstraction.

## Platform Support

| Platform | Supported |
|----------|-----------|
| Android  | ✅ (API 21+) |
| Other    | ❌ — Android-only binding |

## Installation

```bash
dotnet add package Laerdal.Dfu.Bindings.Android
```

## Building Locally

```bash
git clone https://github.com/Laerdal/Laerdal.Dfu.Bindings.Android.git
cd Laerdal.Dfu.Bindings.Android
dotnet build Laerdal.Dfu.Bindings.Android/Laerdal.Dfu.Bindings.Android.csproj
```

Requires the .NET MAUI Android workload (`dotnet workload restore`) and a JDK (CI uses 17). The
build downloads Nordic's `.aar` and a matching Gson `.jar` straight from Maven Central during
`dotnet restore` — no local `gradle` setup needed, despite this being a Java-library binding.

## Known Issues

- **`Xamarin.AndroidX.LocalBroadcastManager` must stay an explicit dependency.** Since the Android
  SDK 36 build-tools upgrade (~December 2025), omitting this pin causes "missing symbol" errors in
  *consuming* apps at build time, even though nothing in this binding's own Java sources changed —
  see [PR #12](https://github.com/Laerdal/Laerdal.Dfu.Bindings.Android/pull/12).
- **The bundled Gson `.jar` is deliberately excluded from binding/packaging**
  (`Laerdal.Dfu.Bindings.Android.csproj`'s `AndroidLibrary`/`AndroidJavaLibrary Remove` items).
  This works around a duplicate-symbol conflict in Nordic's own library — see
  [Android-DFU-Library#428](https://github.com/NordicSemiconductor/Android-DFU-Library/issues/428).
  Removing the exclusion reintroduces the conflict.
- **`TargetPlatformVersion` here is pinned (currently `36`), not floating.** If it's ever bumped
  ahead of what consumers resolve to by default, NuGet will silently fall back away from this
  package's assets with no build warning — the exact failure mode that once made `Laerdal.Dfu`
  itself silently ship a non-functional Android build. Don't bump this without checking what
  `net10.0-android` resolves to by default across the current mobile stack first.

## License

[BSD 3-Clause](LICENSE)
