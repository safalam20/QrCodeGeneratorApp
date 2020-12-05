using Microsoft.AspNetCore.Components;
using OpenHtmlToPdf;
using QrCodeGeneratorApp.dto;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeGeneratorApp.services
{
    public class LiferantGeneratorService
    {
          
        public void handlePdfSave(List<ArtikelDto> artikels, ManufacturerSearch search, Pages.Index i)
        {
                      
            foreach (ArtikelDto p in artikels)

            {
                string textToCode = p.Artikelnummer + "||" + p.UserInternId + "||" + p.Ean + "||" +
                    p.Lieferant + "||" + search.Year + "||" + search.Season;

                generateQR(textToCode,p,i);

            }
            string folderName = search.LieferantId;
            string PathString = System.IO.Path.Combine(ApplicationValues.PdfTopLevelFolderProduktion, folderName);
            if (!Directory.Exists(PathString))
            {
                Directory.CreateDirectory(PathString);
            }
            
            
            i.pathIndex = PathString;

            i.allCount = artikels.Count;

            foreach (ArtikelDto p in artikels)

            {
                generatePDF(p,PathString,i);
                i.countCreated++;
                i.updateUI();               
            }
            i.flag = false;
            i.completionMessage = "Vollständig!";
            i.messageClass = "alert alert-success col-5";
            i.updateUI();
            
        }
       
        public static void generateQR(string textToCode,ArtikelDto p,Pages.Index indexPage)
        {
            

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textToCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(32);

            string folderName =p.UserInternId+".jpg";

            try
            {
                qrCodeImage.Save(ApplicationValues.ImageLocation + folderName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                indexPage.errorMessage = ex.Message;
                indexPage.updateUI();
            }
          

        }
        public static void generatePDF(ArtikelDto artikel, string pathString, Pages.Index indexPage)
        {          
            var html2 = new StringBuilder();
            html2.Append("<!DOCTYPE html>");
            html2.Append("<html>");
            html2.Append("<head>");
            html2.Append("<style>");
            html2.Append("html,body,h1{margin:0; padding:0; text-align:center; font-size:200px;}");
            html2.Append("</style>");
            html2.Append("</head>");
            html2.Append("<body>");
            html2.Append("<div>");
            html2.Append("<h1>");
            html2.Append(artikel.UserInternId);
            html2.Append("-");
            html2.Append(artikel.LieferantCode);        
            html2.Append("</h1>");
            html2.Append("<img style=\"display:block; margin-left:auto; margin-right:auto;\" width=\"180px\" height=\"180px\" src=\"");
            html2.Append(ApplicationValues.ImageLink);
            html2.Append(artikel.UserInternId);
            html2.Append(".jpg\">");
            html2.Append("</div");
            html2.Append("</body");
            html2.Append("</html");



            var pdf = Pdf
                .From(html2.ToString())
                .WithGlobalSetting("orientation", "Portrait")
                .WithGlobalSetting("size.width", "15cm")
                .WithGlobalSetting("size.height", "10cm")
                .WithGlobalSetting("margin.right","0cm")
                .WithGlobalSetting("margin.top", "0cm")
                .WithGlobalSetting("margin.bottom", "0cm")
                .WithGlobalSetting("margin.left", "0cm")
                .WithObjectSetting("web.defaultEncoding", "utf-8")
                .Content();
            try
            {
                File.WriteAllBytes(pathString + "/" + artikel.UserInternId 
                    + "_" + artikel.Artikelnummer + ".pdf", pdf);
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                indexPage.errorMessage = ex.Message;
                indexPage.countCreated--;
                indexPage.updateUI();
                
            }
            
            
            
        }
        
    }
}
