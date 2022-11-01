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
using WPF_MVVM.Repositories.Abstracts;
using WPF_MVVM.Repositories.Concretes;
using WPF_MVVM.Views;

namespace WPF_MVVM.ViewModels;

public class MainViewModel
{
    private readonly ICarRepository _carRepository;
    public ObservableCollection<Car> Cars { get; set; }
    public Car? SelectedCar { get; set; }

    public ICommand ShowCommand { get; set; }
    public ICommand AddCommand { get; set; }
    public ICommand UpdateCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public MainViewModel(ICarRepository carRepository)
    {
        _carRepository=carRepository;
        Cars = new(_carRepository.GetList()??new List<Car>());
        ShowCommand = new RelayCommand(ExecuteShowCommand, CanExecuteCommand);
        AddCommand = new RelayCommand(ExecuteAddCommand);
        DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteCommand);
    }

    void ExecuteShowCommand(object? parameter)
     => MessageBox.Show(SelectedCar?.Model);


    bool CanExecuteCommand(object? parameter)
    => SelectedCar != null;


    void ExecuteAddCommand(object? parametr)
    {
        AddViewModel addViewModel = new();

        AddView addView = new();
        addView.DataContext = addViewModel;

        addView.ShowDialog();

        if (addViewModel.DialogResult)
            Cars.Add(addViewModel.NewCar);

    }

    void ExecuteDeleteCommand(object? parametr) => Cars.Remove(SelectedCar!);
}
