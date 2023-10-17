using MVC5_Task.Data;
using MVC5_Task.Interfaces;
using MVC5_Task.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MVC5_Task.Dto_s;

namespace MVC5_Task.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<EmployeeDto>> GetSingleEmployeeWithTotalProjectHours()
        {
            var result = await (from emp in _dbContext.Employees
                                join  dep  in _dbContext.Departments
                                on emp.Id equals dep.Employee.Id
                                join proEmp in _dbContext.EmployeeProjects
                                on emp.Id equals proEmp.EmployeeId
                                join pro in _dbContext.Projects
                                on proEmp.ProjectId equals pro.Id
                                select new EmployeeDto 
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
    }
}
