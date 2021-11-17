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

    [CheckFilterAttributes(IsCheck = true)]
    public class AlumnoController : Controller
    {
        readonly NegocioAlumno negocioAlumno = new NegocioAlumno();

        public ActionResult Index()
        {

            Request<List<CE.Entidades.Alumno>> listaAlumnos = negocioAlumno.GetAlumnos();
            if (listaAlumnos.Exito)
            {

                CE.Entidades.Alumno alumno = (CE.Entidades.Alumno)Session["Usuario"];
                if (alumno != null)
                {
                    ViewBag.nom = alumno.Nombre_Alumno;
                    ViewBag.ape = alumno.ApePaterno_Alumno;
                }

                List<CE.Entidades.Alumno> lista = listaAlumnos.Respuesta;
                ViewBag.Mensaje = listaAlumnos.Mensaje;
                return View(lista);

            }
            ViewBag.Error = listaAlumnos.Error;
            return View();
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
            ViewBag.Mensaje = alumno_obj.Mensaje;
            ViewBag.Error = alumno_obj.Error;
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
        public ActionResult Create(string nombre, string apePaterno, string usuario, string password, string apeMaterno)
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
                    ViewBag.Exito = alumno.Mensaje;
                    return RedirectToAction("Index", "Alumno");

                }
                else
                {
                    ViewBag.Error = alumno.Error;
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
            return View("Edit", alumno);
        }
        // POST: Alumno/Edit/5
        [HttpPost]
        [Route("Update/{ID_Alumno:int}")]
        public ActionResult Edit(int ID_Alumno, string nombre, string apePaterno, string apeMaterno, string usuario)
        {
            try
            {
                Request<CE.Entidades.Alumno> alumno = negocioAlumno.UpdateAlumno(nombre, apePaterno, apeMaterno, usuario);
                if (alumno.Exito)
                {
                    ViewBag.Exito = alumno.Mensaje;
                    return RedirectToAction("Index", "Alumno");
                }
                ViewBag.Error = alumno.Error;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: Alumno/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            try
            {
                Request<CE.Entidades.Alumno> alumno = negocioAlumno.DeleteAlumno(id);
                if (alumno.Exito)
                {
                    ViewBag.Exito = "Se eliminó el alumno";
                    return RedirectToAction("Index", "Alumno");
                }
                ViewBag.Error = alumno.Error;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
