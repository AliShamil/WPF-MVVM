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

namespace WPF_MVVM.ViewModels;

public class MainViewModel
{
    private readonly ICarRepository _carRepository;
    public ObservableCollection<Car> Cars { get; set; }
    public Car? SelectedCar { get; set; }

    public ICommand ShowCommand { get; set; }
    public ICommand Addommand { get; set; }
    public ICommand UpdateCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public MainViewModel(ICarRepository carRepository)
    {
        _carRepository=carRepository;
        Cars = new(_carRepository.GetList()??new List<Car>());
        ShowCommand = new RelayCommand(ExecuteShowCommand, CanExecuteShowCommand);
    }

    void ExecuteShowCommand(object? parameter)
     => MessageBox.Show(SelectedCar?.Model);


    bool CanExecuteShowCommand(object? parameter)
    => SelectedCar != null;
}
