; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Kompano"
#define MyAppVersion "1.0.3"
#define MyAppPublisher "Symon Kipkemei"
#define MyAppURL "https://www.linkedin.com/in/symon-kipkemei/"


[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{0425B4F5-55DF-49B1-95D0-D67519A400D4}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={commonpf}\{#MyAppName}
DefaultGroupName={#MyAppName}
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=F:\BIMHABITAT\SOFTWARES\Kompano\installer
OutputBaseFilename=KompanoInstaller
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"


[Files]
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\bin\Release\*"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2020\Kompano"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\bin\Release\*"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2021\Kompano"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\bin\Release\*"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2022\Kompano"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\bin\Release\*"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2023\Kompano"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\bin\Release\*"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2024\Kompano"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\bin\Release\*"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2025\Kompano"; Flags: ignoreversion


; Add-in manifest files for various Revit versions
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\Kompano.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2020"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\Kompano.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2021"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\Kompano.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2022"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\Kompano.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2023"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\Kompano.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2024"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\Kompano\Kompano.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2025"; Flags: ignoreversion


; NOTE: Don't use "Flags: ignoreversion" on any shared system files




