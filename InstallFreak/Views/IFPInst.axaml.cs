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

    string[] letterlist = {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
            "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N",
            "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };

    public void InstSuccess()
    {
        Window mainwin = (Window)this.GetVisualRoot();
        mainwin.Content = new IFPFinish();
    }

    public void CleanUpTemp() {
        txtCurTask.Text = "Cleaning up temporary files";

        try {
            Directory.Delete(downloadLocation, true);
        }
        
        catch {

        }
    }

    public void InstFail(string rsFail) {
        txtHeader.Text = "Installation failed! Reverting changes...";
        txtCurTask.Text = "Deleting program files";

        try {
            Directory.Delete(instPath, true);
        }
        
        catch {

        }

        CleanUpTemp();
        Window mainwin = (Window)this.GetVisualRoot();
        mainwin.Content = new IFPFail(rsFail);
    }

    public void CreateFolder() {
        txtCurTask.Text = $"Preparing installation path \"{instPath}\"";

        try {
            Directory.CreateDirectory(instPath);
        }
        
        catch (Exception ex) {
            InstFail(ex.Message);
        }
    }

    public void SetDownloadLoc() {
        txtCurTask.Text = "Setting download location";
        downloadLocation = $"{Environment.SpecialFolder.UserProfile}\\AppData\\Local\\Temp\\IF-";

        Random random = new Random();

        for (int i = 0; i <= 32; i++) {
            int rndmIdx = random.Next(0, letterlist.Length);
            downloadLocation = $"{downloadLocation}{letterlist[rndmIdx]}";
        }

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
                    Assert.That(File.Exists(saveTo));
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
        txtCurTask.Text = "Downloading program files";

        try {
            DownloadEngine(appDl, $"{downloadLocation}/{appName}_{appVer}.zip");
        }

        catch (Exception ex) {
            InstFail(ex.Message);
        }
    }

    public void VerifySHA256() {
        if (appSha256 != "" && appSha256 != null) {
            txtCurTask.Text = "Verifying file with SHA256";

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
                    //insert code for aborting installation
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
            txtCurTask.Text = "Verifying file with SHA512";

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
                    //insert code for aborting installation
                }

                filestream.Close();
            }

            catch (Exception ex) {
                InstFail(ex.Message);
            }
        }
    }

    public void VerifyPkg() {
        txtCurTask.Text = "Checking for verification files";
        VerifySHA256();
        VerifySHA512();
    }

    public void ExtractProg() {
        txtCurTask.Text = "Extracting program files";
        try {
            ZipFile.ExtractToDirectory($"{downloadLocation}/{appName}_{appVer}.zip", instPath);
        }

        catch (Exception ex) {
            InstFail(ex.Message);
        }
        
    }

    public void InstallProg() {
        CreateFolder();
        SetDownloadLoc();
        DownloadProgFile();
        VerifyPkg();
        ExtractProg();
        CleanUpTemp();
        InstSuccess();
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