using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Cars_ASPCore2.Models;

namespace Cars_ASPCore2.Models
{
    public class Service
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double Miles { get; set; }
        [Required]
        public double Price { get; set; }
        public string Details { get; set; }

        [Required]
        [DisplayFormat(DataFormatString ="{0:d}")]
        public DateTime DateAdded { get; set; }

        public int CarId { get; set; }
        public int ServiceTypeId { get; set; }
        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }
    }
}
