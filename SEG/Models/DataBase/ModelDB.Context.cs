﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SEG.Models.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<tabla_alternativas> tabla_alternativas { get; set; }
        public virtual DbSet<tabla_estados> tabla_estados { get; set; }
        public virtual DbSet<fechasreporte> fechasreportes { get; set; }
        public virtual DbSet<producto> productos { get; set; }
        public virtual DbSet<evidencia> evidencias { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<equipos> equipos { get; set; }
        public virtual DbSet<programa_tareas> programa_tareas { get; set; }
        public virtual DbSet<responsabilidade> responsabilidades { get; set; }
        public virtual DbSet<actividade> actividades { get; set; }
        public virtual DbSet<cargo> cargos { get; set; }
    }
}
