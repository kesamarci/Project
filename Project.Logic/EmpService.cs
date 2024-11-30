using Project.Data;
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
        void DeleteEmployee(Employee id);
    }
    public class EmpService : IEmployeeService
    {
        private readonly IEmployeeDataProvider _employeeService;
        public EmpService(IEmployeeDataProvider employeeService)
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
        public void DeleteEmployee(Employee id)
        {
            _employeeService.DeleteEmployee(id);
        }
    }
}
