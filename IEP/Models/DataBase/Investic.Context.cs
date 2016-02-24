﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IEP.Models.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InvesticEntities : DbContext
    {
        public InvesticEntities()
            : base("name=InvesticEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<tblAreaConocimiento> tblAreaConocimiento { get; set; }
        public virtual DbSet<tblAsesor> tblAsesor { get; set; }
        public virtual DbSet<tblAsesorGrupoInvestigacion> tblAsesorGrupoInvestigacion { get; set; }
        public virtual DbSet<tblCentrosPoblado> tblCentrosPoblado { get; set; }
        public virtual DbSet<tblCoInvestigadorGrupoInvestigacion> tblCoInvestigadorGrupoInvestigacion { get; set; }
        public virtual DbSet<tblDocumentosSoporte> tblDocumentosSoporte { get; set; }
        public virtual DbSet<tblGrupoInvestigacion> tblGrupoInvestigacion { get; set; }
        public virtual DbSet<tblInstitucion> tblInstitucion { get; set; }
        public virtual DbSet<tblInvitacionGrupo> tblInvitacionGrupo { get; set; }
        public virtual DbSet<tblLineaInvestigacion> tblLineaInvestigacion { get; set; }
        public virtual DbSet<tblMaestroCoInvestigador> tblMaestroCoInvestigador { get; set; }
        public virtual DbSet<tblMiembroGrupo> tblMiembroGrupo { get; set; }
        public virtual DbSet<tblMunicipios> tblMunicipios { get; set; }
        public virtual DbSet<tblPreguntaInvestigacion> tblPreguntaInvestigacion { get; set; }
        public virtual DbSet<tblPreguntaProyectoInvestigacion> tblPreguntaProyectoInvestigacion { get; set; }
        public virtual DbSet<tblProblemaInvestigacion> tblProblemaInvestigacion { get; set; }
        public virtual DbSet<tblProblemaProyectoInvestigacion> tblProblemaProyectoInvestigacion { get; set; }
        public virtual DbSet<tblProyectoInvestigacion> tblProyectoInvestigacion { get; set; }
        public virtual DbSet<tblRol> tblRol { get; set; }
        public virtual DbSet<tblSeguimientoProyectoInvestigacion> tblSeguimientoProyectoInvestigacion { get; set; }
        public virtual DbSet<tblTipoInstitucion> tblTipoInstitucion { get; set; }
        public virtual DbSet<tblZona> tblZona { get; set; }
        public virtual DbSet<tblConceptosEstadoArte> tblConceptosEstadoArte { get; set; }
        public virtual DbSet<tblEstadoArteProyectoInvestigacion> tblEstadoArteProyectoInvestigacion { get; set; }
        public virtual DbSet<tblHerramientasRecoleccionInformacion> tblHerramientasRecoleccionInformacion { get; set; }
        public virtual DbSet<tblLecturaAnalisisFuentesinformacionPoryectoInvestigacion> tblLecturaAnalisisFuentesinformacionPoryectoInvestigacion { get; set; }
        public virtual DbSet<tblPresupuestoProyectoInvestigacion> tblPresupuestoProyectoInvestigacion { get; set; }
        public virtual DbSet<tblRecoleccionInformacionProyectoInvestigacion> tblRecoleccionInformacionProyectoInvestigacion { get; set; }
        public virtual DbSet<tblRubro> tblRubro { get; set; }
        public virtual DbSet<tblForoProyectoInvestigacion> tblForoProyectoInvestigacion { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tblReflexionProyectoInvestigacion> tblReflexionProyectoInvestigacion { get; set; }
        public virtual DbSet<tblComentarioGrupo> tblComentarioGrupo { get; set; }
        public virtual DbSet<tblPropagacionGrupo> tblPropagacionGrupo { get; set; }
        public virtual DbSet<tblTipoFeria> tblTipoFeria { get; set; }
    }
}