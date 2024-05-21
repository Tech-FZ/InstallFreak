using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace InstallFreak.Views;

public partial class IFP3 : UserControl
{
    string appName;
    string appVer;
    string appDl;
    string appSha256;
    string appSha512;
    string instPath;

    public IFP3(string selAppName, string selAppVer, string selDl, string selSha256, string selSha512, string selInstPath)
    {
        InitializeComponent();
        appName = selAppName;
        appVer = selAppVer;
        appDl = selDl;
        appSha256 = selSha256;
        appSha512 = selSha512;
        instPath = selInstPath;

        txtAppName.Text = $"App Name: {appName}";
        txtAppVer.Text = $"App Version: {appVer}";
        txtInstallPath.Text = $"Installation Path: {instPath}";
    }

    public void ChangeToSecondPage(object sender, RoutedEventArgs args)
    {
        Window mainwin = (Window)this.GetVisualRoot();
        mainwin.Content = new IFP2();
    }

    public void CloseProgram(object sender, RoutedEventArgs args)
    {
        Window mainwin = (Window)this.GetVisualRoot();
        mainwin.Close();
    }
}