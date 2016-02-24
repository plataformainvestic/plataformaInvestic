using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IEP.Models.DataBase
{ 

    public partial class AspNetUsers
    {
        public static string GetUserId(string userName)
        {
            InvesticEntities db = new InvesticEntities();            
            var q = (from u in db.AspNetUsers
                     where u.UserName.Equals(userName)
                     select u).First();
            return q.Id;
        }

        public static string GetName(string userName)
        {
            InvesticEntities db = new InvesticEntities();
            var q = (from u in db.AspNetUsers
                     where u.UserName.Equals(userName)
                     select u).First();
            return string.Format("{0} {1}", q.Name, q.SureName);
        }               
 
    }

    public partial class tblGrupoInvestigacion
    {
        public static tblGrupoInvestigacion Find(int id)
        {
            InvesticEntities cxt = new InvesticEntities();            
            var q = (from m in cxt.tblGrupoInvestigacion
                     where m.id == id
                     select m).SingleOrDefault();
            return q;
        }

        public static tblGrupoInvestigacion FindByUserId(string userId)
        {
            InvesticEntities cxt = new InvesticEntities();
            var q = (from m in cxt.tblGrupoInvestigacion
                     where m.idUsuario.Equals(userId)
                     select m).SingleOrDefault();
            return q;
        }

        public static bool ExistGroup(string UserId)
        {
            InvesticEntities cxt = new InvesticEntities();         
            var q = (from m in cxt.tblGrupoInvestigacion
                     where m.idUsuario == UserId
                     select m).SingleOrDefault();
            return q != null;
        }

        public static int ResearchGroupIdByUser(string UserId)
        {
            InvesticEntities cxt = new InvesticEntities();         
            var q = (from m in cxt.tblGrupoInvestigacion
                     where m.idUsuario == UserId
                     select m.id).SingleOrDefault();
            return q;
        }
    }

    public partial class tblMaestroCoinvestigador
    {
        public static bool Exist(string userId)
        {
            InvesticEntities db = new InvesticEntities();
            var q = (from u in db.tblMaestroCoInvestigador
                     where u.idUsuario.Equals(userId)
                     select u).FirstOrDefault();
            return q != null;
        }

        public static int GetIdInstitucion(string userId)
        {
            InvesticEntities db = new InvesticEntities();
            var q = (from u in db.tblMaestroCoInvestigador
                     where u.idUsuario.Equals(userId)
                     select u).First();
            return q.idInstitucion;
        }
    }

    public partial class tblMiembroGrupo
    {
        public static int GetRoleMiembro(string userId)
        {
            InvesticEntities db = new InvesticEntities();
            var q = (from u in db.tblMiembroGrupo
                     where u.idUsuario.Equals(userId)
                     select u).First();
            return q.idRol;
        }

        public static int GetRoleMiembro(string userId, int idGrupo)
        {
            InvesticEntities db = new InvesticEntities();
            var q = (from u in db.tblMiembroGrupo
                     where u.idUsuario.Equals(userId) && u.idGrupoInvestigacion == idGrupo
                     select u).First();
            return q.idRol;
        }
    }

    public class MiembrosGrupoCoinvestigador
    {
        public int idGrupoInvestigacion { get; set; }
        public List<AspNetUsers> Usuarios { get; set; }
    }

    public class UploadFile
    {
        public int Id { get; set; }
        public int ResearchGroupId { get; set; }
        public string FileToUpload { get; set; }
        public bool Acceptance { get; set; }
    }   
}