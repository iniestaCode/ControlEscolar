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
        public ActionResult Create(string nombre,string apePaterno, string apeMaterno, string usuario, string password)
        {
            try
            {
                if(nombre.Length<1 || apePaterno.Length<1 || usuario.Length < 3 || password.Length < 8)
                {                    
                    ViewBag.Error = "Los valores no pueden ir vacios";
                    return RedirectToAction("Create", "Registro");
                }
                else
                {
                    Request<Usuario> userC = negocioUsuario.CreateUsuario(usuario, password,1);

                    Request<CE.Entidades.Alumno> alumno = negocioAlumno.CreateAlumno(nombre.ToUpper(), apePaterno.ToUpper(), apeMaterno.ToUpper(), userC.Respuesta.Usuario1);

                    if (alumno.Exito)
                    {                        
                        ViewBag.Exito = userC.Mensaje + ", ahora inicia sesión";
                        return View("../Login/Login");
                       
                    }
                    else
                    {
                        Request<Usuario> userB = negocioUsuario.DeleteUsuario(usuario);
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
