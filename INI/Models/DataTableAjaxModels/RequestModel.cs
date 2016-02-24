using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INI.Models.DataTableAjaxModels
{
    public class RequestModel
    {
        public int Draw { get; set; }
        public int Start { get; set; }

        public int Length { get; set; }

        public RqSearch Search { get; set; }
        public IEnumerable<RqOrder> Orders { get; set; }

        public IEnumerable<RqColumn> Columns { get; set; }

        
    }

   
}