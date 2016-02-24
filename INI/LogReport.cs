using INI.Models.DataBase;
using INI.Models.RangeFechas;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace INI
{
    public class LogReport
    {
        public static MemoryStream Reporte(ReportModel dr)
        {
            investicEntities db = new investicEntities();

            Document doc = new Document(PageSize.LEGAL, 2, 2, 50, 2);
            

            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            Font negrita = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD);
            Font normal = new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL);

            
            var datos = db.tblLogAcceso.Where(m => m.FechaInicioSesion >= dr.StartDate && m.FechaInicioSesion <= dr.EndDate).OrderByDescending(m=>m.FechaInicioSesion);

            if (dr.IsAdvancedSearch)
            {
                if(dr.IsRole){
                    datos = db.tblLogAcceso.Where(m => m.FechaInicioSesion >= dr.StartDate && m.FechaInicioSesion <= dr.EndDate && m.Rol.Contains(dr.criterion)).OrderByDescending(m => m.FechaInicioSesion);
                }
                else
                {
                    datos = db.tblLogAcceso.Where(m => m.FechaInicioSesion >= dr.StartDate && m.FechaInicioSesion <= dr.EndDate && m.Usuario.Contains(dr.criterion)).OrderByDescending(m => m.FechaInicioSesion);
                }
            }

            PdfPTable pdfTab = new PdfPTable(8) { WidthPercentage = 80 };
            
            pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTab.SpacingBefore = 20f;
            pdfTab.SpacingAfter = 20f;

            

            

            Phrase headert = new Phrase("REPORTE LOG DE ACCESO", negrita);
            PdfPCell cell=new PdfPCell(headert);
            cell.Colspan = 8;
            cell.HorizontalAlignment = 1;
            pdfTab.AddCell(cell);

            
            pdfTab.AddCell(new Phrase("Rol", negrita));
            pdfTab.AddCell(new Phrase("Usuario", negrita));
            pdfTab.AddCell(new Phrase("Ip", negrita));
            pdfTab.AddCell(new Phrase("Latitud", negrita));
            pdfTab.AddCell(new Phrase("Longitud", negrita));
            pdfTab.AddCell(new Phrase("Altitud", negrita));
            pdfTab.AddCell(new Phrase("F.Inicio sesión", negrita));
            pdfTab.AddCell(new Phrase("F.Cierre sesión", negrita));
            

            foreach (var info in datos)
            {                
                
                pdfTab.AddCell(new Phrase(info.Rol, normal));
                pdfTab.AddCell(new Phrase(info.Usuario, normal));
                pdfTab.AddCell(new Phrase(info.IP, normal));
                pdfTab.AddCell(new Phrase(info.Latitud, normal));
                pdfTab.AddCell(new Phrase(info.Longitud, normal));
                pdfTab.AddCell(new Phrase(info.Altitud, normal));
                pdfTab.AddCell(new Phrase(info.FechaInicioSesion.ToString(), normal));
                pdfTab.AddCell(new Phrase(info.FechaCierreSesion.ToString(), normal));               

            }

            doc.Add(pdfTab);
            doc.Close();

            return ms;

        }
    }
}