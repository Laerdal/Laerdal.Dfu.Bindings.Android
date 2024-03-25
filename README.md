# Laerdal.Dfu.Bindings.Android

This is an Xamarin binding library for the Nordic Semiconductors Android library for updating the firmware of their devices over the air via Bluetooth Low Energy.

The Java library is located here: https://github.com/NordicSemiconductor/Android-DFU-Library

## Requirements

You'll need :

- **MacOS**
  - with **gradle**
  - with **Xamarin.Android**

## Steps to build on Local-Dev

### 1) Checkout

```bash
git clone https://github.com/Laerdal/Laerdal.Dfu.Bindings.Android.git
```

### 2) Restore

```bash
dotnet restore
```

### 2) Build

```bash
dotnet build
```

You'll find the nuget in `Output/`
