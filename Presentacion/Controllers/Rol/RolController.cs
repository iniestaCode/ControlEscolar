using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CE.Negocio;
using CE.Entidades;
using Presentacion.Filters;

namespace Presentacion.Controllers.Rol
{
    [CheckFilterAttributes(IsCheck = true)]
    public class RolController : Controller
    {
        readonly NegocioRol negocioRol = new NegocioRol();
        
        // GET: Rol
        public ActionResult Index()
        {
            Request<List<CE.Entidades.Rol>> listaRoles = negocioRol.GetRoles();    

            return View(listaRoles);
        }
      
    }
}
