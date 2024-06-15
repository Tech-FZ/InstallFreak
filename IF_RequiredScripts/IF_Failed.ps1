Add-Type -AssemblyName PresentationFramework

[System.Windows.MessageBox]::Show('InstallFreak failed to install EmuGUI and therefore reverted all changes it made. Please press OK to close the window.', 'InstallFreak - Setup failed', 'OK', 'Error')