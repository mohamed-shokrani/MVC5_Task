using MVC5_Task.Data;
using MVC5_Task.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MVC5_Task.Dto_s;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using System.IO;
using System;

namespace MVC5_Task.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<EmployeeProjectDto>> GetSingleEmployeeWithTotalProjectHours()
        {
            var result = await (from emp in _dbContext.Employees
                                join  dep  in _dbContext.Departments
                                on emp.Id equals dep.Employee.Id
                                join proEmp in _dbContext.EmployeeProjects
                                on emp.Id equals proEmp.EmployeeId
                                join pro in _dbContext.Projects
                                on proEmp.ProjectId equals pro.Id
                                select new EmployeeProjectDto
                                { EmployeeName = emp.Name,
                                  ProjectName = pro.Name,
                                  TotalHours =  proEmp.Hours,
                                  DepartmentName =dep.Name
                                }).OrderByDescending(x=>x.TotalHours)
                                  .ThenBy(x => x.EmployeeName)
                                  .AsNoTracking()
                                  .ToListAsync();

            return result;
        }
        public async Task<byte[]> GenerateEmployeeReportService()
        {
            var employees = await GetSingleEmployeeWithTotalProjectHours();

            return Report(employees);
        }
        private byte[] Report(IReadOnlyList<EmployeeProjectDto> employees)
        {
            using MemoryStream stream = new();

            PdfSharpCore.Pdf.PdfDocument pdf = new PdfSharpCore.Pdf.PdfDocument();
            PdfSharpCore.Pdf.PdfPage page = pdf.AddPage();

            XGraphics graphics = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 12, XFontStyle.Bold);

            int y = 20;
            string title = "Employee Report";
            XSize titleSize = graphics.MeasureString(title, font);
            double titleX = (page.Width - titleSize.Width) / 2;
            double titleY = y;
            graphics.DrawString(title, font, XBrushes.Black, new XPoint(titleX, titleY));
            y = 40;
            graphics.DrawString("Employee Name", font, XBrushes.Black, new XPoint(50, y));
            graphics.DrawString("Department", font, XBrushes.Black, new XPoint(200, y));
            graphics.DrawString("Project Name", font, XBrushes.Black, new XPoint(500, y));
            graphics.DrawString("Total Hours", font, XBrushes.Black, new XPoint(350, y));
            y += 40; ;
            foreach (var employee in employees)
            {
                graphics.DrawString(employee.EmployeeName, font, XBrushes.Gray, new XPoint(50, y));
                graphics.DrawString(employee.DepartmentName, font, XBrushes.Gray, new XPoint(200, y));
                graphics.DrawString(employee.TotalHours.ToString(), font, XBrushes.Gray, new XPoint(350, y));
                graphics.DrawString(employee.ProjectName, font, XBrushes.Gray, new XPoint(500, y));

                y += employees.Count;
            }
            // Add page number
            var pageNumberFont = new XFont("Arial", 10, XFontStyle.Regular);
            var pageNumberText = string.Format("Page {0} of {1}", 1, 1);
            graphics.DrawString(pageNumberText, pageNumberFont, XBrushes.Black, new XPoint(50, page.Height - 50));

            // Add current date and time
            var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dateTimeFont = new XFont("Arial", 10, XFontStyle.Regular);
            graphics.DrawString(dateTimeNow, dateTimeFont, XBrushes.Black, new XPoint(page.Width - 200, page.Height - 50));
            pdf.Save(stream);
            stream.Position = 0;

            return stream.ToArray();
        }
    }
}
