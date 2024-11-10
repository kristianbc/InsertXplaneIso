[Setup]
AppName=InsertXplaneIso
AppVersion=1.0
DefaultDirName={pf}\InsertXplaneIso
DefaultGroupName=InsertXplaneIso
OutputDir=.\Output
OutputBaseFilename=InsertXplaneIsoSetup
Compression=lzma
SolidCompression=yes
SetupIconFile=C:\Users\Kristian\source\repos\InsertXplaneIso\InsertXplaneIso\bin\Release\net8.0\xplane.ico
DisableDirPage=no
[Files]
Source: "C:\Users\Kristian\source\repos\InsertXplaneIso\InsertXplaneIso\bin\Release\net8.0\InsertXplaneIso.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Kristian\source\repos\InsertXplaneIso\InsertXplaneIso\bin\Release\net8.0\InsertXplaneIso.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Kristian\source\repos\InsertXplaneIso\InsertXplaneIso\bin\Release\net8.0\InsertXplaneIso.dll"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{userdesktop}\InsertXplaneIso"; Filename: "{app}\InsertXplaneIso.exe"
Name: "{userstartmenu}\InsertXplaneIso"; Filename: "{app}\InsertXplaneIso.exe"

[Run]
Filename: "{app}\InsertXplaneIso.exe"; Description: "Launch InsertXplaneIso"; Flags: nowait postinstall skipifsilent
