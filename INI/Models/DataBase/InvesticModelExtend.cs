using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace INI.Models.DataBase
{

    public partial class AspNetUsers
    {
        public static string GetUserId(string userName)
        {
            investicEntities db = new investicEntities();
            var q = (from u in db.AspNetUsers
                     where u.UserName.Equals(userName)
                     select new {u.Id}).FirstOrDefault();
            if (q != null)
            {
                return q.Id;
            }
            return "";
        }
        public static string GetNameById(Guid id)
        {
            investicEntities db = new investicEntities();
            string ids = id.ToString();
            var q = (from u in db.AspNetUsers
                     where u.Id==ids
                     select new {u.Name,u.SureName}).FirstOrDefault();
            if (q != null)
            {
                return string.Format("{0} {1}", q.Name, q.SureName);
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

        public static AspNetUsers Find(string UserId)
        {
            investicEntities db = new investicEntities();
            var q = (from u in db.AspNetUsers
                     where u.Id.Equals(UserId)
                     select u).FirstOrDefault();
            if (q != null)
            {
                return q;
            }
            return null;
        }

        public static List<AspNetUsers> GetUsersInRole(string rol)
        {
            investicEntities db = new investicEntities();
            List<AspNetUsers> users = new List<AspNetUsers>();

            var idRol = AspNetRoles.GetRoleId(rol);
            var usuarios = db.AspNetUserRoles.Where(m => m.RoleId.Equals(idRol)).Select(m => m.UserId).Take(10);
            foreach (string item in usuarios)
            {
                users.Add(AspNetUsers.Find(item));
            }
            return users;
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

    public partial class tblGrupoInvestigacion
    {
        public static tblGrupoInvestigacion Find(int id)
        {
            investicEntities cxt = new investicEntities();
            var q = (from m in cxt.tblGrupoInvestigacion
                     where m.id == id
                     select m).SingleOrDefault();
            return q;
        }

        public static tblGrupoInvestigacion FindByUserId(string userId)
        {
            investicEntities cxt = new investicEntities();
            var q = (from m in cxt.tblGrupoInvestigacion
                     where m.idUsuario.Equals(userId)
                     select m).SingleOrDefault();
            return q;
        }

        public static bool ExistGroup(string UserId)
        {
            investicEntities cxt = new investicEntities();
            var q = (from m in cxt.tblGrupoInvestigacion
                     where m.idUsuario == UserId
                     select m).SingleOrDefault();
            return q != null;
        }

        public static int ResearchGroupIdByUser(string UserId)
        {
            investicEntities cxt = new investicEntities();
            var q = (from m in cxt.tblGrupoInvestigacion
                     where m.idUsuario == UserId
                     select m.id).SingleOrDefault();
            return q;
        }

        public static List<AspNetUsers> UserNotInGroup(int id)
        {
            investicEntities cxt = new investicEntities();

            //List<AspNetUsers> Usuarios = new List<AspNetUsers>();

            var UsuariosEnGrupo = cxt.tblMiembroGrupo.Where(m => m.idGrupoInvestigacion == id);

            var UsuariosMaestros = AspNetUsers.GetUsersInRole("Maestro");

            var Usuarios = from u in UsuariosMaestros
                           where !(from ug in UsuariosEnGrupo
                                   select ug.idUsuario).Contains(u.Id)
                           select u;

            return Usuarios.ToList();
        }
        

        public static List<AspNetUsers> UserStudentNotInGroup(int id)
        {
            investicEntities cxt = new investicEntities();

            //List<AspNetUsers> Usuarios = new List<AspNetUsers>();

            var UsuariosEnGrupo = cxt.tblMiembroGrupo.Where(m => m.idGrupoInvestigacion == id);

            var UsuariosMaestros = AspNetUsers.GetUsersInRole("Estudiante");

            var Usuarios = from u in UsuariosMaestros
                           where !(from ug in UsuariosEnGrupo
                                   select ug.idUsuario).Contains(u.Id)
                           select u;

            return Usuarios.ToList();
        }
    }

    public partial class tblMaestroCoInvestigador
    {
        public static bool Exist(string userId)
        {
            investicEntities db = new investicEntities();
            var q = (from u in db.tblMaestroCoInvestigador
                     where u.idUsuario.Equals(userId)
                     select u).FirstOrDefault();
            return q != null;
        }

        public static int GetIdInstitucion(string userId)
        {
            investicEntities db = new investicEntities();
            var q = (from u in db.tblMaestroCoInvestigador
                     where u.idUsuario.Equals(userId)
                     select u).FirstOrDefault();
            return q.idInstitucion;
        }

        public static string GetNameInstitucion(string userId)
        {
            investicEntities db = new investicEntities();
            var q = (from u in db.tblMaestroCoInvestigador
                     where u.AspNetUsers.Id.Equals(userId)
                     select u).FirstOrDefault();
            return q.tblInstitucion.Nombre;
        }

        public static string GetMunicipalityInstitucion(string userId)
        {
            investicEntities db = new investicEntities();
            var q = (from u in db.tblMaestroCoInvestigador
                     where u.AspNetUsers.Id.Equals(userId)
                     select u).FirstOrDefault();
            return q.tblInstitucion.tblMunicipios.NombreMunicipio;
        }
    }

    public partial class tblMiembroGrupo
    {
        public static int GetRoleMiembro(string userId)
        {
            investicEntities db = new investicEntities();
            var q = (from u in db.tblMiembroGrupo
                     where u.idUsuario.Equals(userId)
                     select u).First();
            return q.idRol;
        }

        public static int GetRoleMiembro(string userId, int idGrupo)
        {
            investicEntities db = new investicEntities();

            var q = (from u in db.tblMiembroGrupo
                     where u.idUsuario.Equals(userId) && u.idGrupoInvestigacion.Equals(idGrupo)
                     select u).First();

            //var q = (from u in db.tblMiembroGrupo
            //         where u.idUsuario.Equals(userId) && u.idGrupoInvestigacion.Equals(idGrupo)
            //         select u).First();
            return q.idRol;
        }
    }


    public partial class tblGruposInvestigacion
    {
        public static List<AspNetUsers> UserNotInGroup(long id)
        {
            investicEntities cxt = new investicEntities();            

            var UsuariosEnGrupo = cxt.tblIntegrantesGrupoInv.Where(m => m.tblGruposInvestigacion_ID == id);

            var UsuariosMaestros = AspNetUsers.GetUsersInRole("Maestro");

            var Usuarios = from u in UsuariosMaestros
                           where !(from ug in UsuariosEnGrupo
                                   select ug.tblUsuarioPlataforma_ID).Contains(u.Id)
                           select u;

            return Usuarios.ToList();
        }
    }
    public class MiembrosGrupoCoinvestigador
    {
        public int idGrupoInvestigacion { get; set; }
        public List<AspNetUsers> Usuarios { get; set; }
    }

    public class MiembrosEstudianteinvestigador
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