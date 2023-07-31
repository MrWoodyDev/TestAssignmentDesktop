﻿using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using TestAssignmentDesktop.CoinCap.Client;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Request;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Request;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Response;

namespace TestAssignmentDesktop.Wpf.ViewModels;

public class MainViewModel : ObservableObject
{
    private readonly NavigationService _navigNavigationService;
    private string _searchTicket;
    private ObservableCollection<Coins> _coins = new();
    private Coins _selectedCoin = null;
    private int _searchLimit = 10;
    private ObservableCollection<int> _searchLimits = new();

    public MainViewModel(NavigationService navigNavigationService)
    {
        _navigNavigationService = navigNavigationService;
        _searchLimits.Add(5);
        _searchLimits.Add(10);
        _searchLimits.Add(50);
        _searchLimits.Add(150);
    }

    public string? SearchTicket
    {
        get => _searchTicket;
        set
        {
            SetProperty(ref _searchTicket, value);
            SearchBoxTextChanged();
        }
    }

    public ObservableCollection<Coins> Coins
    {
        get => _coins;
        set => SetProperty(ref _coins, value);
    }

    public Coins SelectedCoin
    {
        get => _selectedCoin;
        set
        {
            SetProperty(ref _selectedCoin, value);
            ViewDetailsCoin();
        }
    }

    public int SearchLimit
    {
        get => _searchLimit;
        set
        {
            SetProperty(ref _searchLimit, value);
            SearchBoxTextChanged();
        }
    }

    public ObservableCollection<int> SearchLimits
    {
        get => _searchLimits;
        set => SetProperty(ref _searchLimits, value);
    }

    public async void SearchBoxTextChanged()
    {
        var coinClient = new CoinCapClient(new HttpClient());
        string searchText = _searchTicket;
        int limit = _searchLimit;
        var request = new GetCoinsRequest(searchText, limit, 0);
        var coinsListResponse = await coinClient.GetAssetsAsync(request);

        if (searchText != "")
        {
            Coins.Clear();

            foreach (var coin in coinsListResponse)
            {
                Coins.Add(coin);
            }
        }
    }

    public async Task CryptoCoinsList()
    {
        var coinClient = new CoinCapClient(new HttpClient());
        var request = new GetCoinsRequest("", _searchLimit, 0);
        var coinsListResponse = await coinClient.GetAssetsAsync(request);

        Coins.Clear();

        foreach (var coin in coinsListResponse)
        {
            Coins.Add(coin);
        }
    }

    public async void ViewDetailsCoin()
    {
        var coinClient = new CoinCapClient(new HttpClient());
        if (_selectedCoin != null)
        {
            string coinId = _selectedCoin.Id;
            var request = new GetCoinByIdRequest(coinId);
            var coinByIdResponse = await coinClient.GetAssetByIdAsync(request);

            var detailsPage = new DetailsPage
            {
                DataContext = coinByIdResponse.Data
            };

            _navigNavigationService.Navigate(detailsPage);
        }
    }
}