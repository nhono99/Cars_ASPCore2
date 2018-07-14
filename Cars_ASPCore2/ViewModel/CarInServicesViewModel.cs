using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cars_ASPCore2.Models;
namespace Cars_ASPCore2.ViewModel
{
    public class CarInServicesViewModel
    {

        public int CarId { get; set; }
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Style { get; set; }
        public int Year { get; set; }
      

        public string UserId { get; set; }
        public Service NewServiceObj { get; set; }
        public IEnumerable<Service> PastServiceObject { get; set; }
        public List<ServiceType> ServiceTypeObject { get; set; }
    }
}