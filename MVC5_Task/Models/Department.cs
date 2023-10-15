using System.ComponentModel.DataAnnotations;

namespace MVC5_Task.Models
{
    public class Department
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public Employee Employee { get; set; }  

    }
}
