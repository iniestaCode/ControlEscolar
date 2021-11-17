using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CE.Entidades;
namespace Datos
{
    public class DatosAlumno
    {

        public Request<List<Alumno>> GetAlumnos()
        {
            try
            {
                using(DBConnection db = new DBConnection())
                {
                    List<Alumno> listaAlumnos = db.Alumno.Include("Usuario").Where(x=>x.Activo_Alumno=="A").ToList();
                    return new Request<List<Alumno>>() { Mensaje = "Alumnos encontrados", Respuesta = listaAlumnos };
                }
            }catch(Exception ex)
            {
                return new Request<List<Alumno>>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Alumno> GetAlumno(string usuario)
        {
            try
            {
                
                using(CE.Entidades.DBConnection db = new DBConnection())
                {
                    Usuario user = db.Usuario.First(u => u.Usuario1 == usuario);
                    
                    if (user != null)
                    {
                        Alumno alumnoE = db.Alumno.Include("Usuario").Where(u => u.FK_ID_Usuario == user.Usuario1).First();
                        return new Request<Alumno>() { Mensaje = "Alumno encontrado", Respuesta = alumnoE };
                    }
                    else
                    {
                        return new Request<Alumno>() { Exito = false, Mensaje = "El alumno no existe" };
                    }
                    
                }
            }catch(Exception ex)
            {
                return new Request<Alumno>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Alumno> CreateAlumno(string nombre, string apePaterno, string apeMaterno,string usuario)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Alumno alumno = new Alumno() { Nombre_Alumno = nombre, ApePaterno_Alumno = apePaterno, ApeMaterno_Alumno = apeMaterno, FK_ID_Usuario = usuario };
                    db.Alumno.Add(alumno);
                    db.SaveChanges();
                    return new Request<Alumno> { Mensaje = "Se registró el alumno con éxito", Respuesta = alumno };
                }
            }
            catch (Exception ex)
            {
                return new Request<Alumno>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Alumno> UpdateAlumno(string nombre, string apePaterno, string apeMaterno, string usuario)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Usuario user = db.Usuario.First(u => u.Usuario1 == usuario);                   
                    Alumno alumnoE = db.Alumno.First(u => u.FK_ID_Usuario == user.Usuario1);
                    alumnoE.Nombre_Alumno = nombre;
                    alumnoE.ApePaterno_Alumno = apePaterno;
                    alumnoE.ApeMaterno_Alumno = apeMaterno;
                    db.Alumno.Attach(alumnoE);
                    db.Entry(alumnoE).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new Request<Alumno>() { Mensaje = "Se modificó el alumno con éxito", Respuesta = alumnoE };
                }
            }
            catch (Exception ex)
            {
                return new Request<Alumno>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Alumno> DeleteAlumno(string usuario)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Usuario user = db.Usuario.First(u => u.Usuario1 == usuario);                    
                    Alumno alumnoE = db.Alumno.First(u => u.FK_ID_Usuario == user.Usuario1);
                    alumnoE.Activo_Alumno = "I";
                    db.Alumno.Attach(alumnoE);
                    db.Entry(alumnoE).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new Request<Alumno>() { Mensaje = "Se eliminó el alumno con éxito", Respuesta = alumnoE };
                }
            }
            catch (Exception ex)
            {
                return new Request<Alumno>() { Exito = false, Error = ex.Message };
            }
        }

    }
}
