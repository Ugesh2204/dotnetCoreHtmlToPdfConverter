using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PdfGenerator.Controllers
{
    [Route("api/PdfCreator")]
    [ApiController]
    public class PdfCreatorController : Controller
    {
        private readonly IConverter _converter;
       
        public PdfCreatorController(IConverter converter)
        {
            _converter = converter;

        }



        [HttpGet]
        public IActionResult CreatePDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                //Print pdf in local stroge
                //Out = @"C:\Users\User\Desktop\Pdfconvertor\Employee_Report.pdf"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            //print pdf local stroge
            //_converter.Convert(pdf);
            //return Ok("Successfully created PDF document.");

            //print to Browser
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf");
            //print to Browser




        }
    }
}
