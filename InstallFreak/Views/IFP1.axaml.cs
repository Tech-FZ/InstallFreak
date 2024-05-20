using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace InstallFreak.Views;

public partial class IFP1 : UserControl
{
    public IFP1()
    {
        InitializeComponent();
    }

    public void ChangeToSecondPage(object sender, RoutedEventArgs args)
    {
        (Parent as Window).Content = new IFP2();
    }
}