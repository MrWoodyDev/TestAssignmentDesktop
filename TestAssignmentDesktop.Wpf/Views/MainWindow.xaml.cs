using System.Net.Http;
using System.Windows;
using TestAssignmentDesktop.CoinCap.Client;
using TestAssignmentDesktop.Wpf.ViewModels;

namespace TestAssignmentDesktop.Wpf.Views;

public partial class MainWindow : Window
{
    private MainViewModel _mainViewModel;

    public MainWindow()
    {
        InitializeComponent();
        _mainViewModel = new(Navig.NavigationService, new CoinCapClient(new HttpClient()));
        _mainViewModel.CryptoCoinsList();
        this.DataContext = _mainViewModel;
    }
}