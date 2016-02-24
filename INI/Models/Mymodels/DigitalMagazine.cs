using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INI.Models.Mymodels
{
    public class DigitalMagazine
    {
        public List<String> lstFiles { get; set; }
        public String Directory { get; set; }

        public String UrlPdf { get; set; }

        public String Prefix { get; set; }
    }
}