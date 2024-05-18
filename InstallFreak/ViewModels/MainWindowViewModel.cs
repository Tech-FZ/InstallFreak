using InstallFreak.Services;

namespace InstallFreak.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        public MainWindowViewModel() {
            var service = new IFService();
            IFAppList = new IFViewModel(service.GetItems());
        }

        public IFViewModel IFAppList {get;}
    }
}


