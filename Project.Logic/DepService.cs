using Project.Data;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Logic
{
    public interface IDepartmentService 
    {
        Department GetDepartmentByCode(string code);
        IEnumerable<Department> GetAllDepartments();
        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department code);
    }
    public class DepService : IDepartmentService
    {
        private readonly IDepartmentDataProvider _departmentService;
        public DepService(IDepartmentDataProvider departmentService)
        {
            _departmentService = departmentService;
        }
        public Department GetDepartmentByCode(string code)
        {
            return _departmentService.GetDepartmentByCode(code);
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            return _departmentService.GetAllDepartments();
        }
        public void AddDepartment(Department department)
        {
            _departmentService.AddDepartment(department);
        }
        public void UpdateDepartment(Department department)
        {
            _departmentService.UpdateDepartment(department);
        }
        public void DeleteDepartment(Department code)
        {
            _departmentService.DeleteDepartment(code);
        }
    }
}
