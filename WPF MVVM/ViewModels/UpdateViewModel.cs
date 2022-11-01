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

namespace WPF_MVVM.ViewModels
{
    public class UpdateViewModel
    {
        public Car SelectedCar { get; set; }

        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public UpdateViewModel(Car selectedCar)
        {
            SelectedCar = selectedCar;

            AcceptCommand = new RelayCommand(ExecuteAcceptCommand, CanExecuteAcceptCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
        }

        void ExecuteAcceptCommand(object? parametr)
        {
            //https://stopbyte.com/t/how-to-force-wpf-property-binding-to-update-its-source-value/49 
            //Bu saytdan Bindingin update olunmasini tapdim
            if (parametr is Window window && window.Content is StackPanel stackPanel)
            {
                
                foreach (var txt in stackPanel.Children.OfType<TextBox>())
                {
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }

                window.DialogResult = true;
            }
        }

        bool CanExecuteAcceptCommand(object? parametr)
        {
            if (parametr is Window window && window.Content is StackPanel stackPanel)
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
        {
            if (parametr is Window window)
                window.DialogResult = false;
        }
    }
}
