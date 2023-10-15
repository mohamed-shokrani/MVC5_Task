﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC5_Task.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
        public ICollection<Department> Departments  { get; set; }  = new HashSet<Department>();

    }
}
