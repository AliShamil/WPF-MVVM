using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using WPF_MVVM.Command;
using WPF_MVVM.Models;
using WPF_MVVM.Navigations;
using WPF_MVVM.Repositories.Abstracts;

namespace WPF_MVVM.ViewModels;

public class AddViewModel : BaseViewModel
{

    private ICarRepository _carRepository;
    private NavigationStore _navigationStore;
    public Car NewCar { get; set; } = new();



    public ICommand AcceptCommand { get; set; }
    public ICommand CancelCommand { get; set; }


    public AddViewModel(ICarRepository carRepository, NavigationStore navigationStore)
    {
        _carRepository = carRepository;
        _navigationStore = navigationStore;

        AcceptCommand = new RelayCommand(ExecuteAcceptCommand, CanExecuteAcceptCommand);
        CancelCommand = new RelayCommand(ExecuteCancelCommand);
    }


    void ExecuteAcceptCommand(object? parametr)
    {
        _carRepository.Add(NewCar);
        _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
    }

    void ExecuteCancelCommand(object? parametr)
    {
        _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
    }


    bool CanExecuteAcceptCommand(object? parametr)
    {
        if (parametr is UserControl view && view.Content is StackPanel stackPanel)
        {
            foreach (var txt in stackPanel.Children.OfType<TextBox>())
            {
                if (txt.Name == "txtbYear" && !txt.Text.All(c => char.IsDigit(c))||txt.Text.Length < 3)
                {
                    return false;
                }
            }
            return true;
        }

        return false;
    }

}
