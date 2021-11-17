using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CE.Entidades;
using CE.Datos;
namespace Datos
{
    public class DatosAlumno
    {
        readonly DatosUsuario datosUsuario = new DatosUsuario();
        public Request<List<Alumno>> GetAlumnos()
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    List<Alumno> listaAlumnos = db.Alumno.Include("Usuario").Include("Rol").Where(x => x.Activo_Alumno == "A").Where(u => u.FK_ID_Usuario != "administrador" || u.FK_ID_Usuario == "admin").ToList();
                    if (listaAlumnos == null)
                    {
                        return new Request<List<Alumno>>() { Error = "No se encontraron alumnos", Exito = false };
                    }
                    return new Request<List<Alumno>>() { Mensaje = "Alumnos encontrados", Respuesta = listaAlumnos };
                }
            }
            catch (Exception ex)
            {
                return new Request<List<Alumno>>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Alumno> GetAlumno(string usuario)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Usuario user = db.Usuario.First(u => u.Usuario1 == usuario);

                    if (user != null)
                    {
                        Alumno alumnoE = db.Alumno.Include("Usuario").Where(u => u.FK_ID_Usuario == user.Usuario1).First();
                        return new Request<Alumno>() { Mensaje = "Alumno encontrado", Respuesta = alumnoE };
                    }
                    else
                    {
                        return new Request<Alumno>() { Exito = false, Error = "El alumno no existe" };
                    }

                }
            }
            catch (Exception ex)
            {
                return new Request<Alumno>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Alumno> CreateAlumno(string nombre, string apePaterno, string apeMaterno, string usuario, string password)
        {
            #region listaReservados
            String[] listaReservados = new string[10];
            listaReservados[0] = "ADMINISTRADOR";
            listaReservados[1] = "administrador";
            listaReservados[2] = "ADMIN";
            listaReservados[3] = "admin";
            #endregion
            try
            {
                if (listaReservados.Contains(usuario))
                {
                    return new Request<Alumno>() { Exito = false, Error = "No puede usar ese usuario, está reservado" };
                }
                using (DBConnection db = new DBConnection())
                {
                    int rolAlumno = int.Parse(db.Rol.First(x => x.Nombre_Rol == "Alumno").ToString());
                    if (!ExisteUsuario(usuario))
                    {
                        Request<Usuario> usuarioCreado = datosUsuario.CreateUsuario(usuario, password, rolAlumno);
                        if (usuarioCreado.Exito)
                        {
                            Alumno alumno = new Alumno() { Nombre_Alumno = nombre, ApePaterno_Alumno = apePaterno, ApeMaterno_Alumno = apeMaterno, FK_ID_Usuario = usuarioCreado.Respuesta.Usuario1 };
                            db.Alumno.Add(alumno);
                            db.SaveChanges();
                            return new Request<Alumno> { Mensaje = "Se registró el alumno con éxito", Respuesta = alumno };
                        }
                        else
                        {
                            return new Request<Alumno> { Error = usuarioCreado.Error, Exito = false };
                        }
                    }
                    else
                    {
                        return new Request<Alumno> { Error = "El usuario ingresado ya existe, intente con otro" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Request<Alumno>() { Exito = false, Error = ex.Message };
            }
        }

        public bool ExisteUsuario(string usuario)
        {
            using (DBConnection db = new DBConnection())
            {
                bool existe = db.Usuario.Any(x => x.Usuario1 == usuario);
                return existe;
            }
        }

        public Request<Alumno> UpdateAlumno(string nombre, string apePaterno, string apeMaterno, string usuario)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {

                    Usuario user = db.Usuario.First(u => u.Usuario1 == usuario);
                    Alumno alumnoEditado = db.Alumno.First(u => u.FK_ID_Usuario == user.Usuario1);
                    alumnoEditado.Nombre_Alumno = nombre;
                    alumnoEditado.ApePaterno_Alumno = apePaterno;
                    alumnoEditado.ApeMaterno_Alumno = apeMaterno;
                    db.Alumno.Attach(alumnoEditado);
                    db.Entry(alumnoEditado).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new Request<Alumno>() { Mensaje = "Se modificó el alumno con éxito", Respuesta = alumnoEditado };
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
                    Alumno alumnoEliminado = db.Alumno.First(u => u.FK_ID_Usuario == user.Usuario1);
                    alumnoEliminado.Activo_Alumno = "I";
                    db.Alumno.Attach(alumnoEliminado);
                    db.Entry(alumnoEliminado).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new Request<Alumno>() { Mensaje = "Se eliminó el alumno con éxito", Respuesta = alumnoEliminado };
                }
            }
            catch (Exception ex)
            {
                return new Request<Alumno>() { Exito = false, Error = ex.Message };
            }
        }

    }
}
