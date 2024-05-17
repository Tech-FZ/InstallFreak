using System.Collections.Generic;
using InstallFreak.DataModel;

namespace InstallFreak.Services {
    public class IFService {
        public IEnumerable<InstallFreakApps> GetItems() => new[] {
            new InstallFreakApps {
                AppName = "EmuGUI",
                AppVer = "1.2.3.5513",
                AppDL = "https://www.github.com/Tech-FZ/EmuGUI/releases/download/v1.2.3.5513/EmuGUI_v1.2.3.5513_Win_amd64.zip",
                AppSHA256 = "https://www.github.com/Tech-FZ/EmuGUI/releases/download/v1.2.3.5513/EmuGUI_v1.2.3.5513_Win_amd64.zip.sha256",
                Notes = "Latest stable version"
                },

            new InstallFreakApps {
                AppName = "EmuGUI Pre-Release",
                AppVer = "2.0.0.5608_rc2",
                AppDL = "https://www.github.com/Tech-FZ/EmuGUI-PreRelease/releases/download/v2.0.0.5608_rc2/EmuGUI_v2.0.0.5608_rc2_Win_amd64.zip",
                AppSHA256 = "https://www.github.com/Tech-FZ/EmuGUI-PreRelease/releases/download/v2.0.0.5608_rc2/EmuGUI_v2.0.0.5608_rc2_Win_amd64.zip.sha256",
                Notes = "Latest pre-release version"
            }
        };
    }
}