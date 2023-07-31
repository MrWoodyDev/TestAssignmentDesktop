using System.Windows;
using TestAssignmentDesktop.Wpf.ViewModels;

namespace TestAssignmentDesktop.Wpf.Views;

public partial class MainWindow : Window
{
    private MainViewModel _mainViewModel;

    public MainWindow()
    {
        InitializeComponent();
        _mainViewModel = new(Navig.NavigationService);
        _mainViewModel.CryptoCoinsList();
        this.DataContext = _mainViewModel;
    }
}