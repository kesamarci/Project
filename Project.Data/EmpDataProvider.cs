using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public interface IEmployeeDataProvider
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(string employeeId);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(string employeeid);
    }
    public class EmployeeDataProvider : IEmployeeDataProvider
    {
        private readonly DataDbContext _context;

        public EmployeeDataProvider(DataDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.Include(e => e.Departments).ToList();
        }

        public Employee GetEmployeeById(string employeeId)
        {
            return _context.Employees.Include(e => e.Departments)
                                     .FirstOrDefault(e => e.Id == employeeId)!;
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(string employeeid)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeid);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}
