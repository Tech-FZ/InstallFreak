using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace InstallFreak.Views;

public partial class IFPFail : UserControl
{
    string reasonFail;

    public IFPFail(string rsFail)
    {
        InitializeComponent();
        reasonFail = rsFail;
    }

    public void CloseProgram(object sender, RoutedEventArgs args)
    {
        Window mainwin = (Window)this.GetVisualRoot();
        mainwin.Close();
    }
}