using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using CE.Entidades;

namespace CE.Negocio
{
    public class NegocioAlumnoMateria
    {
        readonly DatosAlumnoMateria alumnoMateria = new DatosAlumnoMateria();

        public Request<List<Alumno_Materia>> GetAlumnoMateria(int ID_Alumno) => alumnoMateria.GetAlumnoMateria(ID_Alumno);

        public Request<Alumno_Materia> CreateAlumnoMateria(int ID_Alumno, int ID_Materia) => alumnoMateria.CreateAlumnoMateria(ID_Alumno, ID_Materia);

        public Request<double> Subtotal(double monto, int ID_Materia) => alumnoMateria.Subtotal(monto, ID_Materia);
    }
}
