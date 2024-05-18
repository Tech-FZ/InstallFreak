using System.Collections.Generic;
using System.Collections.ObjectModel;
using InstallFreak.DataModel;

namespace InstallFreak.ViewModels {
    public class IFViewModel : ViewModelBase {
        public IFViewModel(IEnumerable<InstallFreakApps> apps) {
            AppList = new ObservableCollection<InstallFreakApps>(apps);
        }

        public ObservableCollection<InstallFreakApps> AppList {get;}
    }
}