using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CE.Datos;
using Datos;
using CE.Entidades;
namespace CE.Negocio
{
    public class NegocioAlumno
    {
        readonly DatosAlumno datosAlumno = new DatosAlumno();


        public Request<List<Alumno>> GetAlumnos() => datosAlumno.GetAlumnos();

        public Request<Alumno> GetAlumno(string usuario) => datosAlumno.GetAlumno(usuario);

        public Request<Alumno> CreateAlumno(string nombre, string apePaterno, string apeMaterno, string usuario,string password) => datosAlumno.CreateAlumno(nombre, apePaterno, apeMaterno, usuario,password);

        public Request<Alumno> UpdateAlumno(string nombre, string apePaterno, string apeMaterno, string usuario) => datosAlumno.UpdateAlumno(nombre, apePaterno, apeMaterno, usuario);

        public Request<Alumno> DeleteAlumno(string usuario) => datosAlumno.DeleteAlumno(usuario);


    }
}
