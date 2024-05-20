using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace InstallFreak.Views;

public partial class IFP2 : UserControl
{
    //MainWindow mainwin;

    public IFP2()
    {
        InitializeComponent();
        //mainwin = recMainWin;
    }

    public void ChangeToFirstPage(object sender, RoutedEventArgs args)
    {
        Window mainwin = (Window)this.GetVisualRoot();
        mainwin.Content = new IFP1();
    }
}