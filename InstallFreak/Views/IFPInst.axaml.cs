using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Net;
using OctaneEngine;
using OctaneEngineCore;
using Serilog;
using ILogger = Serilog.ILogger;
using System.Threading;
using Microsoft.Extensions.Logging;
using Autofac;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Security.Cryptography;
using System.IO.Compression;
using Avalonia.VisualTree;
using System.ComponentModel;
using Avalonia.Threading;
using System.Diagnostics;
using IWshRuntimeLibrary;

namespace InstallFreak.Views;

public partial class IFPInst : UserControl
{
    string? appName;
    string? appVer;
    string? appDl;
    string? appSha256;
    string? appSha512;
    string? instPath;
    string? downloadLocation;
    bool? startMenShc;
    bool? deskShc;
    Window mainwin;
    bool failed = false;

    string[] letterlist = {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
            "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N",
            "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };

    private void SetCurTaskText(string text) => txtCurTask.Text = text;
    private void SetHeaderText(string text) => txtHeader.Text = text;
    private void SetPatText(string text) => txtPatience.Text = text;

    private void InstSuccess() {
        Dispatcher.UIThread.Post(() => SetHeaderText("Installation succeeded!"));
        Dispatcher.UIThread.Post(() => SetPatText("InstallFreak was able to install EmuGUI."));
        Dispatcher.UIThread.Post(() => SetCurTaskText("Please click on the Close button on the title bar to exit."));
    }
    private void InsFailWinChange(string rsFail) {
        Dispatcher.UIThread.Post(() => SetHeaderText("Installation failed!"));
        Dispatcher.UIThread.Post(() => SetPatText("InstallFreak was not able to install EmuGUI. All changes have been reverted."));
        Dispatcher.UIThread.Post(() => SetCurTaskText("Please click on the Close button on the title bar to exit."));
    }

    public void CleanUpTemp() {
        Dispatcher.UIThread.Post(() => SetCurTaskText("Cleaning up temporary files"));
        //txtCurTask.Text = "Cleaning up temporary files";

        try {
            Directory.Delete(downloadLocation, true);
        }
        
        catch {

        }
    }

    public void InstFail(string rsFail) {
        failed = true;
        Dispatcher.UIThread.Post(() => SetHeaderText("Installation failed! Reverting changes..."));
        Dispatcher.UIThread.Post(() => SetCurTaskText("Deleting program files"));

        try {
            Directory.Delete(instPath, true);
        }
        
        catch {

        }

        CleanUpTemp();
        Dispatcher.UIThread.Post(() => InsFailWinChange(rsFail));
    }

    public void CreateFolder() {
        Dispatcher.UIThread.Post(() => SetCurTaskText($"Preparing installation path \"{instPath.ToString()}\""));

        try {
            Directory.CreateDirectory(instPath);
        }
        
        catch (Exception ex) {
            InstFail(ex.Message);
        }
    }

    public void SetDownloadLoc() {
        Dispatcher.UIThread.Post(() => SetCurTaskText("Setting download location"));
        downloadLocation = $"{Environment.SpecialFolder.ApplicationData.ToString()}\\IF-EmuGUI";

        try {
            Directory.CreateDirectory(downloadLocation);
        }
        
        catch (Exception ex) {
            InstFail(ex.Message);
        }
    }

    public void DownloadEngine(string toDownload, string saveTo) {
        PauseTokenSource? _pauseTokenSource;
        CancellationTokenSource? _cancelTokenSource;
        ILogger _log;
        ILoggerFactory? _factory;
        Random random = new Random();
        string logid = "";

        for (int i = 0; i <= 32; i++) {
            int rndmIdx = random.Next(0, letterlist.Length);
            logid = $"{logid}{letterlist[rndmIdx]}";
        }

        _log = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Verbose()
            .WriteTo.File($"{downloadLocation}/IFLog-{logid}.txt")
            .WriteTo.Console()
            .CreateLogger();

        _factory = LoggerFactory.Create(logging =>
        {
            logging.AddSerilog(_log);
        });
            
        _pauseTokenSource = new PauseTokenSource(_factory);
        _cancelTokenSource = new CancellationTokenSource();

        _log.Information("Starting File Download Test");

        try
        {
            var config = new OctaneConfiguration
            {
                Parts = 2,
                BufferSize = 8192,
                ShowProgress = false,
                DoneCallback = _ =>
                {
                    Console.WriteLine("Done!");
                    Assert.That(System.IO.File.Exists(saveTo));
                },
                ProgressCallback = Console.WriteLine,
                NumRetries = 20,
                BytesPerSecond = 1,
                UseProxy = false,
                Proxy = null
            };

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(_factory).As<ILoggerFactory>();
            containerBuilder.RegisterInstance(config).As<OctaneConfiguration>();
            containerBuilder.AddOctane();
            var engineContainer = containerBuilder.Build();
            var engine = engineContainer.Resolve<IEngine>();
            engine.DownloadFile(toDownload, saveTo, _pauseTokenSource, _cancelTokenSource).Wait();
        }

        catch (Exception ex) {
            InstFail(ex.Message);
        }
    }

    public void DownloadProgFile() {
        Dispatcher.UIThread.Post(() => SetCurTaskText("Downloading program files"));

        try {
            DownloadEngine(appDl, $"{downloadLocation}/{appName}_{appVer}.zip");
        }

        catch (Exception ex) {
            InstFail(ex.Message);
        }
    }

    public void VerifySHA256() {
        if (appSha256 != "" && appSha256 != null) {
            Dispatcher.UIThread.Post(() => SetCurTaskText("Verifying file with SHA256"));

            try {
                DownloadEngine(appSha256, $"{downloadLocation}/{appName}_{appVer}.zip.sha256");
                using StreamReader reader = new($"{downloadLocation}/{appName}_{appVer}.zip.sha256");
                string text = reader.ReadToEnd();
                string[] shaSplit = text.Split("  ");
                FileStream filestream;
                SHA256 sha256mod = SHA256.Create();
                filestream = new FileStream($"{downloadLocation}/{appName}_{appVer}.zip", FileMode.Open);
                filestream.Position = 0;
                byte[] hashValue = sha256mod.ComputeHash(filestream);
                string hashStr = BitConverter.ToString(hashValue).Replace("-", String.Empty);
                
                if (hashStr != shaSplit[0]) {
                    InstFail("SHA256 hashes don't match");
                }

                filestream.Close();
            }

            catch (Exception ex) {
                InstFail(ex.Message);
            }
        }
    }

    public void VerifySHA512() {
        if (appSha512 != "" && appSha512 != null) {
            Dispatcher.UIThread.Post(() => SetCurTaskText("Verifying file with SHA512"));

            try {
                DownloadEngine(appSha512, $"{downloadLocation}/{appName}_{appVer}.zip.sha512");
                using StreamReader reader = new($"{downloadLocation}/{appName}_{appVer}.zip.sha512");
                string text = reader.ReadToEnd();
                string[] shaSplit = text.Split("  ");
                FileStream filestream;
                SHA512 sha512mod = SHA512.Create();
                filestream = new FileStream($"{downloadLocation}/{appName}_{appVer}.zip", FileMode.Open);
                filestream.Position = 0;
                byte[] hashValue = sha512mod.ComputeHash(filestream);
                string hashStr = BitConverter.ToString(hashValue).Replace("-", String.Empty);
                
                if (hashStr != shaSplit[0]) {
                    InstFail("SHA512 hashes don't match");
                }

                filestream.Close();
            }

            catch (Exception ex) {
                InstFail(ex.Message);
            }
        }
    }

    public void VerifyPkg() {
        Dispatcher.UIThread.Post(() => SetCurTaskText("Checking for verification files"));
        VerifySHA256();
        VerifySHA512();
    }

    public void ExtractProg() {
        Dispatcher.UIThread.Post(() => SetCurTaskText("Extracting program files"));
        
        try {
            ZipFile.ExtractToDirectory($"{downloadLocation}/{appName}_{appVer}.zip", instPath);
        }

        catch (Exception ex) {
            InstFail(ex.Message);
        }
        
    }

    public void CreateShortcut(string shctype)
    {
        if (shctype == "desktop") {
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Desktop\\EmuGUI.lnk");
            shortcut.TargetPath = $"{instPath}\\EmuGUI_v{appVer}_Win_amd64\\emugui.exe";
            shortcut.Description = "Making QEMU emulation easier";
            shortcut.Save();
        }
        
        else if (shctype == "start") {
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut($"{Environment.GetFolderPath(Environment.SpecialFolder.StartMenu)}\\Programs\\EmuGUI.lnk");
            shortcut.TargetPath = $"{instPath}\\EmuGUI_v{appVer}_Win_amd64\\emugui.exe";
            shortcut.Description = "Making QEMU emulation easier";
            shortcut.Save();
        }
    }

    public IFPInst(string selAppName, string selAppVer, string selDl, string selSha256, string selSha512, string selInstPath, bool? startMenu, bool? desktop)
    {
        InitializeComponent();
        appName = selAppName;
        appVer = selAppVer;
        appDl = selDl;
        appSha256 = selSha256;
        appSha512 = selSha512;
        instPath = selInstPath;
        startMenShc = startMenu;
        deskShc = desktop;
        mainwin = (Window)this.GetVisualRoot();

        var bgworker = new BackgroundWorker();
        bgworker.DoWork += (sender, e) => {
            if (failed == false) CreateFolder();
            if (failed == false) SetDownloadLoc();
            if (failed == false) DownloadProgFile();
            if (failed == false) VerifyPkg();
            if (failed == false) ExtractProg();

            if (startMenShc == true && failed == false) {
                CreateShortcut("start");
            }

            if (deskShc == true && failed == false) {
                CreateShortcut("desktop");
            }

            if (failed == false) CleanUpTemp();
            if (failed == false) InstSuccess();
        };
        bgworker.RunWorkerAsync();
    }
}