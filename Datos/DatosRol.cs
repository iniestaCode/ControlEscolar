using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CE.Entidades;

namespace CE.Datos
{
    public class DatosRol
    {

        public Request<List<Rol>> GetRols()
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    List<Rol> listaRoles = db.Rol.ToList();
                    if (listaRoles == null)
                    {
                        return new Request<List<Rol>>() { Exito = false, Mensaje = "No se encontraron roles" };
                    }
                    return new Request<List<Rol>>() { Mensaje = "Se ha consultado con exito!!", Respuesta = listaRoles };
                }
            }
            catch (Exception ex)
            {
                return new Request<List<Rol>>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Rol> GetRol(int ID_Rol)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Rol rol = db.Rol.FirstOrDefault(r => r.ID_Rol == ID_Rol);
                    return new Request<Rol>() { Exito = true, Mensaje = "Se encontró el rol", Respuesta = rol };
                }
            }
            catch (Exception ex)
            {
                return new Request<Rol>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Rol> CreateRol(string nombre)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Rol rol = new Rol() { Nombre_Rol = nombre };
                    db.Rol.Add(rol);
                    return new Request<Rol> { Mensaje = "Se registró el rol con éxito", Respuesta = rol };
                }
            }
            catch (Exception ex)
            {
                return new Request<Rol>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Rol> UpdateRol(int ID_Rol, string nombre)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Rol rol = db.Rol.FirstOrDefault(r => r.ID_Rol == ID_Rol);
                    db.Rol.Attach(rol);
                    db.Entry(rol).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new Request<Rol>() { Mensaje = "Se modificó el rol con éxito", Respuesta = rol };
                }
            }
            catch (Exception ex)
            {
                return new Request<Rol>() { Exito = false, Error = ex.Message };
            }
        }

        public Request<Rol> DeleteRol(int ID_Rol)
        {
            try
            {
                using (DBConnection db = new DBConnection())
                {
                    Rol rol = db.Rol.FirstOrDefault(r => r.ID_Rol == ID_Rol);
                    rol.Activo_Rol = "I";
                    db.Rol.Attach(rol);
                    db.Entry(rol).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new Request<Rol>() { Mensaje = "Se modificó el rol con éxito", Respuesta = rol };
                }
            }
            catch (Exception ex)
            {
                return new Request<Rol>() { Exito = false, Error = ex.Message };
            }
        }
    }
}
