using Microsoft.AspNetCore.Mvc;
using MVC5_Task.Dto_s;
using MVC5_Task.Interfaces;
using MVC5_Task.Models;
using MVC5_Task.Services;
using PdfSharpCore.Drawing;
using Stimulsoft.Report;
using System;
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

        public async Task<ActionResult<IReadOnlySet<EmployeeProjectDto>>> Index()
        {
            return View(await _employeeService.GetSingleEmployeeWithTotalProjectHours());
        }
        public async Task<IActionResult> GenerateReport()
        {
            byte[] reportData=await _employeeService.GenerateEmployeeReportService();
            return File(reportData, "application/pdf", "EmployeeReport.pdf");
         
        }
      

    }
}
