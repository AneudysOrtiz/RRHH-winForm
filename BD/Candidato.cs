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
    
    public partial class Candidato
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Candidato()
        {
            this.Empleados = new HashSet<Empleado>();
            this.CapacitacionesCandidatos = new HashSet<CapacitacionesCandidato>();
            this.CompetenciasCandidatos = new HashSet<CompetenciasCandidato>();
            this.ExperienciasLaborales = new HashSet<ExperienciasLaborale>();
            this.IdiomasCandidatos = new HashSet<IdiomasCandidato>();
        }
    
        public int CadidatoId { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PuestoId { get; set; }
        public string Departamento { get; set; }
        public string Estado { get; set; }
        public Nullable<int> PuestoId1 { get; set; }
        public string Recomendado { get; set; }
        public Nullable<decimal> Salario { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empleado> Empleados { get; set; }
        public virtual Puesto Puesto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CapacitacionesCandidato> CapacitacionesCandidatos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompetenciasCandidato> CompetenciasCandidatos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExperienciasLaborale> ExperienciasLaborales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IdiomasCandidato> IdiomasCandidatos { get; set; }
    }
}
