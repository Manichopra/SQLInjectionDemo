using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLInjectionDemo.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public string SearchString { get; set; }
    }
}
