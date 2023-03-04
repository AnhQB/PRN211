using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.ViewModels
{
    public class ViewModelContainer
    {
		
		public CategoryViewModel Category { get; set; }
		public AccountViewModel  Account { get; set; }

		public ViewModelContainer()
		{
			Category = new CategoryViewModel();
			Account = new AccountViewModel();
		}
	}
}
