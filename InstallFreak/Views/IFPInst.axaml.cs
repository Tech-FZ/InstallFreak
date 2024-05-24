using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Net;

namespace InstallFreak.Views;

public partial class IFPInst : UserControl
{
    string appName;
    string appVer;
    string appDl;
    string appSha256;
    string appSha512;
    string instPath;
    string downloadLocation;

    public void CreateFolder() {
        txtCurTask.Text = $"Preparing installation path \"{instPath}\"";
        Directory.CreateDirectory(instPath);
    }

    public void SetDownloadLoc() {
        txtCurTask.Text = "Setting download location";
        downloadLocation = $"{Environment.SpecialFolder.UserProfile}\\AppData\\Local\\Temp\\IF-";

        string[] letterlist = {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
            "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N",
            "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };

        Random random = new Random();

        for (int i = 0; i <= 32; i++) {
            int rndmIdx = random.Next(0, letterlist.Length);
            downloadLocation = $"{downloadLocation}{letterlist[rndmIdx]}";
        }
    }

    public void DownloadProgFile() {
        txtCurTask.Text = "Downloading program files";
        var appUrl = new Uri(appDl);

        try {
            using (var client = new WebClient()) {
                client.DownloadFile(appUrl, downloadLocation);
            }
        }

        catch (Exception ex) {
            
        }
    }

    public void InstallProg() {
        CreateFolder();
        SetDownloadLoc();
        DownloadProgFile();
    }

    public IFPInst(string selAppName, string selAppVer, string selDl, string selSha256, string selSha512, string selInstPath)
    {
        InitializeComponent();
        appName = selAppName;
        appVer = selAppVer;
        appDl = selDl;
        appSha256 = selSha256;
        appSha512 = selSha512;
        instPath = selInstPath;
        InstallProg();
    }
}