using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI.Models
{
    class FileMetaData
    {
        public string FileId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ContentType { get; set; }

        public int Size { get; set; }
    }
}
