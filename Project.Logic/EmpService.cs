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
        private readonly IDepartmentService _departmentService;

        public EmployeeMenu(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }


        public void Show()
        {
            Console.WriteLine("Válassz egy műveletet az Alkalmazottakhoz:");
            Console.WriteLine("1. Új alkalmazott hozzáadása");
            Console.WriteLine("2. Alkalmazott listázása");
            Console.WriteLine("3. Alkalmazottak listázása");
            Console.WriteLine("4. Alkalmazott módosítása");
            Console.WriteLine("5. Alkalmazott törlése");
            Console.WriteLine("6. Vissza");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEmployee();
                    break;
                case "2":
                    ListEmployee();
                    break;
                case "3":
                    ListEmployees(); 
                    break;
                case "4":
                    UpdateEmployee();
                    break;
                case "5":
                    DeleteEmployee();
                    break;
                case "6":
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
            Console.WriteLine("Adja meg  az alkalmazott department számát:");
            var department = int.Parse(Console.ReadLine());
            string[] departments = new string[department];
            for (int i = 0; i < department; i++)
            {
                Console.WriteLine("Adja meg a department kódját:");
                departments[i] = Console.ReadLine();
            }
            ICollection<Department> departmentList = new List<Department>();
            foreach (var dep in departments)
            {
                departmentList.Add(_departmentService.GetDepartmentByCode(dep));
            }


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
                Commission = commission,
                Departments = departmentList



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
                Console.WriteLine($"ID: {employee.Id}, Név: {employee.Name}, Születési év: {employee.BirthYear}, Kezdési év: {employee.StartYear}, " +
                    $"Kész projectjeit:{employee.CompletedProjects}, aktiv-e?\n {employee.Active}, Visszavonult-e? {employee.Retired}, Emailcimét: {employee.Email}, " +
                    $"Telefonszámát: {employee.Phone},\n Feladata: {employee.Job}, Szintje: {employee.Level}, Fizetése: {employee.Salary}, Commissionje: {employee.Commission} \n");
            }
            Console.ReadKey();
        }
        private void ListEmployee()
        {
            Console.WriteLine("Adja meg az alkalmazott ID-ját:");
            var id = Console.ReadLine();
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                Console.WriteLine("Alkalmazott nem található.");
                return;
            }
            Console.WriteLine($"ID: {employee.Id}, Név: {employee.Name}, Születési év: {employee.BirthYear}, Kezdési év: {employee.StartYear}, " +
                    $"Kész projectjeit:{employee.CompletedProjects}, aktiv-e?\n {employee.Active}, Visszavonult-e? {employee.Retired}, Emailcimét: {employee.Email}, " +
                    $"Telefonszámát: {employee.Phone},\n Feladata: {employee.Job}, Szintje: {employee.Level}, Fizetése: {employee.Salary}, Commissionje: {employee.Commission} \n");
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
            Console.WriteLine("Adja meg az alkalmazott új kész projectjeit:");
            employee.CompletedProjects = int.Parse(Console.ReadLine());
            Console.WriteLine("Adja meg hogy az új alkalmazott aktiv-e? (true/false)");
            employee.Active = bool.Parse(Console.ReadLine());
            Console.WriteLine("Adja meg hogy az új alkalmazott Visszavonult-e?");
            employee.Retired = bool.Parse(Console.ReadLine());
            Console.WriteLine("Adja meg az új alkalmazott Emailcimét");
            employee.Email = Console.ReadLine();
            Console.WriteLine("Adja meg  az új alkalmazott Telefonszámát");
            employee.Phone = Console.ReadLine();
            Console.WriteLine("Adja meg  az új alkalmazott a Feladatát:");
            employee.Job = Console.ReadLine();
            Console.WriteLine("Adja meg  az új alkalmazott Szintjét:");
            employee.Level = Console.ReadLine();
            Console.WriteLine("Adja meg  az új alkalmazott Fizetését:");
            employee.Salary = int.Parse(Console.ReadLine());
            Console.WriteLine("Adja meg  az új alkalmazott Commissionjét:");
            employee.Commission = int.Parse(Console.ReadLine());
            Console.WriteLine("Adja meg  az új alkalmazott department számát:");
            var department = int.Parse(Console.ReadLine());
            string[] departments = new string[department];
            for (int i = 0; i < department; i++)
            {
                Console.WriteLine("Adja meg a department kódját:");
                departments[i] = Console.ReadLine();
            }
            ICollection<Department> departmentList = new List<Department>();
            foreach (var dep in departments)
            {
                departmentList.Add(_departmentService.GetDepartmentByCode(dep));
            }
            employee.Departments = departmentList;


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
