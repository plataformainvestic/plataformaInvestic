using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEG.Extensions.ActionResults
{

    public class PDFResult : ActionResult
    {
        private readonly MemoryStream _memoryStream;
        private readonly string _fileName;

        public PDFResult(MemoryStream memoryStream, string fileName)
        {
            _memoryStream = memoryStream;
            _fileName = fileName;
          
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.ContentType = "application/pdf";
            response.AddHeader("Content-Disposition",
                               "attachment;filename=\"" + _fileName + ".pdf\"");
            response.BinaryWrite(_memoryStream.ToArray());
            response.End();
        }
    }

}