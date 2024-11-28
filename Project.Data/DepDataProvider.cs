using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public interface IDepartmentDataProvider
    {
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartmentByCode(string departmentCode);
        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(string departmentCode);
    }
    public class DepartmentDataProvider : IDepartmentDataProvider
    {
        private readonly DataDbContext _context;
        public DepartmentDataProvider(DataDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }
        public Department GetDepartmentByCode(string departmentCode)
        {
            return _context.Departments.FirstOrDefault(d => d.DepartmentCode == departmentCode)!;
        }
        public void AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }
        public void UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
        }
        public void DeleteDepartment(string departmentCode)
        {
            var department = _context.Departments.Find(departmentCode);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
            }
        }
    }
   

}
