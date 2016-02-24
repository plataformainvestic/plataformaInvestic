using INI.Models.DataBase;
using INI.Models.DataTableAjaxModels;
using INI.Models.DataTableAjaxModels.Binder;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using INI.Extensions.Controllers;
using INI.Models.RangeFechas;
using INI.Extensions.ActionResults;
using INI.Models.Admin;

namespace INI.Controllers.Administracion
{
    //[Authorize(Roles = "Administrator")]
    [Authorize]
    public class LogAccesoController : NewtomSofController
    {
        private investicEntities db = new investicEntities();
       
        // GET: LogAcceso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLogPdf([Bind(Include = "StartDate,EndDate,IsRole,IsAdvancedSearch,criterion")]ReportModel model)
        {
            if (ModelState.IsValid)
            {
                return new PDFResult(INI.LogReport.Reporte(model), "reporteLogAcceso");
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult getLogAcceso([ModelBinder(typeof(RqDatatableModelBinder))]RequestModel requestModel)
        {
            ResponseModel<LogAcceso> responseModel = new ResponseModel<LogAcceso>();
            Type dat = typeof(LogAcceso);

            //---Parametros de control
            string conditions = "";
            string orderbyparameters = "";

            var properties = dat.GetProperties();
            var q = db.tblLogAcceso.Select(m => new LogAcceso()
                    {
                        Rol=m.Rol,
                        Usuario=m.Usuario,
                        Latitud=m.Latitud,
                        Longitud=m.Longitud,
                        Altitud=m.Altitud,
                        Navegacion=m.Navegacion,
                        IP=m.IP,
                        FechaCierreSesion=m.FechaCierreSesion,
                        FechaInicioSesion=m.FechaInicioSesion,
                        Image = m.FechaInicioSesion == m.FechaCierreSesion ? "<img src=\"/images/true.png\" />" : "<img src=\"/images/false.png\" />"                        

                           
                    }
                  );

            if (!String.IsNullOrEmpty(requestModel.Search.Value))
            {
                string searchPhrase = requestModel.Search.Value;
                int i = 0;
                int n = properties.Count();

                foreach (var property in properties)
                {
                    if (property.PropertyType.Name == "DateTime")
                    {
                        i++;
                        continue;
                    }
                    if (property.Name == "Navegacion" || property.Name == "id" || property.Name == "Image")
                    {
                        i++;
                        continue;
                    }
                    if (property.PropertyType.Name == "String") conditions += string.Format("{0}.Contains(\"{1}\")", property.Name, searchPhrase);
                    if (i < n - 5) conditions += " || ";
                    i++;
                }
            }

            if (requestModel.Orders.ToList()[0].Column >= 0)
            {
                int column = requestModel.Orders.ToList()[0].Column;
                string key = requestModel.Columns.ToList()[column].Data;
                string val = requestModel.Orders.ToList()[0].Dir;
                string direction = val == "desc" ? "descending" : "";
                orderbyparameters = string.Format("{0} {1}", key, direction);
            }

            // Build Response 

            responseModel.recordsTotal = db.tblLogAcceso.Count();
            responseModel.draw = requestModel.Draw;

            if (conditions != "" && orderbyparameters != "")
            {
                //var q = db.AspNetUsers.Where(conditions).OrderBy(orderbyparameters).Skip(requestModel.Start).Take(requestModel.Length).
                var q2 = q.Where(string.Format(conditions, requestModel.Search.Value)).OrderBy(orderbyparameters);
                responseModel.recordsFiltered = q2.Count();
                responseModel.data = q2.Skip(requestModel.Start).Take(requestModel.Length).ToList();
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }
            else if (conditions == "" && orderbyparameters != "")
            {
                var q2 = q.OrderBy(orderbyparameters).Skip(requestModel.Start).Take(requestModel.Length);
                responseModel.recordsFiltered = q.Count();
                responseModel.data = q2.ToList();
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }
            else
            {
                responseModel.recordsFiltered = responseModel.recordsTotal;
                responseModel.data = q.OrderByDescending(m=>m.FechaInicioSesion).Skip(requestModel.Start).Take(requestModel.Length).ToList();

                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

        }
    }
}