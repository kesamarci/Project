using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Employee
    {

        [Key]
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public int StartYear { get; set; }
        public int CompletedProjects { get; set; }
        public bool Active { get; set; }
        public bool Retired { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Job { get; set; }
        public string Level { get; set; }
        public int Salary { get; set; }
        public decimal Commission { get; set; }
        public List<Department> Departments { get; set; }
    }
}
