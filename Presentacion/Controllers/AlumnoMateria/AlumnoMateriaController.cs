using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CE.Entidades;
using CE.Negocio;
using Presentacion.Filters;

namespace Presentacion.Controllers.AlumnoMateria
{
    [CheckFilterAttributes(IsCheck = true)]
    public class AlumnoMateriaController : Controller
    {
        readonly NegocioAlumnoMateria alumnoMateria = new NegocioAlumnoMateria();
        readonly NegocioMateria negocioMateria = new NegocioMateria();
        // GET: AlumnoMateria
        public ActionResult Index(int id)
        {
            CE.Entidades.Alumno alumno = (CE.Entidades.Alumno)Session["Usuario"];
            if (alumno != null)
            {
                ViewBag.nom = alumno.Nombre_Alumno;
                ViewBag.ape = alumno.ApePaterno_Alumno;
            }
            Request<List<CE.Entidades.Alumno_Materia>> lista = alumnoMateria.GetAlumnoMateria(id);
            return View(lista.Respuesta);
        }
        // GET: AlumnoMateria/Create
        public ActionResult Create()
        {
            CE.Entidades.Alumno alumno = (CE.Entidades.Alumno)Session["Usuario"];
            if (alumno != null)
            {
                ViewBag.nom = alumno.Nombre_Alumno;
                ViewBag.ape = alumno.ApePaterno_Alumno;
                ViewBag.id = alumno.ID_Alumno;
            }
            Request<List<CE.Entidades.Materia>> listaMaterias = negocioMateria.GetMaterias();
            return View(listaMaterias.Respuesta);
        }

        // POST: AlumnoMateria/Create
        [HttpPost]
        public ActionResult Create(string materias)
        {
            try
            {
                Array listMaterias = materias.Split(',');

                int ID_Alumno = int.Parse(listMaterias.GetValue(0).ToString());
                listMaterias.SetValue("vacio", 0);

                foreach (string i in listMaterias)
                {
                    if (i != "vacio")
                    {

                        alumnoMateria.CreateAlumnoMateria(ID_Alumno, int.Parse(i.ToString()));
                    }

                }                

                return RedirectToAction("Index","AlumnoMateria",ID_Alumno);                   
            }
            catch
            {
                return View();
            }
        }

        public ActionResult MisMaterias(int ID)
        {

            Request<List<CE.Entidades.Alumno_Materia>> misMaterias = alumnoMateria.GetAlumnoMateria(ID);            

            return View(misMaterias.Respuesta);
        }

        [HttpGet]
        public double SumarMonto(int ID_Materia, double monto)
        {
            Request<CE.Entidades.Materia> materia = negocioMateria.GetMateria(ID_Materia);
            ViewBag.Materia = materia.Respuesta;
            return double.Parse(materia.Respuesta.Costo_Materia.ToString()) + monto;
        }

        public double RestarMonto(int ID_Materia, double monto)
        {
            Request<CE.Entidades.Materia> materia = negocioMateria.GetMateria(ID_Materia);
            ViewBag.Materia = materia.Respuesta;
            return monto - double.Parse(materia.Respuesta.Costo_Materia.ToString());
        }
    }
}
