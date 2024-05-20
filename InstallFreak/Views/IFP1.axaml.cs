using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace InstallFreak.Views;

public partial class IFP1 : UserControl
{
    public IFP1()
    {
        InitializeComponent();
    }

    public void ChangeToSecondPage(object sender, RoutedEventArgs args)
    {
        Window mainwin = (Window)this.GetVisualRoot();
        mainwin.Content = new IFP2();
    }

    public void CloseProgram(object sender, RoutedEventArgs args)
    {
        Window mainwin = (Window)this.GetVisualRoot();
        mainwin.Close();
    }
}