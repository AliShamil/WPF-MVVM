using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_MVVM.Command;
using WPF_MVVM.Models;
using WPF_MVVM.Navigations;
using WPF_MVVM.Repositories.Abstracts;
using WPF_MVVM.Repositories.Concretes;
using WPF_MVVM.Views;

namespace WPF_MVVM.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private readonly ICarRepository _carRepository;
    private readonly NavigationStore _navigationStore;

    public ObservableCollection<Car> Cars { get; set; }
    private Car? _car;
    public Car? SelectedCar
    {
        get { return _car; }
        set
        {
            _car = value;
            NotifyPropertyChanged(nameof(SelectedCar));
        }
    }


    public ICommand ShowCommand { get; set; }
    public ICommand AddCommand { get; set; }
    public ICommand UpdateCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public HomeViewModel(ICarRepository carRepository, NavigationStore navigationStore)
    {
        _carRepository=carRepository;
        _navigationStore = navigationStore;

        Cars = new(_carRepository.GetList()??new List<Car>());

        ShowCommand = new RelayCommand(ExecuteShowCommand, CanExecuteCommand);
        AddCommand = new RelayCommand(ExecuteAddCommand);
        DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteCommand);
        UpdateCommand = new RelayCommand(ExecuteUpdateCommand, CanExecuteCommand);
       
    }

    void ExecuteShowCommand(object? parameter)
     => MessageBox.Show(SelectedCar?.Model);


    bool CanExecuteCommand(object? parameter)
    => SelectedCar != null;


    void ExecuteAddCommand(object? parametr)
    {
        AddViewModel addViewModel = new(_carRepository, _navigationStore);

        AddView addView = new();
        addView.DataContext = addViewModel;

        _navigationStore.CurrentViewModel = addViewModel;

    }

    void ExecuteUpdateCommand(object? parametr)
    {
        UpdateViewModel updateViewModel = new UpdateViewModel(SelectedCar, _navigationStore, _carRepository);

        UpdateView updateView = new();
        _navigationStore.CurrentViewModel = updateViewModel;

    }

    void ExecuteDeleteCommand(object? parametr) => Cars.Remove(SelectedCar!);
}
