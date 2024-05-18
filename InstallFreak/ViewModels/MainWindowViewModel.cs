using InstallFreak.DataModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InstallFreak.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        public ObservableCollection<InstallFreakApps> AppList {get;}

        public MainWindowViewModel() {
            var apps = new List<InstallFreakApps> {
                new InstallFreakApps(
                    "EmuGUI", "1.2.3.5513", "https://www.github.com/Tech-FZ/EmuGUI/releases/download/v1.2.3.5513/EmuGUI_v1.2.3.5513_Win_amd64.zip",
                    "https://www.github.com/Tech-FZ/EmuGUI/releases/download/v1.2.3.5513/EmuGUI_v1.2.3.5513_Win_amd64.zip.sha256", "",
                    "Latest stable version"),

                new InstallFreakApps(
                    "EmuGUI Pre-Release", "2.0.0.5608_rc2", "https://www.github.com/Tech-FZ/EmuGUI-PreRelease/releases/download/v2.0.0.5608_rc2/EmuGUI_v2.0.0.5608_rc2_Win_amd64.zip",
                    "https://www.github.com/Tech-FZ/EmuGUI-PreRelease/releases/download/v2.0.0.5608_rc2/EmuGUI_v2.0.0.5608_rc2_Win_amd64.zip.sha256", "",
                    "Latest pre-release version"),
            };

            AppList = new ObservableCollection<InstallFreakApps>(apps);
        }
    }
}


