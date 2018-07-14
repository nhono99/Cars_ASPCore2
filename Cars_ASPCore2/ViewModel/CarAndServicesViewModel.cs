using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cars_ASPCore2.Models;

namespace Cars_ASPCore2.Models
{
    public class CarAndServicesViewModel
    {
        public Car CarObj { get; set; }
        public Service NewServiceObj { get; set; }
        public IEnumerable<Service> PastServiceObject { get; set; }
        public List<ServiceType> ServiceTypeObject { get; set; }
    }
}
