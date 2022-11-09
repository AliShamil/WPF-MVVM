using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;
using WPF_MVVM.Command;
using WPF_MVVM.Models;
using WPF_MVVM.Navigations;
using WPF_MVVM.Repositories.Abstracts;

namespace WPF_MVVM.ViewModels
{
    public class UpdateViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly ICarRepository _carRepository;
        public Car SelectedCar { get; set; }

        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public UpdateViewModel(Car selectedCar, NavigationStore navigationStore, ICarRepository carRepository)
        {
            SelectedCar = selectedCar;
            _navigationStore = navigationStore;
            _carRepository = carRepository;
            AcceptCommand = new RelayCommand(ExecuteAcceptCommand, CanExecuteAcceptCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
        }
        
        void ExecuteAcceptCommand(object? parametr)
        {
            //https://stopbyte.com/t/how-to-force-wpf-property-binding-to-update-its-source-value/49 
            //Bu saytdan Bindingin update olunmasini tapdim
            if (parametr is UserControl view && view.Content is StackPanel stackPanel)
            {
                foreach (var txt in stackPanel.Children.OfType<TextBox>())
                {
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }

                _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
            }
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

        void ExecuteCancelCommand(object? parametr)
            => _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
    }
}
