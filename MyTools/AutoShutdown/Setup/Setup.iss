; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "自动关机小工具"
#define MyAppExeName "AutoShutdown.exe"
#define MyAppVersion GetFileVersion('..\bin\Debug\AutoShutdown.exe')
#define MyAppPublisher "zf"
#define MyAppURL "https://github.com/sudazf/MyTools"
#define MyAppAssocName "AutoShutdownTool"
#define MyAppAssocExt ".myp"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{50DB5A48-CE10-48FC-9950-A811B326DF3F}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\AutoShutdownTool
; "ArchitecturesAllowed=x64compatible" specifies that Setup cannot run
; on anything but x64 and Windows 11 on Arm.
ArchitecturesAllowed=x64compatible
; "ArchitecturesInstallIn64BitMode=x64compatible" requests that the
; install be done in "64-bit mode" on x64 or Windows 11 on Arm,
; meaning it should use the native 64-bit Program Files directory and
; the 64-bit view of the registry.
ArchitecturesInstallIn64BitMode=x64compatible
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
OutputBaseFilename={#MyAppName} {#MyAppVersion} Installer
SetupIconFile=..\bin\Debug\Resources\Icon\Strawberry.ico 
Compression=zip
;SolidCompression=yes
WizardStyle=modern
WizardImageFile=..\bin\Debug\Resources\Bmp\Left.bmp 

;设置控制面板中程序图标
UninstallDisplayIcon={app}\Resources\Icon\Strawberry.ico 
;设置控制面板中程序的名称
Uninstallable=yes
UninstallDisplayName={#MyAppName} {#MyAppVersion}

;安装时删除指定文件或文件夹
[InstallDelete]
Name: {app}; Type: filesandordirs

;卸载时删除指定文件或文件夹
[UninstallDelete]
Name: {app}; Type: filesandordirs

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "chinesesimplified"; MessagesFile: "compiler:Languages\ChineseSimplified.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\bin\Debug\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Code]
//程序启动函数
function InitializeSetup(): Boolean;
  var ResultCode: Integer;
  begin
  //判断是否安装了NETFramework4.7.2  大家注意，正确是SOFTWARE\\Microsoft\\.NETFramework\\v4.0.30319\\SKUs\\.NETFramework,Version=v4.7.2
  if RegKeyExists(HKLM, 'SOFTWARE\Microsoft\.NETFramework\v4.0.30319\SKUs\.NETFramework,Version=v4.7.2') then
    begin
    //返回值 继续执行
    //MsgBox('系统检测到您已安装.Net Framework4.7.2运行环境', mbConfirmation, MB_OK)
    Result :=true;
    end
  else
    //若用户没有安装
    begin
    //弹出一个对话框
    if MsgBox('系统检测到您没有安装.Net Framework4.7.2运行环境，是否立即安装？', mbConfirmation, MB_YESNO) = idYes then
      begin
        //要把.net安装包，从我们的安装包中取出来，放到系统的临时文件夹中
        ExtractTemporaryFile('NDP472-KB4054530-x86-x64-AllOS-ENU.exe');
  
        //我们再运行这个安装包(运行1个.exe文件)
        Exec(ExpandConstant('{tmp}\NDP472-KB4054530-x86-x64-AllOS-ENU.exe'), '', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode);

        //返回值 继续执行
        Result :=true;
      end
    else 
      begin
        MsgBox('未能成功安装.Net Framework4.7.2运行环境，系统将无法运行，本安装程序即将退出！',mbInformation,MB_OK);
        Result := false;
      end
  end;
  
end;

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

