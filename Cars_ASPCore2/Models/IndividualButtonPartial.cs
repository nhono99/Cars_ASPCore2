using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_ASPCore2.Models
{
    public class IndividualButtonPartial
    {
        public string ButtonType { get; set; }
        public string Action { get; set; }
        public string Text { get; set; }
        public string Glyph { get; set; }

        public int? ServiceId { get; set; }
        public string CustomerId { get; set; }

        public string ActionParameters {
            get
            {
                var param = new StringBuilder(@"/");
                if(ServiceId != null)
                {
                    if (ServiceId != 0)
                    {
                        param.Append(String.Format("{0}", ServiceId));
                    }
                }
               
                if(CustomerId != null)
                {
                    if (CustomerId.Length > 0) 
                    {
                        param.Append(String.Format("{0}", CustomerId));
                    }
                }
              
                return param.ToString().Substring(0, param.Length);
            }

        }
    }
}
