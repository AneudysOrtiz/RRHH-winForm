//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RRHHOrtiz.BD
{
    using System;
    using System.Collections.Generic;
    
    public partial class Puesto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Puesto()
        {
            this.Candidatos = new HashSet<Candidato>();
        }
    
        public int PuestoId { get; set; }
        public string Nombre { get; set; }
        public string NivelRiesgo { get; set; }
        public decimal NivelMinimoSalario { get; set; }
        public decimal NivelMaximoSalario { get; set; }
        public bool Estado { get; set; }
        public Nullable<int> Cupo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Candidato> Candidatos { get; set; }
    }
}
