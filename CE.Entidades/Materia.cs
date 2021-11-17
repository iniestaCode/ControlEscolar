
namespace CE.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Materia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Materia()
        {
            this.Alumno_Materia = new HashSet<Alumno_Materia>();
        }
    
        [Key]
        public int ID_Materia { get; set; }
        [Required]
        public string Nombre_Materia { get; set; }
        [Required]
        public decimal Costo_Materia { get; set; }
        public string Activo_Materia { get; set; } = "A";
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alumno_Materia> Alumno_Materia { get; set; }
    }
}
