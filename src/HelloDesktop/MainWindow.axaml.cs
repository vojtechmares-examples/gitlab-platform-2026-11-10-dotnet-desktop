using Avalonia.Controls;
using Avalonia.Interactivity;
using HelloDesktop.Core;

namespace HelloDesktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new GreetingViewModel();
    }

    private void OnGreet(object? sender, RoutedEventArgs e) => ((GreetingViewModel)DataContext!).Greet();
}
