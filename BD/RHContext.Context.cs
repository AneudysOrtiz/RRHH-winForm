﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RRHHOrtizEntities : DbContext
    {
        public RRHHOrtizEntities()
            : base("name=RRHHOrtizEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<Candidato> Candidatos { get; set; }
        public virtual DbSet<Capacitacione> Capacitaciones { get; set; }
        public virtual DbSet<CapacitacionesCandidato> CapacitacionesCandidatos { get; set; }
        public virtual DbSet<Competencia> Competencias { get; set; }
        public virtual DbSet<CompetenciasCandidato> CompetenciasCandidatos { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<ExperienciasLaborale> ExperienciasLaborales { get; set; }
        public virtual DbSet<Idioma> Idiomas { get; set; }
        public virtual DbSet<IdiomasCandidato> IdiomasCandidatos { get; set; }
        public virtual DbSet<Puesto> Puestos { get; set; }
    }
}