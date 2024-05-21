using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using InstallFreak.DataModel;

namespace InstallFreak.Views;

public partial class IFP2 : UserControl
{
    //MainWindow mainwin;

    public IFP2()
    {
        InitializeComponent();
        //mainwin = recMainWin;
    }

    public void ChangeToFirstPage(object sender, RoutedEventArgs args)
    {
        Window mainwin = (Window)this.GetVisualRoot();
        mainwin.Content = new IFP1();
    }

    public void ChangeToThirdPage(object sender, RoutedEventArgs args)
    {
        Window mainwin = (Window)this.GetVisualRoot();
        ObservableCollection<InstallFreakApps> ifapps = (ObservableCollection<InstallFreakApps>)ifAppList.ItemsSource;

        string selAppName;
        string selAppVer;
        string selDownload;
        string selSha256;
        string selSha512;
        string selInstallPath;

        foreach (InstallFreakApps ifapp in ifapps) {
            if (ifapp.ToInstall) {
                selAppName = ifapp.AppName;
                selAppVer = ifapp.AppVer;
                selDownload = ifapp.AppDL;
                selSha256 = ifapp.AppSHA256;
                selSha512 = ifapp.AppSHA512;
                selInstallPath = txtBInstPath.Text;
                mainwin.Content = new IFP3(selAppName, selAppVer, selDownload, selSha256, selSha512, selInstallPath);
            }
        }
    }

    public void CloseProgram(object sender, RoutedEventArgs args)
    {
        Window mainwin = (Window)this.GetVisualRoot();
        mainwin.Close();
    }
}