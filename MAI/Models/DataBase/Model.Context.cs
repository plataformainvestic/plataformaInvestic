﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MAI.Models.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class investicEntities : DbContext
    {
        public investicEntities()
            : base("name=investicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<tblCaracteristicasProy> tblCaracteristicasProy { get; set; }
        public virtual DbSet<tblCaracteristicasProy_Rev> tblCaracteristicasProy_Rev { get; set; }
        public virtual DbSet<tblCriticoSocial> tblCriticoSocial { get; set; }
        public virtual DbSet<tblCronogramaProy> tblCronogramaProy { get; set; }
        public virtual DbSet<tblCronogramaProy_Rev> tblCronogramaProy_Rev { get; set; }
        public virtual DbSet<tblDiseniosProy> tblDiseniosProy { get; set; }
        public virtual DbSet<tblEjeInvestigacion> tblEjeInvestigacion { get; set; }
        public virtual DbSet<tblEstado> tblEstado { get; set; }
        public virtual DbSet<tblEventosAcademicos> tblEventosAcademicos { get; set; }
        public virtual DbSet<tblExperienciaProyectos> tblExperienciaProyectos { get; set; }
        public virtual DbSet<tblFechaCronograma> tblFechaCronograma { get; set; }
        public virtual DbSet<tblGruposInvestigacion> tblGruposInvestigacion { get; set; }
        public virtual DbSet<tblHistoricoHermeneutico> tblHistoricoHermeneutico { get; set; }
        public virtual DbSet<tblHojaVida> tblHojaVida { get; set; }
        public virtual DbSet<tblIdiomas> tblIdiomas { get; set; }
        public virtual DbSet<tblInstitucion> tblInstitucion { get; set; }
        public virtual DbSet<tblIntegrantesGrupoInv> tblIntegrantesGrupoInv { get; set; }
        public virtual DbSet<tblLenguas> tblLenguas { get; set; }
        public virtual DbSet<tblMarcoReferenciaProy> tblMarcoReferenciaProy { get; set; }
        public virtual DbSet<tblMarcoReferenciaProy_Rev> tblMarcoReferenciaProy_Rev { get; set; }
        public virtual DbSet<tblMetodoProy> tblMetodoProy { get; set; }
        public virtual DbSet<tblMetodoProy_Rev> tblMetodoProy_Rev { get; set; }
        public virtual DbSet<tblNivel> tblNivel { get; set; }
        public virtual DbSet<tblNivelAcademicoEducacionSuperior> tblNivelAcademicoEducacionSuperior { get; set; }
        public virtual DbSet<tblNivelIdioma> tblNivelIdioma { get; set; }
        public virtual DbSet<tblNivelLengua> tblNivelLengua { get; set; }
        public virtual DbSet<tblParadigmaEpistemologico> tblParadigmaEpistemologico { get; set; }
        public virtual DbSet<tblParadigmaMetodologico> tblParadigmaMetodologico { get; set; }
        public virtual DbSet<tblPresentacionProyecto> tblPresentacionProyecto { get; set; }
        public virtual DbSet<tblPresentacionProyecto_Rev> tblPresentacionProyecto_Rev { get; set; }
        public virtual DbSet<tblPresupuestoProy> tblPresupuestoProy { get; set; }
        public virtual DbSet<tblPresupuestoProy_Rev> tblPresupuestoProy_Rev { get; set; }
        public virtual DbSet<tblProblemaInvestigacionProy> tblProblemaInvestigacionProy { get; set; }
        public virtual DbSet<tblProblemaInvestigacionProy_Rev> tblProblemaInvestigacionProy_Rev { get; set; }
        public virtual DbSet<tblProductosAcademicos> tblProductosAcademicos { get; set; }
        public virtual DbSet<tblProyectosInvestigacion> tblProyectosInvestigacion { get; set; }
        public virtual DbSet<tblProyectosInvestigacion_Rev> tblProyectosInvestigacion_Rev { get; set; }
        public virtual DbSet<tblReferenciasProy> tblReferenciasProy { get; set; }
        public virtual DbSet<tblReferenciasProy_Rev> tblReferenciasProy_Rev { get; set; }
        public virtual DbSet<tblRubro> tblRubro { get; set; }
        public virtual DbSet<tblRubroPresupuesto> tblRubroPresupuesto { get; set; }
        public virtual DbSet<tblTituloEducacionSuperior> tblTituloEducacionSuperior { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<tblCategoriaProductos> tblCategoriaProductos { get; set; }
        public virtual DbSet<tblTipoEstudioProy> tblTipoEstudioProy { get; set; }
    }
}
