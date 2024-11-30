using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Logic
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(string id);
        IEnumerable<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(string id);
    }
    public class EmpService : IEmployeeService
    {
        private readonly IEmployeeService _employeeService;
        public EmpService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public Employee GetEmployeeById(string id)
        {
            return _employeeService.GetEmployeeById(id);
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeService.GetAllEmployees();
        }
        public void AddEmployee(Employee employee)
        {
            _employeeService.AddEmployee(employee);
        }
        public void UpdateEmployee(Employee employee)
        {
            _employeeService.UpdateEmployee(employee);
        }
        public void DeleteEmployee(string id)
        {
            _employeeService.DeleteEmployee(id);
        }
    }
}
