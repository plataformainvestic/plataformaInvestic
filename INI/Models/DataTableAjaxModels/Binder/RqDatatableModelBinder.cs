using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INI.Models.DataTableAjaxModels.Binder
{
    public class RqDatatableModelBinder:IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;

            int draw = int.Parse(request["draw"]);
            int start = int.Parse(request["start"]);
            int lengh = int.Parse(request["length"]);

            RqSearch search = new RqSearch() 
            {
                Value = request["search[value]"],
                Regex = bool.Parse(request["search[regex]"])
            };
            //--------------------------
            
            int i = 0;
            List<RqOrder> order = new List<RqOrder>();
            while (!string.IsNullOrEmpty(request["order[" + i + "][column]"]))
            {
                order.Add(new RqOrder()
                {
                    Column = int.Parse(request["order[" + i + "][column]"]),
                    Dir = request["order[" + i + "][dir]"]
                });
                i++;
            }
            //-------------------------
            i = 0;
            List<RqColumn> columns = new List<RqColumn>();
            while (!string.IsNullOrEmpty(request["columns[" + i + "][data]"]))
            {
                columns.Add(new RqColumn() {
                    Data = request["columns[" + i + "][data]"],
                    Name = request["columns[" + i + "][name]"],
                    Searchable = bool.Parse(request["columns[" + i + "][searchable]"]),
                    Orderable = bool.Parse(request["columns[" + i + "][orderable]"]),
                    Search = new RqSearch()
                    {
                        Value = request["columns[" + i + "][search][value]"],
                        Regex = bool.Parse(request["columns[" + i + "][search][regex]"])
                    }
                });
                i++;
            }


            return new RequestModel() {Draw=draw,Start=start,Length=lengh,Search=search,Orders=order,Columns=columns };
        }
    }
}