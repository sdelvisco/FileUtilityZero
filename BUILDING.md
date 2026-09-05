# Building a Windows Installer

This document is for maintainers cutting a release, not for end users
running the app — see the Usage section in [README.md](README.md) for that.

Producing a distributable `Setup.exe` is two steps: publish a self-contained
build of the app, then compile that build into an installer with Inno Setup.

## Requirements

- Windows, with the .NET 10 SDK installed
- [Inno Setup](https://jrsoftware.org/isinfo.php) (6.x recommended), so its
  compiler, `ISCC.exe`, is on your `PATH`

## 1. Publish a self-contained build

From the repository root:

```
dotnet publish FileUtilityZero.UI\FileUtilityZero.UI.csproj -c Release -p:PublishProfile=win-x64-selfcontained
```

This uses the profile at
`FileUtilityZero.UI/Properties/PublishProfiles/win-x64-selfcontained.pubxml`
and is equivalent to:

```
dotnet publish FileUtilityZero.UI\FileUtilityZero.UI.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

Either produces a self-contained, single-file win-x64 build at
`FileUtilityZero.UI\bin\Release\net10.0-windows\win-x64\publish\` — no
separate .NET runtime install is required on the machine that eventually
runs it.

## 2. Compile the installer

From the repository root (or from inside `installer\`, either works — the
script's paths are relative to itself):

```
ISCC installer\FileUtilityZero.iss
```

This packages the publish output from step 1 into
`installer\Output\FileUtilityZeroSetup-<version>.exe`.

## Keeping the version in sync

The app's version has two independent places it needs to be updated for
each release — neither is read from the other automatically:

1. `<Version>` in `FileUtilityZero.UI.csproj`
2. `MyAppVersion` (a `#define` near the top of `installer\FileUtilityZero.iss`)

Update both before running the two commands above.

## Other installer placeholders

`installer\FileUtilityZero.iss` also defines `MyAppName` and
`MyAppPublisher` near the top — edit these if you want the installer to
show a different display name or publisher. The `AppId` GUID further down,
by contrast, should stay exactly as-is across releases: it's what lets
Inno Setup recognize a new Setup.exe as an upgrade of a previous install
rather than a separate, parallel one.
