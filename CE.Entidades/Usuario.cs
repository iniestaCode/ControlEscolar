

namespace CE.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Alumno = new HashSet<Alumno>();
        }
    
        [Key]
        public string Usuario1 { get; set; }
        [Required]
        public string Contrasenia { get; set; }
        [ForeignKey("K_ID_Rol")]
        public int FK_ID_Rol { get; set; }
       
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alumno> Alumno { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
