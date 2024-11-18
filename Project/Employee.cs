using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project.Program;

namespace Project
{
    public class Employee
    {
        public Employee(string employeeId, string name, int birthYear, int startYear, int completedProjects, bool active, bool retired, string email, string phone, string job, string level, int salary, decimal commission, List<Department> departments)
        {
            EmployeeId = employeeId;
            Name = name;
            BirthYear = birthYear;
            StartYear = startYear;
            CompletedProjects = completedProjects;
            Active = active;
            Retired = retired;
            Email = email;
            Phone = phone;
            Job = job;
            Level = level;
            Salary = salary;
            Commission = commission;
            Departments = departments;
        }

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
