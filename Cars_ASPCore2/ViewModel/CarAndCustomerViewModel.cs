using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cars_ASPCore2.Models;
namespace Cars_ASPCore2.ViewModel
{
    public class CarAndCustomerViewModel
    {
        public ApplicationUser UserObj { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}
