using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CE.Entidades;
using CE.Negocio;
using System.Configuration;
using Presentacion.Filters;

namespace Presentacion.Controllers.Materia
{
    [CheckFilterAttributes(IsCheck = true)]
    public class MateriaController : Controller
    {
        readonly NegocioMateria negocioMateria = new NegocioMateria();
        // GET: Materia
        public ActionResult Index()
        {

            Request<List<CE.Entidades.Materia>> listaMaterias = negocioMateria.GetMaterias();
            CE.Entidades.Alumno alumno = (CE.Entidades.Alumno)Session["Usuario"];
            if (alumno != null)
            {
                ViewBag.nom = alumno.Nombre_Alumno;
                ViewBag.ape = alumno.ApePaterno_Alumno;
            }
            List<CE.Entidades.Materia> lista = listaMaterias.Respuesta;
            ViewBag.Mensaje = listaMaterias.Mensaje;
            ViewBag.Error = listaMaterias.Error;
            return View(lista);
        }

        // GET: Materia/Details/5
        public ActionResult Details(int id)
        {
            Request<CE.Entidades.Materia> materia = negocioMateria.GetMateria(id);
            CE.Entidades.Alumno alumno = (CE.Entidades.Alumno)Session["Usuario"];
            if (alumno != null)
            {
                ViewBag.nom = alumno.Nombre_Alumno;
                ViewBag.ape = alumno.ApePaterno_Alumno;
            }
            ViewBag.Mensaje = materia.Mensaje;
            ViewBag.Error = materia.Error;
            return View(materia.Respuesta);
        }

        // GET: Materia/Create
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
        public string LimpiarNombre(string nombre)
        {
            String[] listaAQuitar = new string[4];
            listaAQuitar[0] = ConfigurationManager.AppSettings.Get("amperson");
            listaAQuitar[1] = ConfigurationManager.AppSettings.Get("mayor");
            listaAQuitar[2] = ConfigurationManager.AppSettings.Get("menor");
            listaAQuitar[3] = ConfigurationManager.AppSettings.Get("diagonal");
            ViewBag.strings = listaAQuitar;
            for (int i = 0; i < listaAQuitar.Length; i++)
            {
                var caracter = listaAQuitar[i].ToString();
                if (nombre.Contains(caracter))
                {
                    nombre = nombre.Replace(caracter, string.Empty);
                }
            }
            return nombre;
        }
        // POST: Materia/Create
        [HttpPost]
        public ActionResult Create(string nombre, double costo)
        {

            nombre = LimpiarNombre(nombre);

            if (nombre.Length < 1 || costo < 0)
            {
                ViewBag.Error = "Los valores no pueden ir vacios";
                return RedirectToAction("Create", "Materia");
            }
            else
            {
                Request<CE.Entidades.Materia> materia = negocioMateria.CreateMateria(nombre.ToUpper(), costo);



                if (materia.Exito)
                {
                    ViewBag.Exito = materia.Mensaje;
                    return RedirectToAction("Index", "Materia");

                }
                else
                {
                    ViewBag.Error = materia.Error;
                    return View();
                }
            }
        }

        // GET: Materia/Edit/5
        public ActionResult Edit(int id)
        {
            CE.Entidades.Alumno alumno = (CE.Entidades.Alumno)Session["Usuario"];
            if (alumno != null)
            {
                ViewBag.nom = alumno.Nombre_Alumno;
                ViewBag.ape = alumno.ApePaterno_Alumno;
            }
            CE.Entidades.Materia materia = negocioMateria.GetMateria(id).Respuesta;
            return View(materia);
        }

        // POST: Materia/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string nombre, double costo)
        {
            try
            {
                Request<CE.Entidades.Materia> materiaCreate = negocioMateria.UpdateMateria(nombre, costo, id);
                ViewBag.Exito = materiaCreate.Exito;
                return RedirectToAction("Index", "Materia");
            }
            catch
            {
                ViewBag.Error = "No se pudo actualizar la materia, intente de nuevo";
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Request<CE.Entidades.Materia> materiaDeleted = negocioMateria.DeleteMateria(id);
                ViewBag.Exito = materiaDeleted.Exito;
                return RedirectToAction("Index", "Materia");
            }
            catch
            {
                ViewBag.Error = "No se pudo eliminar la materia, intente de nuevo";
                return View();
            }
        }
    }
}