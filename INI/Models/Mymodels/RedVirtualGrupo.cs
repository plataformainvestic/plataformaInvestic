using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INI.Models.Mymodels
{
    public class RedVirtualGrupoOwner
    {
        public int id { get; set; }
        public String Name { get; set; }
        public Boolean State { get; set; }
        public Boolean IsOwner { get; set; }

        public DateTime CreateDate { get; set; }
    }

    public class RedVirtualGrupoOtherUser:RedVirtualGrupoOwner
    {                   
        public String NameUser { get; set; }
        public Guid idUser { get; set; }        
        public int StateUserAceptGroup { get; set; }
        
    }

    public class RedVirtualRequesting : RedVirtualGrupoOtherUser
    {
        public Guid idAplicant { get; set; }
    }

    public class RedVirtualWallMessage
    {
        
        public Guid idUser { get; set; }
        public bool IsOwner { get; set; }
        public bool IsMine { get; set; }
        public String Message { get; set; }        
        public DateTime DataSend { get; set; }


    }

    public class RVCWMessages
    {
        public List<RedVirtualWallMessage> RedvirtualWallMessage { get; set; }
        public int idNVCGrpUser { get; set; }
    }

    

}