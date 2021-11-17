using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Filters
{
    public class CheckFilterAttributes : ActionFilterAttribute
    {      
        public bool IsCheck { get; set; } // IsCheck se usa para los campos de la interfaz que no necesitan ser verificados
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (IsCheck)
            {
                // Comprueba si el usuario ha iniciado sesión
                if (filterContext.HttpContext.Session["Usuario"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Login/Login"); // "/ Home / AdminLogin" Saltar a la página
                }
            }
        }

        }
    }