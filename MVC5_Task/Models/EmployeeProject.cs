using System.ComponentModel.DataAnnotations.Schema;

namespace MVC5_Task.Models
{
    public class EmployeeProject
    {
        [ForeignKey("EmployeeId")]
        public int EmployeeId {  get; set; }
        public Employee Employee { get; set; }  
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public decimal Hours { get; set; }

    }
}
