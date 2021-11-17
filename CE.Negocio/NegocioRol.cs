using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CE.Datos;
using CE.Entidades;

namespace CE.Negocio
{
    public class NegocioRol
    {
        readonly DatosRol datosRol = new DatosRol();
        public Request<List<Rol>> GetRoles() => datosRol.GetRols();

        public Request<Rol> GetRol(int id) => datosRol.GetRol(id);

        public Request<Rol> CreateRol(string nombre) => datosRol.CreateRol(nombre);

        public Request<Rol> UpdateRol(int id,string nombre) => datosRol.UpdateRol(id,nombre);

        public Request<Rol> DeleteRol(int id) => datosRol.DeleteRol(id);


    }
}
