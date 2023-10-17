using System.ComponentModel;

namespace MVC5_Task.Dto_s
{
    public record EmployeeProjectDto
    {
        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }
        [DisplayName("Department")]
        public string DepartmentName { get; set; }

        [DisplayName("Project Name")]

        public string ProjectName { get; set; }
        [DisplayName("Total Hours")]

        public decimal TotalHours { get; set; }
    }
}
