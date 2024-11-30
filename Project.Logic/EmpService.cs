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
        void DeleteEmployee(string id);
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
        public void DeleteEmployee(string id)
        {
            _employeeService.DeleteEmployee(id);
        }
    }
    public class EmployeeMenu
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeMenu(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void Show()
        {
            Console.WriteLine("Válassz egy műveletet az Alkalmazottakhoz:");
            Console.WriteLine("1. Új alkalmazott hozzáadása");
            Console.WriteLine("2. Alkalmazottak listázása");
            Console.WriteLine("3. Alkalmazott módosítása");
            Console.WriteLine("4. Alkalmazott törlése");
            Console.WriteLine("5. Vissza");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEmployee();
                    break;
                case "2":
                    ListEmployees();
                    break;
                case "3":
                    UpdateEmployee();
                    break;
                case "4":
                    DeleteEmployee();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Érvénytelen választás.");
                    break;
            }
        }

       
        private void AddEmployee()
        {
            Console.WriteLine("Adja meg az alkalmazott ID-át");
            var id = Console.ReadLine();
            Console.WriteLine("Adja meg az alkalmazott nevét:");
            var name = Console.ReadLine();

            Console.WriteLine("Adja meg a születési évet:");
            var birthYear = int.Parse(Console.ReadLine());

            Console.WriteLine("Adja meg a kezdési évet:");
            var startYear = int.Parse(Console.ReadLine());

            Console.WriteLine("Adja meg az alkalmazott kész projectjeit:");
            var project = int.Parse(Console.ReadLine()); 

            Console.WriteLine("Adja meg hogy az alkalmazott aktiv-e? (true/false)");
            bool active = bool.Parse(Console.ReadLine());

            Console.WriteLine("Adja meg hogy az alkalmazott Visszavonult-e?");
            bool retired = bool.Parse(Console.ReadLine());

            Console.WriteLine("Adja meg az alkalmazott Emailcimét");
            var email = Console.ReadLine();

            Console.WriteLine("Adja meg  az alkalmazott Telefonszámát");
            var phonenumber = Console.ReadLine();

            Console.WriteLine("Adja meg  az alkalmazott a Feladatát:");
            var job = Console.ReadLine();

            Console.WriteLine("Adja meg  az alkalmazott Szintjét:");
            var level = Console.ReadLine();

            Console.WriteLine("Adja meg  az alkalmazott Fizetését:");
            var salary =int.Parse( Console.ReadLine());

            Console.WriteLine("Adja meg  az alkalmazott Commissionjét:");
            var commission =int.Parse( Console.ReadLine());

            var employee = new Employee
            {
                Id = id,
                Name = name,
                BirthYear = birthYear,
                StartYear = startYear,
                CompletedProjects = project, 
                Active = active,
                Retired = retired,
                Email = email,
                Phone = phonenumber,
                Job = job,
                Level = level,
                Salary = salary,
                Commission = commission
            };

            _employeeService.AddEmployee(employee);
            Console.WriteLine("Alkalmazott hozzáadva.");
            Console.ReadKey();
        }

        private void ListEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.Id}, Név: {employee.Name}, Születési év: {employee.BirthYear}, Kezdési év: {employee.StartYear}");
            }
            Console.ReadKey();
        }

        private void UpdateEmployee()
        {
            Console.WriteLine("Adja meg az alkalmazott ID-ját:");
            var id = Console.ReadLine();

            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                Console.WriteLine("Alkalmazott nem található.");
                return;
            }

            Console.WriteLine("Adja meg az új nevet:");
            employee.Name = Console.ReadLine();

            Console.WriteLine("Adja meg az új születési évet:");
            employee.BirthYear = int.Parse(Console.ReadLine());

            Console.WriteLine("Adja meg az új kezdési évet:");
            employee.StartYear = int.Parse(Console.ReadLine());

            _employeeService.UpdateEmployee(employee);
            Console.WriteLine("Alkalmazott módosítva.");
            Console.ReadKey();
        }

        private void DeleteEmployee()
        {
            Console.WriteLine("Adja meg az alkalmazott ID-ját a törléshez:");
            var id = Console.ReadLine();
            _employeeService.DeleteEmployee(id);
            Console.WriteLine("Alkalmazott törölve.");
            Console.ReadKey();
        }
    }
}
