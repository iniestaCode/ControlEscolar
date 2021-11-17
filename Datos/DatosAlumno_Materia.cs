using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CE.Entidades;
namespace Datos
{
    public class DatosAlumnoMateria
    {
        public Request<List<Alumno_Materia>> GetAlumnoMateria(int ID_Alumno)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    List<Alumno_Materia> listaMateriasAlumno = db.Alumno_Materia.
                                                                Include("Materia").
                                                                Include("Alumno").
                                                                Where(u => u.FK_ID_Alumno == ID_Alumno).
                                                                ToList();
                    if (listaMateriasAlumno != null)
                    {
                        return new Request<List<Alumno_Materia>>() { Mensaje = "Materias del alumno encontradas", Respuesta = listaMateriasAlumno };
                    }
                    else
                    {
                        return new Request<List<Alumno_Materia>>() { Exito = false, Error = "No se encontraron materias" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Request<List<Alumno_Materia>>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Alumno_Materia> CreateAlumnoMateria(int ID_Alumno, int ID_Materia)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Alumno_Materia alumnoMateria = new Alumno_Materia() { FK_ID_Materia = ID_Materia, FK_ID_Alumno = ID_Alumno };
                    db.Alumno_Materia.Add(alumnoMateria);
                    db.SaveChanges();
                    return new Request<Alumno_Materia>() { Mensaje = "Se agregó correctamente la materia", Respuesta = alumnoMateria };
                }
            }
            catch (Exception ex)
            {
                return new Request<Alumno_Materia>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<double> Subtotal(double monto, int ID_Materia)
        {
            double subtotal = 0;
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Materia materia = db.Materia.First(m => m.ID_Materia == ID_Materia);
                    double costo = (double)materia.Costo_Materia;
                    subtotal = monto + costo;
                    return new Request<double>() { Respuesta = subtotal };
                }
            }
            catch (Exception ex)
            {
                return new Request<double>() { Exito = false, Respuesta = 0, Error = ex.Message };
            }
        }
    }
}
