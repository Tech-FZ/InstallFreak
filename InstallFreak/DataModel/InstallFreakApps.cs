using System;

namespace InstallFreak.DataModel {
    public class InstallFreakApps {
        public string AppName {get; set;} = String.Empty;
        public string AppVer {get; set;} = String.Empty;
        public string AppDL {get; set;} = String.Empty;
        public string AppSHA256 {get; set;} = String.Empty;
        public string AppSHA512 {get; set;} = String.Empty;
        public string Notes {get; set;} = String.Empty;
    }
}