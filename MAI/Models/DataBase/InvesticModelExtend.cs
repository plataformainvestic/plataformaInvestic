using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace MAI.Models.DataBase
{

    public partial class AspNetUsers
    {
        public static string GetUserId(string userName)
        {
            investicEntities db = new investicEntities();
            var q = (from u in db.AspNetUsers
                     where u.UserName.Equals(userName)
                     select u).FirstOrDefault();
            if (q != null)
            {
                return q.Id;
            }
            return "";
        }

        public static string GetName(string userName)
        {
            investicEntities db = new investicEntities();
            var q = (from u in db.AspNetUsers
                     where u.UserName.Equals(userName)
                     select u).FirstOrDefault();
            if (q != null)
            {
                return string.Format("{0} {1}", q.Name, q.SureName);
            }
            return "";
        }

        public static string GetFullName(string id)
        {
            investicEntities db = new investicEntities();
            var q = (from t in db.AspNetUsers
                    where t.Id.Equals(id)
                    select t).FirstOrDefault();
            if (q != null)
            {
                string name = string.Format("{0} {1}", q.Name, q.SureName);
                return name;
            }
            return "";
        }
    }

    public partial class AspNetRoles
    {
        public static string GetRoleId(string roleName)
        {
            investicEntities db = new investicEntities();

            var q = (from r in db.AspNetRoles
                     where r.Name.Equals(roleName)
                     select r).FirstOrDefault();
            return q.Id;
        }
    }

    public partial class AspNetUsersRoles
    {
        public static List<string> GetRoles(string Iduser)
        {
            investicEntities db = new investicEntities();

            var r = (from l in db.AspNetUserRoles
                     where l.UserId.Equals(Iduser)
                     select l.AspNetRoles.Name);
            return r.ToList();
            //var q = (from u in db.AspNetUsers
            //         where u.UserName.Equals(userName)
            //         select u).First();
            //return q.Id;
        }

        public static bool IsUserInRole(string roleName, string userName)
        {
            investicEntities db = new investicEntities();

            var userId = AspNetUsers.GetUserId(userName);
            var roleId = AspNetRoles.GetRoleId(roleName);
            var r = (from l in db.AspNetUserRoles
                     where l.UserId.Equals(userId) && l.RoleId.Equals(roleId)
                     select l).FirstOrDefault();
            return r != null;
        }
    }



    public class MiembrosGrupoCoinvestigador
    {
        public int idGrupoInvestigacion { get; set; }
        public List<AspNetUsers> Usuarios { get; set; }
    }
}