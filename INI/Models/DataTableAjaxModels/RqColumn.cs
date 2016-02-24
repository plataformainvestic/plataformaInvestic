using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI.Models.DataTableAjaxModels
{
    public class RqColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public RqSearch Search { get; set; }

    }
}
