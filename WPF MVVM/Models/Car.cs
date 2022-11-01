using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM.Models;

public class Car : Entity
{
    public string? Make { get; set; }
    public string? Model { get; set; }
   
	private uint _year;

	public uint Year
    {
		get { return _year; }
		set 
		{
			if((value <= DateTime.Now.Year))
				_year = value; 
		}
	}

}
