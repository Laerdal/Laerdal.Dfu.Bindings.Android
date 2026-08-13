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
- **The bundled Gson `.jar` is downloaded but deliberately excluded from binding/packaging**
  (`Laerdal.Dfu.Bindings.Android.csproj`'s `AndroidLibrary`/`AndroidJavaLibrary Remove` items) —
  Nordic's own `.aar` needs Gson on the classpath to compile against, but this project doesn't want
  to ship a full Gson C# binding alongside it. The csproj comment citing
  [Android-DFU-Library#428](https://github.com/NordicSemiconductor/Android-DFU-Library/issues/428)
  as the reason is **likely a mislabeled reference** — that issue is about an unrelated `values.xml`
  string-formatting build error, not Gson. The real history is closer to
  [#218](https://github.com/NordicSemiconductor/Android-DFU-Library/issues/218) (a long-since-fixed
  missing-Gson-in-POM issue from 2019), but that doesn't explain the *exclusion* either. Don't treat
  either issue number as authoritative without further digging; don't remove the exclusion without
  testing first.

## License

[BSD 3-Clause](LICENSE)
