using System;
using System.Web.Providers.Entities;
using DocumentFormat.OpenXml.EMMA;
using INI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INI.Models.DataBase;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Security;
using System.Globalization;
using System.Text;
using INI.pages;


namespace INI
{
    public class BitacoraReport
    {
            
          public static MemoryStream Reporte(int idGrupodeInvestigacion)
            {
                investicEntities db = new investicEntities();

                Document doc = new Document(PageSize.LEGAL, 2, 2, 2, 2);

                MemoryStream ms = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Font negrita = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD);

                var datos = db.tblBitacoraGrupoInvestigacion.Where(x => x.idGrupoInvestigacion == idGrupodeInvestigacion).ToList();

                PdfPTable pdfTab = new PdfPTable(1) { WidthPercentage = 80 };
                pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTab.SpacingBefore = 20f;
                pdfTab.SpacingAfter = 20f;

                pdfTab.DefaultCell.Border = Rectangle.NO_BORDER;
              

                Phrase headert= new Phrase("Investigación como estrátegia pedagógica",negrita);
                pdfTab.AddCell(headert);

                foreach(var info in datos)
                {
                    //1



                    Phrase reflexion = new Phrase("Reflexion de la Onda: ", negrita);
                    pdfTab.AddCell(reflexion);



                    Phrase titulo = new Phrase("Titulo de la Investigación: ",negrita);
                    pdfTab.AddCell(titulo);
                    Phrase titulo1 = new Phrase(info.Titulo_1);
                    pdfTab.AddCell(titulo1);

                    Phrase nombre = new Phrase("Nombre del Grupo de Investigación: ", negrita);
                    pdfTab.AddCell(nombre);
                    Phrase nombre1 = new Phrase(info.Nombre_1);
                    pdfTab.AddCell(nombre1);


                    Phrase maestro = new Phrase("Nombre del Maestro Co-Investigador: ", negrita);
                    pdfTab.AddCell(maestro);
                    Phrase maestro1 = new Phrase(info.Maestro_1);
                    pdfTab.AddCell(maestro1);

                    Phrase intro = new Phrase("Introduccion: ", negrita);
                    pdfTab.AddCell(intro);
                    Phrase intro1 = new Phrase(info.Introduccion_1);
                    pdfTab.AddCell(intro1);

                    Phrase resumen = new Phrase("Resumen Informe Final: ", negrita);
                    pdfTab.AddCell(resumen);
                    Phrase resumen1 = new Phrase(info.ResumenFinal_1);
                    pdfTab.AddCell(resumen1);

                    //2

                    Phrase ruta = new Phrase("2. Ruta Meotodológica: ", negrita);
                    pdfTab.AddCell(ruta);

                    Phrase estar = new Phrase("2.1 Estar en la onda de Ondas: ", negrita);
                    pdfTab.AddCell(estar);

                    Phrase conformacion = new Phrase("Como se conformo tu grupo? ");
                    pdfTab.AddCell(conformacion);
             

                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(info.InsigniaGrupoURL_2));
                    logo.ScaleToFit(50f, 50f);
                    PdfPCell log = new PdfPCell(logo);
                    log.Border = Rectangle.NO_BORDER;
   
       
                    pdfTab.AddCell(log);

                    Phrase pertub = new Phrase("2.2 Pertubación de la Onda: ", negrita);
                    pdfTab.AddCell(pertub);
                    Phrase preg = new Phrase("Pregunta de Investigación");
                    pdfTab.AddCell(preg);
                    Phrase preg1 = new Phrase(info.PreguntaInvestigacion_2);
                    pdfTab.AddCell(preg1);

                    Phrase superp = new Phrase("2.3 Superposición de la Onda",negrita);
                    pdfTab.AddCell(superp);
                    Phrase describ = new Phrase("Descripción del problema");
                    pdfTab.AddCell(describ);
                    Phrase desc= new Phrase(info.DescripcionProblema_2);
                    pdfTab.AddCell(desc);

                    Phrase trayec = new Phrase("2.4 Diseño de las trayectorias de indagación",negrita);
                    pdfTab.AddCell(trayec);
                    Phrase obje = new Phrase("Objetivo general y Objetivos Especificos");
                    pdfTab.AddCell(obje);
                    Phrase obje1 = new Phrase(info.Objetivos_2);
                    pdfTab.AddCell(obje1);

                    Phrase trayec2 = new Phrase("2.5 Recorrido de las trayectorias de indagación", negrita);
                    pdfTab.AddCell(trayec2);
                    Phrase act = new Phrase("Actividades Realizadas");
                    pdfTab.AddCell(act);
                    Phrase act1 = new Phrase(info.ActividadesRealizadas_2);
                    pdfTab.AddCell(act1);

                    //3
                    Phrase reflex = new Phrase("3. Reflexión de la Onda - Estado del Arte ", negrita);
                    pdfTab.AddCell(reflex);

                    Phrase mapa = new Phrase("Mapa conceptual - Conceptos Principales");
                    pdfTab.AddCell(mapa);
                    Phrase mapa2 = new Phrase(info.ConceptosPrincipales_3);
                    pdfTab.AddCell(mapa2);

                    Phrase resul = new Phrase("Resultados: Registra todos los resultados analizados y sistematizados de los instrumentos desarrollados en el trabajo de campo");
                    pdfTab.AddCell(resul);
                    Phrase resul1 = new Phrase(info.Resultalos_3);
                    pdfTab.AddCell(resul);

                    Phrase propa = new Phrase("3. Propagación de la Onda ", negrita);
                    pdfTab.AddCell(propa);

                    Phrase prop = new Phrase("Presenta los espacios de participación internos y externos");
                    pdfTab.AddCell(prop);

                    Phrase partic = new Phrase(info.Participacion_3);
                    pdfTab.AddCell(partic);
                    //4

                    Phrase conclu = new Phrase("4.Conclusiones",negrita);
                    pdfTab.AddCell(conclu);

                    Phrase reg = new Phrase("Registra el analisis global de los resultados");
                    pdfTab.AddCell(reg);
                    Phrase reg1 = new Phrase(info.Concluciones_4);
                    pdfTab.AddCell(reg1);


                    //5

                    Phrase biblio = new Phrase("5. Bibliografia y/o WebGrafia",negrita);
                    pdfTab.AddCell(biblio);
                    Phrase refe= new Phrase("Referencias");
                    pdfTab.AddCell(refe);
                    Phrase refe1 = new Phrase(info.Bibliografia_5);
                    pdfTab.AddCell(refe1);


                    //6
                    Phrase anex = new Phrase("6. Anexos", negrita);
                    pdfTab.AddCell(anex);
                    Phrase fich = new Phrase("Fichas de instrumentos aplicados y registro de fotos generales");
                    pdfTab.AddCell(fich);

                    Phrase ponencia = new Phrase("Ponencia", negrita);
                    pdfTab.AddCell(ponencia);
                    Phrase ponen1 = new Phrase(info.Ponencia_6);
                    pdfTab.AddCell(ponen1);

                }

                doc.Add(pdfTab);
              

               
                doc.Close();

                return ms;

            }








        }
    
}