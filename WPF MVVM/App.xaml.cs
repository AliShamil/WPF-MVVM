using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_MVVM.Repositories.Abstracts;
using WPF_MVVM.Repositories.Concretes;
using WPF_MVVM.ViewModels;
using WPF_MVVM.Views;

namespace WPF_MVVM
{
    public partial class App : Application
    {
        void ApplicationStartup(object? sender , StartupEventArgs e)
        {
            ICarRepository _carRepository = new FakeCarRepository();
            MainView mainView = new ();
            MainViewModel mainViewModel = new MainViewModel(_carRepository);
            mainView.DataContext = mainViewModel;
            mainView.ShowDialog();
        }

    }
}
