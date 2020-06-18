[Setup]
AppName=EQMOD ASCOMPAD
AppVerName=EQMOD ASCOMPAD V2.07
AppVersion=2.07
OutputBaseFilename=ASCOMPAD_V207_Setup
AppPublisher=EQMOD Project <EQMOD-owner@yahoogroups.com>
AppPublisherURL=mailto:EQMOD-owner@yahoogroups.com
AppSupportURL=http://tech.groups.yahoo.com/group/EQMOD/
AppUpdatesURL=http://eq-mod.sourceforge.net/
MinVersion=0,5.0.2195sp4
DefaultDirName={pf}\EQMOD\ASCOMPAD
DisableDirPage=no
DefaultGroupName = EQMOD
DisableProgramGroupPage=no
OutputDir=.
Compression=lzma
SolidCompression=yes
UsePreviousSetupType=yes


[Languages]
Name: english; MessagesFile: compiler:Default.isl

[Dirs]
; TODO: Add subfolders below {app} as needed (e.g. Name: "{app}\MyFolder")

;  Add an option to install the source files
[Tasks]
Name: source; Description: Install the Source files; Flags: unchecked

[Files]
; Install EQASCOM driver - use ignoreversion to install over previous versions
Source: *.exe; DestDir: {app}; Flags: ignoreversion;
Source: *.wav; DestDir: {app}; Flags: ignoreversion;
;Source: *.dll; DestDir: {app}; Flags: ignoreversion;
;Source: *.lst   ; DestDir: {app}; Flags: ignoreversion;
;Source: *.pdf; DestDir: {app}; Flags: ignoreversion;
; Require a read-me HTML to appear after installation, maybe driver's Help doc
;Source: releasenotes.txt; DestDir: {app}; Flags: isreadme
Source: *; Excludes: *.zip,*.exe, *.pdf, \bin\*, \obj\*, \doc\*; DestDir: {app}\source; Tasks: source; Flags: recursesubdirs
; TODO: Add other files needed by your driver here (add subfolders above)

[Icons]
Name: "{group}\ASCOMPAD\ASCOMPAD"; Filename: "{app}\ascompad.EXE"; WorkingDir: "{app}"
Name: "{group}\EQTour\Uninstall"; Filename: "{uninstallexe}"


;Only if COM Local Server
[Run]

;Only if COM Local Server
[UninstallRun]

[Code]

