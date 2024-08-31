using InstallFreak.DataModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InstallFreak.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        public ObservableCollection<InstallFreakApps> AppList {get;}

        public MainWindowViewModel() {
            var apps = new List<InstallFreakApps> {
                new InstallFreakApps(
                    false, "EmuGUI", "2.0.8.5619", "https://github.com/Tech-FZ/EmuGUI/releases/download/v2.0.8.5619/EmuGUI_v2.0.8.5619_Win_amd64.zip",
                    "https://github.com/Tech-FZ/EmuGUI/releases/download/v2.0.8.5619/EmuGUI_v2.0.8.5619_Win_amd64.zip.sha256", "",
                    "Latest stable version"),

                new InstallFreakApps(
                    false, "EmuGUI Pre-Release", "2.1.0.5700_dev", "https://www.github.com/Tech-FZ/EmuGUI-PreRelease/releases/download/v2.1.0.5700_dev/EmuGUI_v2.1.0.5700_dev_Win_amd64.zip",
                    "https://www.github.com/Tech-FZ/EmuGUI-PreRelease/releases/download/v2.1.0.5700_dev/EmuGUI_v2.1.0.5700_dev_Win_amd64.zip.sha256",
                    "https://www.github.com/Tech-FZ/EmuGUI-PreRelease/releases/download/v2.1.0.5700_dev/EmuGUI_v2.1.0.5700_dev_Win_amd64.zip.sha512",
                    "Latest pre-release version"),
            };

            AppList = new ObservableCollection<InstallFreakApps>(apps);
        }
    }
}


