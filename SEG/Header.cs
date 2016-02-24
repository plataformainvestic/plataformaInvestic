using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace SEG
{
    public class Header : PdfPageEventHelper
    {
        Phrase[] header = new Phrase[2];

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            

        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            
        }
    }
}
