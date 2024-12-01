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
        void DeleteDepartment(string code);
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
        public void DeleteDepartment(string code)
        {
            _departmentService.DeleteDepartment(code);
        }
    }
    public class DepartmentMenu
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentMenu(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public void Show()
        {
            Console.WriteLine("Válassz egy műveletet a Részlegekhez:");
            Console.WriteLine("1. Új részleg hozzáadása");
            Console.WriteLine("2. Részlegek listázása");
            Console.WriteLine("3. Részleg módosítása");
            Console.WriteLine("4. Részleg törlése");
            Console.WriteLine("5. Vissza");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddDepartment();
                    break;
                case "2":
                    ListDepartments();
                    break;
                case "3":
                    UpdateDepartment();
                    break;
                case "4":
                    DeleteDepartment();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Érvénytelen választás.");
                    break;
            }
        }

        private void AddDepartment()
        {

            Console.WriteLine("Adja meg a részleg nevét:");
            var name = Console.ReadLine();
           

            Console.WriteLine("Adja meg a részleg kódját:");
            var code = Console.ReadLine();

            Console.WriteLine("Adja meg a részleg vezetőjét:");
            var head = Console.ReadLine();

            var department = new Department
            {
                Name = name,
                DepartmentCode = code,
                HeadOfDepartment = head
            };

            _departmentService.AddDepartment(department);
            Console.WriteLine("Részleg hozzáadva.");
            Console.ReadKey();
        }

        private void ListDepartments()
        {
            var departments = _departmentService.GetAllDepartments();
            foreach (var department in departments)
            {
                Console.WriteLine($"Név: {department.Name}, Kód: {department.DepartmentCode}, Vezető: {department.HeadOfDepartment}");
            }
            Console.ReadKey();

        }

        private void UpdateDepartment()
        {
            Console.WriteLine("Adja meg a részleg kódját:");
            var code = Console.ReadLine();

            var department = _departmentService.GetDepartmentByCode(code);
            if (department == null)
            {
                Console.WriteLine("Részleg nem található.");
                return;
            }

            Console.WriteLine("Adja meg az új nevet:");
            department.Name = Console.ReadLine();

            Console.WriteLine("Adja meg az új vezetőt:");
            department.HeadOfDepartment = Console.ReadLine();

            _departmentService.UpdateDepartment(department);
            Console.WriteLine("Részleg módosítva.");
            Console.ReadKey();

        }

        private void DeleteDepartment()
        {
            Console.WriteLine("Adja meg a részleg kódját a törléshez:");
            var code = Console.ReadLine();
            _departmentService.DeleteDepartment(code);
            Console.WriteLine("Részleg törölve.");
            Console.ReadKey();

        }
    }
}
