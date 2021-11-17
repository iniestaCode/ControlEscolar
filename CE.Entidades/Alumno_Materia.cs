

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
        [ForeignKey("FK_ID_Alumno")]
        public int FK_ID_Alumno { get; set; }
        [ForeignKey("FK_ID_Materia")]
        public int FK_ID_Materia { get; set; }
    
        public virtual Alumno Alumno { get; set; }
        public virtual Materia Materia { get; set; }
    }
}
