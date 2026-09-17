---
name: laerdal-dfu-bindings-android
description: Use when writing, reviewing, or debugging code in this repo (Laerdal.Dfu.Bindings.Android — the thin Xamarin/MAUI binding over Nordic's native Android DFU library) or when bumping the wrapped Nordic native library version. Routes to this repo's existing Copilot instructions instead of re-deriving them, and flags a cross-repo coordination step that isn't written down in any single repo.
---

# Laerdal.Dfu.Bindings.Android orientation

Thin, largely 1:1 Xamarin/MAUI binding over Nordic Semiconductor's native Android
`Android-DFU-Library` `.aar`. Most consumers should depend on `Laerdal.Dfu` instead, which
wraps this binding (plus its iOS counterpart) behind one cross-platform API — reach for this
package directly only when the raw Nordic Java API surface is actually needed. Verify the
paths below still exist before trusting them; if they don't, this skill is stale, not the docs.

## Step 1 — do this now, before anything else

**Read `.github/copilot-instructions.md` in full, right now, before touching the `.csproj`,
`Laerdal.targets`, or anything under `Jars/`.** It already documents the load-bearing build
mechanics: the `.aar` is downloaded (not vendored) via a `BeforeTargets="Restore"` hook because
`AndroidLibrary`/`AndroidJavaLibrary` items glob at MSBuild *evaluation* time; `Nordic_Package_Version`
is the single source of truth CI reads via `sed`; `TargetPlatformVersion` is deliberately left
floating (a past hardcoded value silently broke NuGet asset resolution for every consumer, with
no build warning); and why `Xamarin.AndroidX.LocalBroadcastManager` is pinned. Everything below
this point only adds what that file doesn't cover.

## What's not written down in this repo alone — cross-repo Nordic version bumps

`Laerdal.Dfu.Bindings.Android` and `Laerdal.Dfu.Bindings.iOS` wrap **independently-versioned**
native libraries (`Android-DFU-Library` vs `IOS-Pods-DFU-Library`) — bumping this repo's Nordic
version does **not** imply bumping the iOS binding too. What *does* need re-checking whenever
either platform's native library moves is `Laerdal.Dfu`'s `NordicDfuUuids` (Legacy/Secure DFU
GATT constants) — re-verify it against the new native version before republishing, since
neither binding repo's own build would catch a drift there.

- This repo's own version pin: `Nordic_Package_Version`, defined once in
  `Laerdal.Dfu.Bindings.Android.csproj`.
- `Laerdal.Dfu.Bindings.iOS`'s equivalent pin is defined in **both**
  `Laerdal.Scripts/Laerdal.targets` and `Laerdal.Scripts/Laerdal.Builder.targets` there —
  letting those two drift apart breaks that repo's release step silently.
