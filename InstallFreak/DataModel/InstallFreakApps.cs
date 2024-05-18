using System;

namespace InstallFreak.DataModel {
    public class InstallFreakApps {
        public bool ToInstall {get; set;}
        public string AppName {get; set;}
        public string AppVer {get; set;}
        public string AppDL {get; set;}
        public string AppSHA256 {get; set;}
        public string AppSHA512 {get; set;}
        public string Notes {get; set;}

        public InstallFreakApps(bool toInstall, string appName, string appVer, string appDL, string appSHA256, string appSHA512, string notes) {
            ToInstall = toInstall;
            AppName = appName;
            AppVer = appVer;
            AppDL = appDL;
            AppSHA256 = appSHA256;
            AppSHA512 = appSHA512;
            Notes = notes;
        }
    }
}