Add-Type -AssemblyName PresentationFramework

$null = Show-MessageBox "InstallFreak was able to install EmuGUI. Please press OK to close the window." "InstallFreak - Setup successful"
[System.Windows.MessageBox]::Show('InstallFreak was able to install EmuGUI. Please press OK to close the window.', 'InstallFreak - Setup successful', 'OK', 'Information')