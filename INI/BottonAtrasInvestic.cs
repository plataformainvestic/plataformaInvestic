using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace INI
{
    public static class BottonAtrasInvestic
    {
        /// <summary>
        /// Se genero la adicion del boton atras en Javascript en el Navegador-atras 
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper requerimos de la Clase</param>
        /// <param name="textoBoton">Muestra texto del boton "Atras"</param>
        /// <param name="actionName">Nombre de la accion Click</param>
        /// <param name="controller">Nombre Opcional del controlador</param>
        /// <returns></returns>
        public static MvcHtmlString AtrasInvestic(this HtmlHelper htmlHelper, string textoBoton= "Atrás", string actionName = "index", string controller = null, object routeValuesObject = null)
        {
            // Note: "Index is provided as a default
            return MetodoRegresaAtras(htmlHelper, textoBoton, actionName, controller, routeValuesObject, new { onclick = "history.go(-1);return false;" });
        }


        public static MvcHtmlString MetodoRegresaAtras(this HtmlHelper htmlHelper, string textoBoton, string actionName, string controllerName, object routeValuesObject = null, object htmlAttributes = null)
        {
            // For testing - create links instead of buttons
            //return System.Web.Mvc.Html.LinkExtensions.ActionLink(htmlHelper, buttonText, actionName, controllerName, routeValues, htmlAttributes);

            if (string.IsNullOrEmpty(controllerName))
            {
                controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            }
            RouteValueDictionary routeValuesDictionary = new RouteValueDictionary(routeValuesObject);
            RouteValueDictionary htmlAttr = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            TagBuilder tb = new TagBuilder("input");
            tb.MergeAttributes(htmlAttr, false);
            string href = UrlHelper.GenerateUrl("default", actionName, controllerName, routeValuesDictionary, RouteTable.Routes, htmlHelper.ViewContext.RequestContext, false);

            tb.MergeAttribute("type", "submit");
            tb.MergeAttribute("value", textoBoton);
             tb.MergeAttribute("class", "button large bg-green");
            //tb.MergeAttribute("class", "button large");
            if (!tb.Attributes.ContainsKey("onclick"))
            {
                tb.MergeAttribute("onclick", "location.href=\'" + href + "\';return false;");
            }
            return new MvcHtmlString(tb.ToString(TagRenderMode.Normal).Replace("&#39;", "\'").Replace("&#32;", " "));
        }


    }
}