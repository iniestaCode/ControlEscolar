//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CE.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Alumno_Materia
    {
        [Key]
        public int ID_Alumno_Materia { get; set; }
        [ForeignKey("Alumno")]
        public int FK_ID_Alumno { get; set; }
        [ForeignKey("Materia")]
        public int FK_ID_Materia { get; set; }
    
        public virtual Alumno Alumno { get; set; }
        public virtual Materia Materia { get; set; }
    }
}
