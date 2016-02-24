using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IEP.Models.DataBase
{
    public class MisGrupos
    {
        public int id { get; set; }

        public string Avatar { get; set; }

        public InformacionGrupo Informacion { get; set; }
    }

    public class InformacionGrupo
    {
        public int idGrupo { get; set; }

        public string Descripcion { get; set; }

        public string NombreGrupo { get; set; }

        public string Municipio { get; set; }

        public string Institucion { get; set; }

        public string Pregunta { get; set; }

        public bool EsCreador { get; set; }
    }

    public class Grado
    {
        public int idGrado { get; set; }
        public string Nombre { get; set; }
    }

    public class MiembroGrupo
    {
        public RegisterViewModel User { get; set; }
        public tblMiembroGrupo Informacion { get; set; }

        public IEnumerable<SelectListItem> RolesItems
        {
            get
            {
                InvesticEntities db = new InvesticEntities();
                return new SelectList(db.tblRol, "id", "Nombre");
            }
        }

        public IEnumerable<SelectListItem> GradesItems
        {
            get
            {
                List<Grado> grades = new List<Grado>();
                grades.Add(new Grado { idGrado = 0, Nombre = "Cero" });
                grades.Add(new Grado { idGrado = 1, Nombre = "Primero" });
                grades.Add(new Grado { idGrado = 2, Nombre = "Segundo" });
                grades.Add(new Grado { idGrado = 3, Nombre = "Tercero" });
                grades.Add(new Grado { idGrado = 4, Nombre = "Cuarto" });
                grades.Add(new Grado { idGrado = 5, Nombre = "Quinto" });
                grades.Add(new Grado { idGrado = 6, Nombre = "Sexto" });
                grades.Add(new Grado { idGrado = 7, Nombre = "Septimo" });
                grades.Add(new Grado { idGrado = 8, Nombre = "Octavo" });
                grades.Add(new Grado { idGrado = 9, Nombre = "Noveno" });
                grades.Add(new Grado { idGrado = 10, Nombre = "Decimo" });
                grades.Add(new Grado { idGrado = 11, Nombre = "Once" });
                return new SelectList(grades, "idGrado", "Nombre");
            }
        }
    }

    public class MiembroCoInvestigador
    {
        public int idGrupo { get; set; }
        public List<AspNetUsers> Users { get; set; }
    }

    public class MaestroCoinvestigador
    {
        public RegisterViewModel User { get; set; }
        public tblMaestroCoInvestigador Informacion { get; set; }
    }
}