using Microsoft.AspNetCore.Mvc;
using MVC5_Task.Dto_s;
using MVC5_Task.Interfaces;
using PdfSharpCore.Drawing;
using Stimulsoft.Report;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MVC5_Task.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<ActionResult<IReadOnlySet<EmployeeDto>>> Index()
        {
            return View(await _employeeService.GetSingleEmployeeWithTotalProjectHours());
        }
        public async Task<IActionResult> GenerateReport()
        {
            var employees = await _employeeService.GetSingleEmployeeWithTotalProjectHours();

            using (MemoryStream stream = new MemoryStream())
            {
                PdfSharpCore.Pdf.PdfDocument pdf = new PdfSharpCore.Pdf.PdfDocument();
                PdfSharpCore.Pdf.PdfPage page = pdf.AddPage();

                XGraphics graphics = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12, XFontStyle.Bold);

                int y = 50;

                foreach (var employee in employees)
                {
                    graphics.DrawString(employee.EmployeeName, font, XBrushes.Black, new XPoint(50, y));
                    graphics.DrawString(employee.ProjectName, font, XBrushes.Black, new XPoint(200, y));
                    graphics.DrawString(employee.TotalHours.ToString(), font, XBrushes.Black, new XPoint(350, y));
                    graphics.DrawString(employee.DepartmentName, font, XBrushes.Black, new XPoint(500, y));

                    y += 20;
                }

                pdf.Save(stream);
                stream.Position = 0;

                return File(stream.ToArray(), "application/pdf", "EmployeeReport.pdf");
            }
        }

    }
}
