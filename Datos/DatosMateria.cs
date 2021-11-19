using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CE.Entidades;
namespace Datos
{
    public class DatosMateria
    {
        public Request<List<Materia>> GetMaterias()
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    List<Materia> listaMaterias = db.Materia.Where(m => m.Activo_Materia == "A").ToList();
                    if (listaMaterias == null)
                    {
                        return new Request<List<Materia>>() { Error = "No se encontraron materias", Exito = false };
                    }
                    return new Request<List<Materia>>() { Mensaje = "Materias encontradas", Respuesta = listaMaterias };
                }
            }
            catch (Exception ex)
            {
                return new Request<List<Materia>>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Materia> GetMateria(int ID_Materia)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Materia materia = db.Materia.FirstOrDefault(m => m.ID_Materia == ID_Materia);

                    if (materia != null)
                    {
                        return new Request<Materia>() { Mensaje = "Materia encontrada", Respuesta = materia };
                    }
                    else
                    {
                        return new Request<Materia>() { Exito = false, Error = "La materia no existe" };
                    }

                }
            }
            catch (Exception ex)
            {
                return new Request<Materia>() { Exito = false, Error = ex.Message };
            }
        }

        public bool MateriaExiste(string nombre)
        {
            using (DBConnection db = new DBConnection())
            {
                bool existe = db.Materia.Any(x => x.Nombre_Materia.Trim() == nombre.Trim());
                return existe;
            }
        }

        public Request<Materia> CreateMateria(string nombre, double costo)
        {

            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Materia materia;

                    if (MateriaExiste(nombre))
                    {
                        return new Request<Materia> { Error = "La materia ya existe", Exito = false };
                    }
                    materia = new Materia() { Nombre_Materia = nombre, Costo_Materia = (decimal)costo };
                    db.Materia.Add(materia);
                    db.SaveChanges();
                    return new Request<Materia> { Mensaje = "Se registró la materia con éxito", Respuesta = materia };
                }
            }
            catch (Exception ex)
            {
                return new Request<Materia>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Materia> UpdateMateria(string nombre, double costo, int ID_Materia)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Materia materia = db.Materia.FirstOrDefault(m => m.ID_Materia == ID_Materia);
                    materia.Nombre_Materia = nombre;
                    materia.Costo_Materia = (decimal)costo;
                    if (MateriaExiste(nombre))
                    {
                        return new Request<Materia> { Mensaje = "La materia ya existe", Exito = false };
                    }
                    db.Materia.Attach(materia);
                    db.Entry(materia).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new Request<Materia>() { Mensaje = "Se modificó el alumno con éxito", Respuesta = materia };
                }
            }
            catch (Exception ex)
            {
                return new Request<Materia>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Materia> DeleteMateria(int ID_Materia)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Materia materia = db.Materia.FirstOrDefault(m => m.ID_Materia == ID_Materia);
                    materia.Activo_Materia = "I";
                    db.Materia.Attach(materia);
                    db.Entry(materia).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new Request<Materia>() { Mensaje = "Se modificó el rol con éxito", Respuesta = materia };
                }
            }
            catch (Exception ex)
            {
                return new Request<Materia>() { Exito = false, Error = ex.Message };
            }
        }
    }
}
