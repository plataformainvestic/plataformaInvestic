using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Collections;
using System.Net;

namespace INI.pages
{
    public class seg_pdfPage : iTextSharp.text.pdf.PdfPageEventHelper
    {
        public override void OnStartPage(PdfWriter writer, Document doc)
        {

            //Font calibrinegrita = FontFactory.GetFont("Verdana", 10f, Font.BOLD);

            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/logo_udenar.png"));
            //logo.Alignment = iTextSharp.text.Image.TEXTWRAP;
            //logo.IndentationLeft = 100f;
            //logo.ScaleToFit(60f, 60f);
            //logo.SetAbsolutePosition(80f, doc.PageSize.Height - 80f);


            //Paragraph header =
            //    new Paragraph(
            //        "FORTALECIMIENTO DE LA CULTURA CIUDADANA Y DEMOCRÁTICA EN  CTeI A TRAVÉS DE LA INVESTIGACIÓN COMO ESTRATEGIA PEDAGÓGICA APOYADA EN TICs EN EL DEPARTAMENTO DE NARIÑO", calibrinegrita);
            //header.Alignment = Element.ALIGN_CENTER;
            //header.SpacingBefore = 30f;
            //header.SpacingAfter = 10f;
            //header.IndentationRight = 100f;
            //header.IndentationLeft = 130f;

            //doc.Add(logo);
            //doc.Add(header);

            base.OnStartPage(writer, doc);
        }
        public override void OnEndPage(PdfWriter writer, Document doc)
        {

            BaseFont f_cn = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/SEG/logo_udenar.png"));
            logo.Alignment = iTextSharp.text.Image.TEXTWRAP;
            logo.IndentationLeft = 100f;
            logo.ScaleToFit(60f, 60f);
            logo.SetAbsolutePosition(80f, doc.PageSize.Height - 80f);

            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(f_cn, 10);
            cb.SetTextMatrix(190, 560);
            cb.ShowText("FORTALECIMIENTO DE LA CULTURA CIUDADANA Y DEMOCRÁTICA EN  CTeI A TRAVÉS DE LA INVESTIGACIÓN");
            cb.EndText();

            //cb.ShowText("COMO ESTRATEGIA PEDAGÓGICA APOYADA EN TICs EN EL DEPARTAMENTO DE NARIÑO");
            //cb.EndText();

            PdfContentByte cb2 = writer.DirectContent;
            cb2.BeginText();
            cb2.SetFontAndSize(f_cn, 10);
            cb2.SetTextMatrix(240, 550);
            cb2.ShowText("COMO ESTRATEGIA PEDAGÓGICA APOYADA EN TICs EN EL DEPARTAMENTO DE NARIÑO");
            cb2.EndText();

            PdfContentByte cb3 = writer.DirectContent;
            cb3.BeginText();
            cb3.SetFontAndSize(f_cn, 10);
            cb3.SetTextMatrix(390, 520);
            cb3.ShowText("INFORME DE ACTIVIDADES");
            cb3.EndText();
          


            BaseColor grey = new BaseColor(128, 128, 128);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);
            //tbl footer
            PdfPTable footerTbl = new PdfPTable(1);
            footerTbl.TotalWidth = doc.PageSize.Width;
            //img footer
           // iTextSharp.text.Image foot = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("/Images/footer.jpg"));


         //   foot.ScalePercent(45);

            footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;
           // PdfPCell cell = new PdfPCell(foot);
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
          //  cell.Border = 0;
         //   footerTbl.AddCell(cell);


            //page number
            Chunk myFooter = new Chunk("Página " + (doc.PageNumber), FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 8, grey));
            PdfPCell footer = new PdfPCell(new Phrase(myFooter));
            footer.Border = Rectangle.NO_BORDER;
            footer.HorizontalAlignment = Element.ALIGN_CENTER;
            footerTbl.AddCell(footer);

            footerTbl.WriteSelectedRows(0, -1, 0, (doc.BottomMargin + 20), writer.DirectContent);
            doc.Add(logo);
           // doc.Add(header);
            
        }

    }
}