using MVC5_Task.Dto_s;
using MVC5_Task.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC5_Task.Interfaces
{
    public interface IEmployeeService
    {
        Task<IReadOnlyList<EmployeeProjectDto>> GetSingleEmployeeWithTotalProjectHours();
        Task<byte[]> GenerateEmployeeReportService();
    }
}
