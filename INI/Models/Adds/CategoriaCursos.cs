using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INI.Models.Database;

namespace INI.Models.Adds
{
    public class CategoriaCursos
    {

        public long id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }

    public class CursosChamilo
    {
        public int id { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string category_code { get; set; }
        public string tutor_name { get; set; }
    }
    public class ListaCursos
    {
        public int totalResultsCount { get; set; }
        public List<CursosChamilo> Cursos { get; set; }

    }
    public class ListaCategorias {

        public int totalResultsCount { get; set; }
        public List<CategoriaCursos> Categorias { get; set; }
    }
    public class CuentaCursos
    {
        public int Notecinf { get; set; }
        public int NoCNat { get; set; }
        public int NoMat { get; set; }
        public int CSoc { get; set; }
        public int NoLeng { get; set; }
        public int NoIngles { get; set; }
        public int NoProyTrans { get; set; }
        public int Nootros { get; set; }
    }


}