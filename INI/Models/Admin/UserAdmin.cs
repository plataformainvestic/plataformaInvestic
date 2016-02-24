using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INI.Models.Admin
{
    public class UserAdmin
    {        
        public string Name { get; set; }
        public string SureName { get; set; }
        public string Tipodocumento { get; set; }
        public string PersonalID { get; set; }
        public string Genre { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BirthDay { get; set; }

        public String Rol { get; set; }

        public String DT_RowId { get; set; }


    }


}