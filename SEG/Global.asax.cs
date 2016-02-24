using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;

namespace SEG
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private void application_EndRequest(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;

            if ((request.HttpMethod == "POST") &&
                (response.StatusCode == 404 && response.SubStatusCode == 13))
            {
                // Clear the response header but do not clear errors and
                // transfer back to requesting page to handle error
                response.ClearHeaders();
                HttpContext.Current.Server.Transfer(
                    request.AppRelativeCurrentExecutionFilePath);
            }
        }


       
    }
}