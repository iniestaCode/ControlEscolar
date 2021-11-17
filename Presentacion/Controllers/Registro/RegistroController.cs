using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CE.Entidades;
using CE.Negocio;
namespace Presentacion.Controllers.Registro
{
    public class RegistroController : Controller
    {
        readonly NegocioAlumno negocioAlumno = new NegocioAlumno();
        readonly NegocioUsuario negocioUsuario = new NegocioUsuario();
        // GET: Registro/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Registro/Create
        [HttpPost]
        public ActionResult Create(string nombre, string apePaterno, string apeMaterno, string usuario, string password)
        {
            try
            {
                if (nombre.Length < 1 || apePaterno.Length < 1 || usuario.Length < 3 || password.Length < 8)
                {
                    ViewBag.Error = "Los valores no pueden ir vacios";
                    return RedirectToAction("Create", "Registro");
                }
                else
                {

                    Request<CE.Entidades.Alumno> alumno = negocioAlumno.CreateAlumno(nombre.ToUpper(), apePaterno.ToUpper(), apeMaterno.ToUpper(), usuario, password);

                    if (alumno.Exito)
                    {
                        ViewBag.Exito = alumno.Mensaje + ", ahora inicia sesión";
                        return View("../Login/Login");

                    }
                    else
                    {
                        ViewBag.Error = "No se pudo crear su cuenta";
                        return RedirectToAction("Create");
                    }
                }


            }
            catch
            {
                ViewBag.Error = "Ocurrío un error, por favor intente de nuevo";
                return View("Create");
            }
        }

    }
}
