using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Models;

namespace WPF_MVVM.Repositories.Contexts;

public class FakeDbContext
{
    public static List<Car> Cars { get; set; } = new()
    {
        new Car{ Id = 1, Make="Porche" , Model="Panamera",Year = 2017},
        new Car{ Id = 2, Make="Mercedes" , Model="E300",Year = 2008},
        new Car{ Id = 3, Make="Kia" , Model="Optima",Year = 2022},

    };

    public static List<Car> GetCars ()
    {
        var list =  new List<Car>();
        return list;
    }
}
