using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INI.Models.DataTableAjaxModels
{
    public class ResponseErrorModel<T>:ResponseModel<T>
    {
        public string error { get; set; }
    }
}