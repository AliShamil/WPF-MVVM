using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_MVVM.Navigations;
using WPF_MVVM.Repositories.Abstracts;
using WPF_MVVM.Repositories.Concretes;
using WPF_MVVM.ViewModels;
using WPF_MVVM.Views;
using IContainer = Autofac.IContainer;

namespace WPF_MVVM
{
    public partial class App : Application
    {
        public static IContainer? Container { get; set; }

        void ApplicationStartup(object? sender , StartupEventArgs e)
        {
            NavigationStore navigationStore = new();

            var builder = new ContainerBuilder();

            builder.RegisterInstance(navigationStore).SingleInstance();

            builder.RegisterType<FakeCarRepository>().As<ICarRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<HomeViewModel>();

            Container = builder.Build();

            navigationStore.CurrentViewModel = Container.Resolve<HomeViewModel>();

            MainView mainView = new();
            mainView.DataContext = Container.Resolve<MainViewModel>();

            mainView.Show();
        }

    }
}
