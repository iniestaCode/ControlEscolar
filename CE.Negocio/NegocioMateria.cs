using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CE.Datos;
using CE.Entidades;
using Datos;
namespace CE.Negocio
{
    public class NegocioMateria
    {
        readonly DatosMateria datosMateria = new DatosMateria();
        public Request<List<Materia>> GetMaterias() => datosMateria.GetMaterias();

        public Request<Materia> GetMateria(int ID_Materia) => datosMateria.GetMateria(ID_Materia);

        public Request<Materia> CreateMateria(string nombre, double costo) => datosMateria.CreateMateria(nombre, costo);

        public Request<Materia> UpdateMateria(string nombre, double costo, int ID_Materia) => datosMateria.UpdateMateria(nombre, costo, ID_Materia);

        public Request<Materia> DeleteMateria(int ID_Materia) => datosMateria.DeleteMateria(ID_Materia);

    }
}
