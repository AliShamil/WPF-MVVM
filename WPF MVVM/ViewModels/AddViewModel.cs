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

namespace WPF_MVVM.ViewModels;

public  class AddViewModel
{
    public Car _newCar { get; set; } = new();

    public bool DialogResult { get; set; }

    public ICommand AcceptCommand { get; set; }
    public ICommand CancelCommand { get; set; }


    public AddViewModel()
    {
        AcceptCommand = new RelayCommand(ExecuteAcceptCommand, CanExecuteAcceptCommand);
        CancelCommand = new RelayCommand(ExecuteCancelCommand);
    }


    void ExecuteAcceptCommand(object? parametr)
    {
        if (parametr is Window window)
        {
            DialogResult = true;
            window.DialogResult = true;
        }
    }
    
    void ExecuteCancelCommand(object? parametr)
    {
        if (parametr is Window window)
            window.DialogResult = false;
    }


    bool CanExecuteAcceptCommand(object? parametr)
    {
        
        if (parametr is Window window && window.Content is StackPanel stackPanel)
        {
            foreach (var txt in stackPanel.Children.OfType<TextBox>())
            {

                if(txt.Name == "txtbYear" && !txt.Text.All(c => char.IsDigit(c))||txt.Text.Length < 3) 
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

}
