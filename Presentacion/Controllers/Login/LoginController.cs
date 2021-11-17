using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CE.Negocio;
using CE.Entidades;
namespace Presentacion.Controllers.Login
{
    public class LoginController : Controller
    {
        readonly NegocioUsuario negocioUsuario = new NegocioUsuario();
        readonly NegocioAlumno negocioAlumno = new NegocioAlumno();

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }        
      
        [HttpPost]
        public ActionResult Login(string usuario, string password)
        {
            Request<Usuario> user = negocioUsuario.Login(usuario, password);            
            try
            {
                if (user.Exito)
                {
                    Request<CE.Entidades.Alumno> alumno = negocioAlumno.GetAlumno(user.Respuesta.Usuario1);                    
                    Session["Usuario"] = alumno.Respuesta;
                    Session.Add("Usuario", alumno.Respuesta);
                    return RedirectToAction("Comodin", "Alumno");                     
                }
                else
                {
                    ViewBag.Error = "Datos Incorrectos";
                    return View();
                }                
            }
            catch
            {
                ViewBag.Error = "Algo falló, vuelve a intentarlo";
                return View();
            }
        }

        public ActionResult Logout()
        {            
                Session.Abandon();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));            
                return View("Login");
        }
       
    }
}
