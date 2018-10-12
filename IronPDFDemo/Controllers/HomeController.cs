using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IronPdf;
using System.IO;

namespace IronPDFDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AcousticReport()
        {
            var staticPageToRender = new FilePathResult("~/Views/Home/AcousticReport.html", "text/html");
            return staticPageToRender;
        }

        public string ReadHTML()
        {
            string path = Server.MapPath(@"\Views\Home\AcousticReport.html");
            StreamReader streamReader = new System.IO.StreamReader(path);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            return text;
        }

        public Boolean GeneratePdf()
        {
            Random rnd = new Random();
            HtmlToPdf renderer = null;
            PdfDocument pdf = null;
            string pdfUrl = String.Empty;
            string basePath = String.Empty;
            string outputPath = String.Empty;
            string outputName = String.Empty;
            int rand = rnd.Next(0, 10000);

            for (int i = 0; i< 1; i++)
            {
                renderer = new HtmlToPdf();
                renderer.PrintOptions.EnableJavaScript = true;
                renderer.PrintOptions.FitToPaperWidth = true;
                renderer.PrintOptions.MarginLeft = 0;
                renderer.PrintOptions.MarginRight = 0;
                renderer.PrintOptions.MarginTop = 0;
                renderer.PrintOptions.MarginBottom = 0;
                renderer.PrintOptions.RenderDelay = 10000;
                //pdfUrl = "https://en.wikipedia.org/wiki/List_of_compositions_by_Franz_Schubert";
                //pdfUrl = "https://en.wikipedia.org/wiki/Christianity";
                pdfUrl = "http://www.chartjs.org/samples/latest/scales/gridlines-display.html";
                pdf = renderer.RenderUrlAsPdf(pdfUrl);
                basePath = "C:\\Users\\HammerOfTalos\\Desktop\\ReportTest\\";
                outputName = "AcousticReport_" + i + "_" + 0 + ".pdf";
                outputPath = String.Concat(basePath, outputName);
                DeleteFileIfExist(basePath, outputName);
                pdf.SaveAs(outputPath);
            }
            
            return true;
        }

        private void DeleteFileIfExist(string directory, string fileName)
        {
            string path = String.Concat(directory, fileName);
            if (Directory.Exists(directory) && System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}



//var pdf = Renderer.RenderHTMLFileAsPdf("/Views/Home/AcousticReport.html");

//var renderer = new IronPdf.HtmlToPdf();
//var pdf = Renderer.RenderHtmlAsPdf(ReadHTML());
//var pdf = renderer.RenderHTMLFileAsPdf(@"C: \Users\HammerOfTalos\Documents\_Projects\IronPDFDemo\IronPDFDemo\Views\Home\AcousticReport.html");
//var outputPath = "C:\\Users\\HammerOfTalos\\Desktop\\AcousticReport.pdf";
//pdf.SaveAs(outputPath);