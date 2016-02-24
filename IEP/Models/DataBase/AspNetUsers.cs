//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaims>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogins>();
            this.tblAsesor = new HashSet<tblAsesor>();
            this.tblCoInvestigadorGrupoInvestigacion = new HashSet<tblCoInvestigadorGrupoInvestigacion>();
            this.tblGrupoInvestigacion = new HashSet<tblGrupoInvestigacion>();
            this.tblInvitacionGrupo = new HashSet<tblInvitacionGrupo>();
            this.tblMaestroCoInvestigador = new HashSet<tblMaestroCoInvestigador>();
            this.tblMiembroGrupo = new HashSet<tblMiembroGrupo>();
            this.AspNetRoles = new HashSet<AspNetRoles>();
            this.tblForoProyectoInvestigacion = new HashSet<tblForoProyectoInvestigacion>();
            this.tblComentarioGrupo = new HashSet<tblComentarioGrupo>();
        }
    
        public string Id { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string PersonalID { get; set; }
        public int Genre { get; set; }
        public string Address { get; set; }
        public System.DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
    
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<tblAsesor> tblAsesor { get; set; }
        public virtual ICollection<tblCoInvestigadorGrupoInvestigacion> tblCoInvestigadorGrupoInvestigacion { get; set; }
        public virtual ICollection<tblGrupoInvestigacion> tblGrupoInvestigacion { get; set; }
        public virtual ICollection<tblInvitacionGrupo> tblInvitacionGrupo { get; set; }
        public virtual ICollection<tblMaestroCoInvestigador> tblMaestroCoInvestigador { get; set; }
        public virtual ICollection<tblMiembroGrupo> tblMiembroGrupo { get; set; }
        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
        public virtual ICollection<tblForoProyectoInvestigacion> tblForoProyectoInvestigacion { get; set; }
        public virtual ICollection<tblComentarioGrupo> tblComentarioGrupo { get; set; }
    }
}