using System;
using System.Web.Providers.Entities;
using DocumentFormat.OpenXml.EMMA;
using SEG.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEG.Models.DataBase;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Security;
using System.Globalization;
using SEG.pages;
using System.Text;



namespace SEG
{
    public class PDFReports
    {
            
          public static MemoryStream Reporte(Reporte repodata)
            {
                Entities db = new Entities();
              

                List<actividade> data = new List<actividade>();
                data = db.actividades.Where(x => x.Id_Contratista.Equals(repodata.UserId)).ToList();
                List<producto> prolist = new List<producto>();
                List<evidencia> evilist = new List<evidencia>();
                List<responsabilidade> contra = new List<responsabilidade>();
                contra = db.responsabilidades.Where(x=>x.Id_Contratista.Equals(repodata.UserId)).ToList();

                StringBuilder productos = new StringBuilder();
                StringBuilder evidencias = new StringBuilder();

                
                


                string cedu = (from c in db.AspNetUsers where c.Id.Equals(repodata.UserId) select c.Cedula).SingleOrDefault();
                string equipcons = (from e in db.AspNetUsers where e.Id.Equals(repodata.UserId) select e.Equipo).SingleOrDefault();
                int ecc = Convert.ToInt32(equipcons);
                string equip = (from e in db.equipos where e.Id_Equipo == ecc select e.Nombre_Equipo).SingleOrDefault();
                string nombrecoord = (from e in db.equipos where e.Id_Equipo == ecc select e.Nombre_Coordinador).SingleOrDefault();

                string nom = (from n in db.AspNetUsers where n.Id.Equals(repodata.UserId) select n.Nombres).SingleOrDefault();
                string ape = (from a in db.AspNetUsers where a.Id.Equals(repodata.UserId) select a.Apellidos).SingleOrDefault();
                string carg = (from b in db.AspNetUsers where b.Id.Equals(repodata.UserId) select b.Cargo).SingleOrDefault();
                string nct = (from h in db.AspNetUsers where h.Id.Equals(repodata.UserId) select h.Contrato).SingleOrDefault();
                string cdp = (from d in db.AspNetUsers where d.Id.Equals(repodata.UserId) select d.Cdp).SingleOrDefault();

               
              
                Font negrita  = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD);
                Font negritasmall = new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD);

                Font calibri = FontFactory.GetFont("Verdana", 10f, Font.NORMAL);
                Font calibrinegrita = FontFactory.GetFont("Verdana", 10f, Font.BOLD);

              


                Paragraph tipoinforme = new Paragraph();

                PdfPTable tablaInfo = new PdfPTable(7);
                PdfPTable tablaInfoMens = new PdfPTable(7);

                PdfPTable pdfTab = new PdfPTable(5) { WidthPercentage = 80 };

                

                pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTab.SpacingBefore = 20f;
                pdfTab.SpacingAfter = 20f;


                Document doc = new Document(PageSize.A4.Rotate(), 2, 2, 100, 2);
                
                MemoryStream ms = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                
                doc.Open();

                pdfPage PageEventHandler = new pdfPage();

              
                writer.PageEvent = PageEventHandler;

        //aqui iba el codigo de la cabezera

               // Paragraph tipoinforme = new Paragraph();
              // SI TIPO DE INFORME ES semanal

                if (repodata.Tipo == 1)
                {
                    //tipoinforme = new Paragraph("INFORME SEMANAL DE ACTIVIDADES", calibrinegrita);
                    //tipoinforme.Alignment = Element.ALIGN_CENTER;
                    //tipoinforme.SpacingAfter = 10f;

                    Phrase fecha = new Phrase("Fecha Reporte: ", calibrinegrita);
                    tablaInfo.AddCell(fecha);
                    tablaInfo.AddCell(System.DateTime.Now.ToString("dd/MM/yyyy"));
                    PdfPCell celda1 = new PdfPCell();
                    celda1.Colspan = 3;
                    tablaInfo.AddCell(celda1);
                    Phrase equipo = new Phrase("Equipo: ", calibrinegrita);
                    tablaInfo.AddCell(equipo);
                    tablaInfo.AddCell(equip);
                    Phrase cedula = new Phrase("Cedula: ", calibrinegrita);
                    tablaInfo.AddCell(cedula);
                    tablaInfo.AddCell(cedu);
                    Phrase nombre = new Phrase("Nombre: ", calibrinegrita);
                    tablaInfo.AddCell(nombre);
                    tablaInfo.AddCell(nom);
                    tablaInfo.AddCell(ape);
                    Phrase cargo = new Phrase("Cargo: ", calibrinegrita);
                    tablaInfo.AddCell(cargo);
                    tablaInfo.AddCell(carg);
                    Phrase periodo = new Phrase("Período: ", calibrinegrita);
                    tablaInfo.AddCell(periodo);
                    tablaInfo.AddCell("desde ");
                    PdfPCell celda2 = new PdfPCell(new Phrase(repodata.Fechainicial.ToString("dd/MM/yyyy")));
                    celda2.Colspan = 2;
                    tablaInfo.AddCell(celda2);
                    tablaInfo.AddCell("hasta");
                    PdfPCell celda3 = new PdfPCell(new Phrase(repodata.FechaFinal.ToString("dd/MM/yyyy")));
                    celda3.Colspan = 2;
                    tablaInfo.AddCell(celda3);



                    // Encabezado para  informe semanal  
                    PdfPCell celdatarea = new PdfPCell(new Phrase("TAREAS", calibrinegrita));
                    celdatarea.HorizontalAlignment = 1;
                    pdfTab.AddCell(celdatarea);

                    PdfPCell celdaestado = new PdfPCell(new Phrase("ESTADO", calibrinegrita));
                    celdaestado.HorizontalAlignment = 1;
                    pdfTab.AddCell(celdaestado);

                    PdfPCell celdaproducto = new PdfPCell(new Phrase("PRODUCTO", calibrinegrita));
                    celdaproducto.HorizontalAlignment = 1;
                    pdfTab.AddCell(celdaproducto);

                    PdfPCell celdaevidencia = new PdfPCell(new Phrase("EVIDENCIA", calibrinegrita));
                    celdaevidencia.HorizontalAlignment = 1;
                    pdfTab.AddCell(celdaevidencia);

                    PdfPCell celdaobservaciones = new PdfPCell(new Phrase("OBSERVACIONES", calibrinegrita));
                    celdaobservaciones.HorizontalAlignment = 1;
                    pdfTab.AddCell(celdaobservaciones);

                    //Ciclo informe semanal
                    foreach (var actividad in data)
                    {
                        if (actividad.Fecha_Ini >= repodata.Fechainicial)
                        {
                            if (actividad.Fecha_Fin <= repodata.FechaFinal)
                            {
                                pdfTab.AddCell(actividad.Des_Actividad);
                                pdfTab.AddCell(actividad.tabla_estados.Des_Estado);

                                prolist = db.productos.Where(x => x.Id_Contratista.Equals(repodata.UserId)).Where(i => i.Id_Actividad == actividad.Id_Actividad).ToList();
                                foreach (var np in prolist)
                                {

                                    productos.Append(np.Nombre_Producto + " , ");

                                }
                                evilist = db.evidencias.Where(x => x.Id_Contratista.Equals(repodata.UserId)).Where(i => i.Id_Actividad == actividad.Id_Actividad).ToList();


                                foreach (var ne in evilist)
                                {
                                    evidencias.Append(ne.Nombre_Evidencia + " , ");
                                }

                                pdfTab.AddCell(productos.ToString());
                                pdfTab.AddCell(evidencias.ToString());

                             
                          
                                pdfTab.AddCell(actividad.Des_Observaciones);
                            }
                        }


                    }
                    
                }
                

               // SI TIPO DE INFORME ES mensual
                if (repodata.Tipo == 0)
                {
                     //tipoinforme = new Paragraph("INFORME MENSUAL DE ACTIVIDADES", calibrinegrita);
                     //tipoinforme.Alignment = Element.ALIGN_CENTER;
                     //tipoinforme.SpacingAfter = 10f;

                     var colWidthPercentages = new[] { 5f, 5f, 30f, 20f, 20f };
                     pdfTab.SetWidths(colWidthPercentages);

                     Phrase fecha = new Phrase("Fecha Reporte: ", calibrinegrita);
                     tablaInfoMens.AddCell(fecha);
                     tablaInfoMens.AddCell(System.DateTime.Now.ToString("dd/MM/yyyy"));
                     PdfPCell celda1 = new PdfPCell();
                     celda1.Colspan = 3;
                     tablaInfoMens.AddCell(celda1);
                     Phrase equipo = new Phrase("Equipo: ", calibrinegrita);
                     tablaInfoMens.AddCell(equipo);
                     tablaInfoMens.AddCell(equip);
                     Phrase cedula = new Phrase("Cedula: ", calibrinegrita);
                     tablaInfoMens.AddCell(cedula);
                     tablaInfoMens.AddCell(cedu);
                     Phrase nombre = new Phrase("Nombre: ", calibrinegrita);
                     tablaInfoMens.AddCell(nombre);
                     tablaInfoMens.AddCell(nom);
                     tablaInfoMens.AddCell(ape);
                     Phrase cargo = new Phrase("Cargo: ", calibrinegrita);
                     tablaInfoMens.AddCell(cargo);
                     tablaInfoMens.AddCell(carg);
                     Phrase nocontra = new Phrase("No. Contrato", calibrinegrita);
                     tablaInfoMens.AddCell(nocontra);
                     tablaInfoMens.AddCell(nct);
                     Phrase nocdp = new Phrase("No. CDP", calibrinegrita);
                     tablaInfoMens.AddCell(nocdp);
                     PdfPCell celdancp = new PdfPCell(new Phrase(cdp));
                     celdancp.Colspan = 2;
                     tablaInfoMens.AddCell(celdancp);
                     Phrase nrp = new Phrase("No. RP", calibrinegrita);
                     tablaInfoMens.AddCell(nrp);
                     tablaInfoMens.AddCell("-");
                     Phrase periodo = new Phrase("Período: ", calibrinegrita);
                     tablaInfoMens.AddCell(periodo);
                     tablaInfoMens.AddCell("desde ");
                     tablaInfoMens.AddCell(repodata.Fechainicial.ToString("dd/MM/yyyy"));
                     tablaInfoMens.AddCell(" hasta ");
                     tablaInfoMens.AddCell(repodata.FechaFinal.ToString("dd/MM/yyyy"));
                     Phrase actimes = new Phrase("Actividades del mes de: ", calibrinegrita);
                     tablaInfoMens.AddCell(actimes);
                     tablaInfoMens.AddCell(repodata.FechaFinal.ToString("MMMM").ToUpper());


                     PdfPCell celdalt = new PdfPCell(new Phrase("ALTERNATIVA", negritasmall));
                     celdalt.Rotation = 90;
                     celdalt.PaddingTop = 2f;
                     celdalt.PaddingLeft = 14f;
                     pdfTab.AddCell(celdalt);

                     PdfPCell celdaresp = new PdfPCell(new Phrase("RESP. CONTRAC.", negritasmall));
                     celdaresp.Rotation = 90;
                     celdaresp.PaddingLeft = 14f;
                     
                     pdfTab.AddCell(celdaresp);

                     PdfPCell celdasub = new PdfPCell(new Phrase("SUBACTIVIDADES", calibrinegrita));
                     celdasub.HorizontalAlignment = 1;
                     celdasub.PaddingTop = 20f;
                     pdfTab.AddCell(celdasub);

                     PdfPCell celdaevidencia = new PdfPCell(new Phrase("EVIDENCIA", calibrinegrita));
                     celdaevidencia.HorizontalAlignment = 1;
                     celdaevidencia.PaddingTop = 20f;
                     pdfTab.AddCell(celdaevidencia);

                     PdfPCell celdaproduc = new PdfPCell(new Phrase("PRODUCTO", calibrinegrita));
                     celdaproduc.HorizontalAlignment = 1;
                     celdaproduc.PaddingTop = 20f;
                     pdfTab.AddCell(celdaproduc);

               
           

                     foreach (var actividad in data)
                     {
                         if (actividad.Fecha_Ini >= repodata.Fechainicial)
                         {
                             if (actividad.Fecha_Fin <= repodata.FechaFinal)
                             {
                                 pdfTab.AddCell(actividad.tabla_alternativas.Id_Alternativa.ToString());
                                 pdfTab.AddCell(actividad.responsabilidade.IdentificadorResponsa);
                                 pdfTab.AddCell(actividad.Des_Actividad);


                                 prolist = db.productos.Where(x => x.Id_Contratista.Equals(repodata.UserId)).Where(i => i.Id_Actividad == actividad.Id_Actividad).ToList();
                                 foreach (var np in prolist)
                                 {

                                     productos.Append(np.Nombre_Producto + " , ");

                                 }
                                 evilist = db.evidencias.Where(x => x.Id_Contratista.Equals(repodata.UserId)).Where(i => i.Id_Actividad == actividad.Id_Actividad).ToList();


                                 foreach (var ne in evilist)
                                 {
                                     evidencias.Append(ne.Nombre_Evidencia + " , ");
                                 }

                                 pdfTab.AddCell(evidencias.ToString());

                                 pdfTab.AddCell(productos.ToString());



                             }
                         }


                     }

                }

                


           


           

              
    

             //   doc.Add(logo);
              //  doc.Add(header);
              //  doc.Add(tipoinforme);
                if (repodata.Tipo == 1) {doc.Add(tablaInfo); }
                if (repodata.Tipo == 0) { doc.Add(tablaInfoMens); }
               
                doc.Add(pdfTab);

                doc.NewPage();

                Paragraph header2 =
                    new Paragraph(" ", calibrinegrita);
                header2.Alignment = Element.ALIGN_CENTER;
                header2.SpacingBefore = 5f;
                header2.SpacingAfter = 10f;
                header2.IndentationRight = 100f;
                header2.IndentationLeft = 130f;
         
                Phrase nombrefinal = new Phrase(nom, negrita);
                Phrase apefinal = new Phrase(ape, negrita);

                PdfPTable infofin = new PdfPTable(4);
                infofin.HorizontalAlignment = Element.ALIGN_CENTER;
                infofin.SpacingBefore = 100f;
                infofin.SpacingAfter = 100f;
                infofin.DefaultCell.Border = Rectangle.NO_BORDER;

                Phrase nf = new Phrase(nom+" "+ape, negrita);
                infofin.AddCell(nf);

                Phrase nomcor = new Phrase(nombrecoord.ToUpper(), negrita);
                infofin.AddCell(nomcor);
                Phrase silv = new Phrase("SILVIO IBARRA ROSERO", negrita);
                infofin.AddCell(silv);
                Phrase myri = new Phrase("MYRIAM ROSERO", negrita);
                infofin.AddCell(myri);
                
                infofin.AddCell("C.C."+cedu);
                
                infofin.AddCell("Coordinador Equipo");
                
                infofin.AddCell("Asesor de Seguimiento y Control");
                
                infofin.AddCell("Coordinadora General del Proyecto");

                doc.Add(header2);
                doc.Add(infofin);


                if (repodata.Tipo == 0)
                {
                    PdfPTable inforesponsa = new PdfPTable(2) { WidthPercentage = 50 };
                    inforesponsa.HorizontalAlignment = Element.ALIGN_CENTER;


                    var tamano = new[] { 8f, 42f };
                    inforesponsa.SetWidths(tamano);


                    PdfPCell encabezr = new PdfPCell(new Phrase("RESPONSABILIDADES", negrita));
                    encabezr.Colspan = 2;
                    encabezr.HorizontalAlignment = 1;
                    inforesponsa.AddCell(encabezr);

                    Phrase idr = new Phrase("Identificador", negrita);
                    inforesponsa.AddCell(idr);
                    Phrase desr = new Phrase("Descripcion", negrita);
                    inforesponsa.AddCell(desr);


                    foreach (var responsa in contra)
                    {
                        inforesponsa.AddCell(responsa.IdentificadorResponsa);
                        inforesponsa.AddCell(responsa.Descripcion);
                    }

                    doc.Add(inforesponsa);
                }









               
                
              

               
                doc.Close();

                return ms;

            }








        }
    
}