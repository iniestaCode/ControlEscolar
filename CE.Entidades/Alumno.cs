
namespace CE.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Alumno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alumno()
        {
            this.Alumno_Materia = new HashSet<Alumno_Materia>();
        }
    
        [Key]
        public int ID_Alumno { get; set; }
        [Required]
        public string Nombre_Alumno { get; set; }
        [Required]
        public string ApePaterno_Alumno { get; set; }
        public string ApeMaterno_Alumno { get; set; }
        public string Activo_Alumno { get; set; } = "A";
        public string FK_ID_Usuario { get; set; }
        [ForeignKey("FK_ID_Usuario")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        
        public virtual ICollection<Alumno_Materia> Alumno_Materia { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
