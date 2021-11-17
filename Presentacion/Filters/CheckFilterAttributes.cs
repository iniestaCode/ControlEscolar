using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CE.Entidades;
namespace Presentacion.Filters
{
    public class CheckFilterAttributes : ActionFilterAttribute
    {
        public bool IsCheck { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (IsCheck)
            {
                // Comprueba si el usuario ha iniciado sesión
                if (filterContext.HttpContext.Session["Usuario"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Login/Login");
                }
            }
        }
    }
}