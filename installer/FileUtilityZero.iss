; Inno Setup script for FileUtilityZero.
;
; Packages the self-contained, single-file win-x64 publish output (see
; FileUtilityZero.UI/Properties/PublishProfiles/win-x64-selfcontained.pubxml
; and BUILDING.md) into a standalone Setup.exe. Build the publish output
; FIRST, then compile this script - see BUILDING.md for both commands.
;
; Compile with (from the installer/ folder, or give ISCC the full path):
;   ISCC FileUtilityZero.iss
;
; MyAppName/MyAppVersion/MyAppPublisher below are placeholders - edit them
; for your own release. MyAppVersion in particular is NOT read from the
; .csproj automatically; keep it in sync by hand with <Version> in
; FileUtilityZero.UI.csproj when cutting a release.
#define MyAppName "File Utility Zero"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Sal Delvisco"
#define MyAppExeName "FileUtilityZero.exe"
#define MyAppPublishDir "..\FileUtilityZero.UI\bin\Release\net10.0-windows\win-x64\publish"

[Setup]
; This GUID identifies the app across versions - it's what Inno Setup uses
; (not the app name) to recognize an existing install for upgrade/uninstall
; purposes. Keep it exactly as-is in every future release; only generate a
; new one (Tools > Generate GUID in the Inno Setup IDE) if you deliberately
; want a new install that doesn't upgrade/replace this one.
AppId={{48D34619-7DE7-4895-B9DE-C54B9759779A}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
; This is a self-contained win-x64-only build (see Stage 1) - restrict the
; installer to match rather than let it appear installable elsewhere.
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputDir=Output
OutputBaseFilename=FileUtilityZeroSetup-{#MyAppVersion}
Compression=lzma
SolidCompression=yes
WizardStyle=modern
UninstallDisplayIcon={app}\{#MyAppExeName}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
; Pulls in the whole publish output (the single exe, plus its .pdb symbol
; files) recursively, in case a future dependency ever needs to drop
; additional loose files alongside the exe. Debug symbols aren't needed to
; run the app, so they're excluded from what actually ships.
Source: "{#MyAppPublishDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirfolders; Excludes: "*.pdb"

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Uninstall {#MyAppName}"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
