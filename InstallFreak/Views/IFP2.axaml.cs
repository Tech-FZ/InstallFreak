using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
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

    public async Task<string?> BrowseForFolderAsync()
    {
        /* Window mainwin = (Window)this.GetVisualRoot();
        var storageProv = mainwin.StorageProvider; */
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Select install folder",
            AllowMultiple = false,
        });

        if (files.Count >= 1)
        {
            var selFolder = files[0];
            string folderPath = selectedFolder.Path.LocalPath;
            return folderPath;
        }

        else {
            return null;
        }
    }

    public async void BrowseForFolderAsync(object sender, RoutedEventArgs args) {
        string? folderPath = await BrowseForFolderAsync();

        if (folderPath != null) {
            txtBInstPath.Text = folderPath;
        }
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