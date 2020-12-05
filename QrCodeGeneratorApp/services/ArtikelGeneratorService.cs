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
    public class ArtikelGeneratorService
    {
       
        public void handlePdfSave(ArtikelDto artikel, ArtikelSearch artikelSearch, Pages.Artikel artikelPage)
        {

            string textToCode = artikel.Artikelnummer + "||" + artikel.UserInternId + "||" + artikel.Ean;
            if (artikelSearch.LieferantId is not null && artikelSearch.LieferantId!="")
            {
                
                textToCode = textToCode + "||" + artikel.Lieferant;

            }
            if (artikelSearch.Year is not null && artikelSearch.Year!=0)
            {
                textToCode = textToCode + "||" + artikelSearch.Year;
            }
            if (artikelSearch.Season is not null && artikelSearch.Season!="")
            {
                textToCode = textToCode + "||" + artikelSearch.Season;
            }

            generateQR(textToCode, artikel,artikelPage);


            string folderName = folderNameFromDate();
            string PathString = System.IO.Path.Combine(ApplicationValues.PdfTopLevelFolderLager, folderName);
            System.IO.Directory.CreateDirectory(PathString);

            artikelPage.artikelPath = PathString;

            if (artikelSearch.LieferantId is null || artikelSearch.LieferantId=="")
            {
                artikel.LieferantCode = null;

            }
            generatePDF(artikel, PathString,artikelPage);

            artikelPage.flag = false;
            artikelPage.completionMessage = "Vollständig!";
            artikelPage.messageClass = "alert alert-success col-lg-6";
            artikelPage.updateUI();

        }
        public static void generateQR(string textToCode, ArtikelDto p,Pages.Artikel artikelPage)
        {


            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textToCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(32);

            string folderName = p.UserInternId + ".jpg";

            try
            {
                qrCodeImage.Save(ApplicationValues.ImageLocation + folderName);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                artikelPage.errorMessage = ex.Message;
                artikelPage.updateUI();
            }
            

        }
        public static void generatePDF(ArtikelDto artikel, string pathString,Pages.Artikel artikelPage)
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

            if (artikel.LieferantCode is not null)
            {
                html2.Append("<h1>");
                html2.Append(artikel.UserInternId);
                html2.Append("-");
                html2.Append(artikel.LieferantCode);

            }
            else
            {
                html2.Append("<h1 style=\"font-size:270px;\">");
                html2.Append(artikel.UserInternId);
            }
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
                .WithGlobalSetting("margin.right", "0cm")
                .WithGlobalSetting("margin.top", "0cm")
                .WithGlobalSetting("margin.bottom", "0cm")
                .WithGlobalSetting("margin.left", "0cm")
                .WithObjectSetting("web.defaultEncoding", "utf-8")
                .Content();
            try
            {
                File.WriteAllBytes(pathString + "/" + artikel.UserInternId + 
                    "_" + artikel.Artikelnummer + ".pdf", pdf);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                artikelPage.errorMessage = ex.Message;
                artikelPage.updateUI();

            }



        }
        public static string folderNameFromDate()
        {
            DateTime now = DateTime.Now;
            string folderName = now.Day + "_" + now.Month + "_" + now.Year + "_"
                + now.Hour + "_" + now.Minute + "_" + now.Second;
            return folderName;
        }
    }
}
