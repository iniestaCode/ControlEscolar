using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CE.Entidades;
using CE.Negocio;
using Presentacion.Filters;

namespace Presentacion.Controllers.Alumno
{
  
    [CheckFilterAttributes(IsCheck =true)]
    public class AlumnoController : Controller
    {
        readonly NegocioAlumno negocioAlumno = new NegocioAlumno();
        readonly NegocioUsuario negocioUsuario = new NegocioUsuario();

        
        public ActionResult Index()
        {
            
                Request<List<CE.Entidades.Alumno>> listaAlumnos = negocioAlumno.GetAlumnos();
                CE.Entidades.Alumno alumno = (CE.Entidades.Alumno)Session["Usuario"];
                if (alumno != null)
                {
                    ViewBag.nom = alumno.Nombre_Alumno;
                    ViewBag.ape = alumno.ApePaterno_Alumno;
                }          

                List<CE.Entidades.Alumno> lista = listaAlumnos.Respuesta;
                return View(lista);
            
        }
       
        public ActionResult Comodin()
        {

            CE.Entidades.Alumno alumno = (CE.Entidades.Alumno)Session["Usuario"];
            if (alumno != null)
            {
                ViewBag.nom = alumno.Nombre_Alumno;
                ViewBag.ape = alumno.ApePaterno_Alumno;
            }
            return View();
        }
        // GET: Alumno/Details/5
        public ActionResult Details(string id)
        {
            Request<CE.Entidades.Alumno> alumno_obj = negocioAlumno.GetAlumno(id);
            CE.Entidades.Alumno alumno = (CE.Entidades.Alumno)Session["Usuario"];
            if (alumno != null)
            {
                ViewBag.nom = alumno.Nombre_Alumno;
                ViewBag.ape = alumno.ApePaterno_Alumno;
            }
            return View(alumno_obj.Respuesta);
        }

        // GET: Alumno/Create
        public ActionResult Create()
        {
            CE.Entidades.Alumno alumno = (CE.Entidades.Alumno)Session["Usuario"];
            if (alumno != null)
            {
                ViewBag.nom = alumno.Nombre_Alumno;
                ViewBag.ape = alumno.ApePaterno_Alumno;
            }
            return View();
        }

        // POST: Alumno/Create
        [HttpPost]
        public ActionResult Create(string nombre, string apePaterno,string usuario, string password, string apeMaterno)
        {
            if (nombre.Length < 1 || apePaterno.Length < 1 || usuario.Length < 3 || password.Length < 8)
            {
                ViewBag.Error = "Los valores no pueden ir vacios";
                return RedirectToAction("Create", "Registro");
            }
            else
            {
                Request<Usuario> userC = negocioUsuario.CreateUsuario(usuario, password, 1);

                Request<CE.Entidades.Alumno> alumno = negocioAlumno.CreateAlumno(nombre.ToUpper(), apePaterno.ToUpper(), apeMaterno.ToUpper(), userC.Respuesta.Usuario1);

                if (alumno.Exito)
                {
                    ViewBag.Exito = userC.Mensaje;
                    return RedirectToAction("Index", "Alumno");

                }
                else
                {
                    ViewBag.Error = "No se pudo registrar el alumno, intente de nuevo";
                    return View();
                }
            }
        }


        // GET: Alumno/Edit/5        
        [HttpGet]
        public ActionResult Editar(string id)
        {
            CE.Entidades.Alumno alumnoO = (CE.Entidades.Alumno)TempData["Usuario"];
            if (alumnoO != null)
            {
                ViewBag.nom = alumnoO.Nombre_Alumno;
                ViewBag.ape = alumnoO.ApePaterno_Alumno;
            }
            CE.Entidades.Alumno alumno = negocioAlumno.GetAlumno(id).Respuesta;
            return View("Edit",alumno);
        }
        // POST: Alumno/Edit/5
        [HttpPost]
        [Route("Update/{ID_Alumno:int}")]
        public ActionResult Edit(int ID_Alumno, string nombre, string apePaterno, string apeMaterno, string usuario)
        {
            try
            {
                negocioAlumno.UpdateAlumno(nombre, apePaterno, apeMaterno, usuario);
                ViewBag.Exito = "Se actualizo el alumno";
                return RedirectToAction("Index","Alumno");
            }
            catch
            {
                ViewBag.Error = "No se pudo actualizar el alumno, intente de nuevo";
                return View();
            }
        }       

        // POST: Alumno/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            try
            {
                negocioAlumno.DeleteAlumno(id);
                ViewBag.Exito = "Se eliminó el alumno";
                return RedirectToAction("Index", "Alumno");
            }
            catch
            {
                ViewBag.Error = "No se pudo eliminar el alumno, intente de nuevo";
                return View();
            }
        }
    }
}
