using Microsoft.AspNetCore.Mvc;
using MVC5_Task.Dto_s;
using MVC5_Task.Interfaces;
using MVC5_Task.Services;
using System.Collections.Generic;
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
        //[HttpGet]
        //public async Task<ActionResult<IReadOnlySet<EmployeeDto>>> GetAll() {

        //    return View(await _employeeService.GetSingleEmployeeWithTotalProjectHours());
        //}
    }
}
