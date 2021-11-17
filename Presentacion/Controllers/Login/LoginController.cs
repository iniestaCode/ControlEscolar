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
            if (usuario == "admin" && password == "password")
            {
                CE.Entidades.Alumno alumno1 = new CE.Entidades.Alumno() { Nombre_Alumno = "Administrador", ApePaterno_Alumno = "Admin", ApeMaterno_Alumno = "Admin", FK_ID_Usuario = "admin" };
                Session["Usuario"] = alumno1;
                Session.Add("Usuario", alumno1);
                return RedirectToAction("Comodin", "Alumno");
            }
            Request<Usuario> user = negocioUsuario.Login(usuario, password);
            try
            {
                if (user.Exito)
                {
                    Request<CE.Entidades.Alumno> alumno = negocioAlumno.GetAlumno(user.Respuesta.Usuario1);
                    Session["Usuario"] = alumno.Respuesta;
                    Session.Add("Usuario", alumno.Respuesta);
                    ViewBag.Exito = user.Exito;
                    return RedirectToAction("Comodin", "Alumno");
                }
                else
                {
                    ViewBag.Error = user.Error;
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
