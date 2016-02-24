using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Institution
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Municipio { get; set; }
    }

    public class InstitunionList
    {
        public int totalResultsCount { get; set; }
        public List<Institution> Institutions { get; set; }
    }

    public class ResearchLine
    {
        public int id { get; set; }
        public string Name { get; set; }
    }

    public class ResearchLineList
    {
        public int totalResultsCount { get; set; }
        public List<ResearchLine> ResearchLineItems { get; set; }
    }

    public class UploadFile
    {
        public int id { get; set; }
        public int ResearchGroupId { get; set; }
        public string FileToUpload { get; set; }
        public bool Acceptance { get; set; }
    }   
}
