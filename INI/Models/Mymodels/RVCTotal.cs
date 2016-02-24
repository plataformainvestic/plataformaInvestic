using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INI.Models.Mymodels
{
    public class RVCTotal
    {        
        public List<RedVirtualGrupoOtherUser> Misrvc { get; set; }
        public List<RedVirtualGrupoOtherUser> rvcMiembro { get; set; }

        public List<RedVirtualGrupoOtherUser> rvcInvitaciones { get; set; }

        public List<RedVirtualGrupoOtherUser> rvcSolicitudes { get; set; }

        public List<RedVirtualGrupoOtherUser> rvcOtros { get; set; }


    }

    public class RVCWall
    {
        public int id { get; set; }
        public String NameNVC { get; set; }

        public RVCWMessages Messages { get; set; }
        public List<RedVirtualRequesting> Aplicant { get; set; }

        public List<UserRVC> UsersRVC { get; set; }
        
    }

    public class UserRVC
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public Guid Id { get; set; }

        public int Genre { get; set; }
        public bool State { get; set; }

        public bool IsOwner { get; set; }
    }
}