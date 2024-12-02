using Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Logic.LINQ
{
    public class QueryMenu
    {
        private readonly IManagerService _managerService;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public QueryMenu(IManagerService managerService, IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _managerService = managerService;
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public void Show()
        {
            Console.WriteLine("Lekérdezések menü:");
            Console.WriteLine("1. Vezetőkkel kapcsolatos lekérdezések");
            Console.WriteLine("2. Alkalmazottakkal kapcsolatos lekérdezések");
            Console.WriteLine("3. Vegyes lekérdezések");
            Console.WriteLine("4. Vissza a főmenübe");

            Console.Write("Válassz egy opciót: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowManagerQueries();
                    break;
                case "2":
                    ShowEmployeeQueries();
                    break;
                case "3":
                    ShowMixedQueries();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Érvénytelen választás.");
                    Show();
                    break;
            }
        }

        private void ShowManagerQueries()
        {
            Console.WriteLine("Vezetőkkel kapcsolatos lekérdezések:");
            Console.WriteLine("1. Hány doktori címmel rendelkező vezető van?");
            Console.WriteLine("2. Doktori cím, de MBA nélkül: Kik?");
            Console.WriteLine("3. Legrégebb óta munkában lévő vezető");
            Console.WriteLine("4. Legtöbbet dolgozó vezető az életkorához képest");
            Console.WriteLine("5. MBA végzettséggel rendelkező vezetők aránya");
            Console.WriteLine("6. Vissza");

            Console.Write("Válassz egy opciót: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    QueryManagersWithDoctorates();
                    break;
                case "2":
                    QueryManagersWithDoctorateNoMBA();
                    break;
                case "3":
                    QueryLongestWorkingManager();
                    break;
                case "4":
                    QueryManagerWithLongestWorkLifeRatio();
                    break;
                case "5":
                    QueryMBAProportion();
                    break;
                case "6":
                    Show();
                    break;
                default:
                    Console.WriteLine("Érvénytelen választás.");
                    ShowManagerQueries();
                    break;
            }
        }
        private void ShowEmployeeQueries()
        {
            Console.WriteLine("Alkalmazottakal kapcsolatos lekérdezések:");
            Console.WriteLine("1. Hány alkalmazott van, akik a 80-as években születtek?");
            Console.WriteLine("2. Hány alkalmazott van, akik legalább két részlegen dolgoznak?");
            Console.WriteLine("3. Jelenleg nyugdíjban lévő, de még dolgozó alkalmazottak");
            Console.WriteLine("4. Hányan nyugdíjba mentek, és nem dolgoznak?");
            Console.WriteLine("5. Nyugdíjasok átlagos keresete");
            Console.WriteLine("6. Kereset alapján csökkenő sorrend");
            Console.WriteLine("7. Tudásszint szerinti összetétel (junior, medior, senior)");
            Console.WriteLine("8. Részlegek doktori címmel rendelkező vezetői alá tartozó alkalmazottak");
            Console.WriteLine("9. Átlagfizetés fölött/alatt kereső alkalmazottak száma");
            Console.WriteLine("10. Átlagfizetés az egyes szinteken");
            Console.WriteLine("11. Medior átlagfizetés vs. legmagasabb junior fizetés");
            Console.WriteLine("12. Kategória (junior, medior, senior) jutalék összesítése");
            Console.WriteLine("13. Legkevesebb projekten dolgozó alkalmazott");
            Console.WriteLine("14. Születési sorrend szerinti kereset");
            Console.WriteLine("15. Legkevesebb projekten dolgozó aktív alkalmazott");
            Console.WriteLine("16. Jutalék nagyobb, mint más alapfizetése");
            Console.WriteLine("17. Vissza a főmenübe");

            Console.Write("Válassz egy opciót: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    QueryEmployeesBornIn80s();
                    break;
                case "2":
                    QueryEmployeesInMultipleDepartments();
                    break;
                case "3":
                    QueryRetiredButWorkingEmployees();
                    break;
                case "4":
                    QueryFullyRetiredEmployees();
                    break;
                case "5":
                    QueryAverageSalaryOfRetirees();
                    break;
                case "6":
                    QueryEmployeesByTotalEarningsDescending();
                    break;
                case "7":
                    QueryEmployeeSkillComposition();
                    break;
                case "8":
                    QueryEmployeesInDoctorateLedDepartments();
                    break;
                case "9":
                    QueryAboveBelowAverageSalaryEmployees();
                    break;
                case "10":
                    QueryAverageSalaryByLevel();
                    break;
                case "11":
                    QueryMediorAverageVsJuniorHighestSalary();
                    break;
                case "12":
                    QueryCommissionByCategory();
                    break;
                case "13":
                    QueryFewestProjectsEmployee();
                    break;
                case "14":
                    QuerySalaryByBirthOrder();
                    break;
                case "15":
                    QueryFewestProjectsActiveEmployee();
                    break;
                case "16":
                    QueryCommissionHigherThanBaseSalary();
                    break;
                case "17":
                    Show();
                    break;
                default:
                    Console.WriteLine("Érvénytelen választás.");
                    ShowEmployeeQueries();
                    break;
            }
        }

        private void ShowMixedQueries()
        {
            // A vegyes lekérdezéseket is külön menüpontként kezeljük.
        }
        private void QueryManagersWithDoctorates()
        {
            var count = _managerService.GetAllManagers().Count(m => m.Name.StartsWith("Dr"));
            Console.WriteLine($"Doktori címmel rendelkező vezetők száma: {count}");
            Console.ReadKey();
        }
        private void QueryManagersWithDoctorateNoMBA()
        {
            var managers = _managerService.GetAllManagers()
                .Where(m => !m.HasMBA && m.Name.StartsWith("Dr"))
                .Select(m => m.Name)
                .ToList();

            if (managers.Any())
            {
                Console.WriteLine("MBA végzettség nélküli doktori cím tulajdonosai:");
                managers.ForEach(m => Console.WriteLine($"- {m}"));
            }
            else
            {
                Console.WriteLine("Nincs ilyen vezető.");
            }
            Console.ReadKey();
        }
        private void QueryLongestWorkingManager()
        {
            var manager = _managerService.GetAllManagers()
                .OrderBy(m => m.StartOfEmployment)
                .FirstOrDefault();

            if (manager != null)
            {
                Console.WriteLine($"Legrégebb óta munkában lévő vezető: {manager.Name} ({manager.StartOfEmployment:yyyy-MM-dd})");
            }
            else
            {
                Console.WriteLine("Nincsenek vezetők az adatbázisban.");
            }
            Console.ReadKey();
        }
        private void QueryManagerWithLongestWorkLifeRatio()
        {
            var manager = _managerService.GetAllManagers()
                .Select(m => new
                {
                    Manager = m,
                    WorkLifeRatio = (DateTime.Now - m.StartOfEmployment) / (DateTime.Now.Year - m.BirthYear)
                })
                .OrderByDescending(x => x.WorkLifeRatio)
                .FirstOrDefault();

            if (manager != null)
            {
                Console.WriteLine($"A legtöbbet dolgozó vezető az élt éveihez képest: {manager.Manager.Name} " +
                                  $"({manager.WorkLifeRatio:P2})");
            }
            else
            {
                Console.WriteLine("Nincsenek vezetők az adatbázisban.");
            }
        }
        private void QueryMBAProportion()
        {
            var total = _managerService.GetAllManagers().Count();
            var withMBA = _managerService.GetAllManagers().Count(m => m.HasMBA);

            if (total > 0)
            {
                var ratio = (double)withMBA / total * 100;
                Console.WriteLine($"MBA végzettséggel rendelkező vezetők aránya: {ratio:F2}%");
            }
            else
            {
                Console.WriteLine("Nincsenek vezetők az adatbázisban.");
            }
        }
        private void QueryEmployeesBornIn80s()
        {
            var count = _employeeService.GetAllEmployees()
                .Count(e => e.BirthYear >= 1980 && e.BirthYear < 1990);
            Console.WriteLine($"80-as években született alkalmazottak száma: {count}");
        }
        private void QueryEmployeesInMultipleDepartments()
        {
            var employees = _employeeService.GetAllEmployees()
                .Where(e => e.Departments.Count > 1)
                .ToList();
            Console.WriteLine($"Két vagy több részlegen dolgozó alkalmazottak száma: {employees.Count}");
        }
        private void QueryRetiredButWorkingEmployees()
        {
            var employees = _employeeService.GetAllEmployees()
                .Where(e => e.Retired && e.Active)
                .Select(e => e.Name)
                .ToList();
            if (employees.Any())
            {
                Console.WriteLine("Jelenleg nyugdíjban lévő, de dolgozó alkalmazottak:");
                employees.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("Nincs ilyen alkalmazott.");
            }
        }
        private void QueryFullyRetiredEmployees()
        {
            var count = _employeeService.GetAllEmployees()
                .Count(e => e.Retired && !e.Active);
            Console.WriteLine($"Nyugdíjba ment alkalmazottak száma: {count}");
        }
        private void QueryAverageSalaryOfRetirees()
        {
            var average = _employeeService.GetAllEmployees()
                .Where(e => e.Retired)
                .Average(e => e.Salary);
            Console.WriteLine($"Nyugdíjasok átlagkeresete: {average:C0}");
        }
        private void QueryEmployeesByTotalEarningsDescending()
        {
            var employees = _employeeService.GetAllEmployees()
                .OrderByDescending(e => e.Salary+e.Commission)
                .ToList();
            Console.WriteLine("Kereset alapján csökkenő sorrend:");
            employees.ForEach(e => Console.WriteLine($"- {e.Name}"));
        }
        private void QueryEmployeeSkillComposition()
        {
            var juniors = _employeeService.GetAllEmployees().Count(e => e.Level == "Junior");
            var mediors = _employeeService.GetAllEmployees().Count(e => e.Level == "Medior");
            var seniors = _employeeService.GetAllEmployees().Count(e => e.Level == "Senior");
            Console.WriteLine($"Junior: {juniors}, Medior: {mediors}, Senior: {seniors}");
        }
        private void QueryEmployeesInDoctorateLedDepartments()
        {
            var employees = _employeeService.GetAllEmployees()
                .Where(e => e.Departments.Any(d => d.HeadOfDepartment.StartsWith("Dr")))
                .ToList();
            Console.WriteLine($"Doktori címmel rendelkező vezetői alá tartozó alkalmazottak száma: {employees.Count}");
        }
        private void QueryAboveBelowAverageSalaryEmployees()
        {
            var average = _employeeService.GetAllEmployees().Average(e => e.Salary);
            var above = _employeeService.GetAllEmployees().Count(e => e.Salary > average);
            var below = _employeeService.GetAllEmployees().Count(e => e.Salary < average);
            Console.WriteLine($"Átlagfizetés felett: {above}, alatt: {below}");
        }
        //10
        private void QueryAverageSalaryByLevel()
        {
            var levels = _employeeService.GetAllEmployees()
                .GroupBy(e => e.Level)
                .Select(g => new
                {
                    Level = g.Key,
                    Average = g.Average(e => e.Salary)
                })
                .ToList();
            levels.ForEach(l => Console.WriteLine($"{l.Level}: {l.Average:C0}"));
        }
        //11
        private void QueryMediorAverageVsJuniorHighestSalary()
        {
            var mediorAverage = _employeeService.GetAllEmployees()
                .Where(e => e.Level == "Medior")
                .Average(e => e.Salary);
            var juniorHighest = _employeeService.GetAllEmployees()
                .Where(e => e.Level == "Junior")
                .Max(e => e.Salary);
            Console.WriteLine($"Medior átlagfizetés: {mediorAverage:C0}, legmagasabb junior fizetés: {juniorHighest:C0}");
        }
        //12
        private void QueryCommissionByCategory()
        {
            var commissions = _employeeService.GetAllEmployees()
                .GroupBy(e => e.Level)
                .Select(g => new
                {
                    Level = g.Key,
                    TotalCommission = g.Sum(e => e.Commission)
                })
                .ToList();
            commissions.ForEach(c => Console.WriteLine($"{c.Level}: {c.TotalCommission:C0}"));
        }
        //13 
        private void QueryFewestProjectsEmployee()
        {
            var employees = _employeeService.GetAllEmployees()
        .Where(e => e.StartYear > 0) // Csak azok az alkalmazottak, akik dolgoztak a cégnél
        .Select(e => new
        {
            Employee = e.Name,
            ProjectsPerYear = (double)e.CompletedProjects / (DateTime.Now.Year - e.StartYear)
        })
        .OrderBy(x => x.ProjectsPerYear)
        .FirstOrDefault();

            if (employees != null)
            {
                Console.WriteLine($"Az alkalmazott, aki az itt töltött éveihez képest a legkevesebb projekten dolgozott: {employees.Employee}, " +
                                  $"projektek/év arány: {employees.ProjectsPerYear:F2}");
            }
            else
            {
                Console.WriteLine("Nincs adat megfelelő alkalmazottról.");
            }
        }
        //14
        private void QuerySalaryByBirthOrder()
        {
            var employees = _employeeService.GetAllEmployees()
                .OrderBy(e => e.BirthYear)
                .Select(e => new
                {
                    Employee = e.Name,
                    Salaryy = e.Salary
                })
                .ToList();
            Console.WriteLine("Születési sorrend szerinti kereset:");
            employees.ForEach(e => Console.WriteLine($"- {e.Employee}: {e.Salaryy:C0}"));
        }
        //15
        private void QueryFewestProjectsActiveEmployee()
        {
            var employees = _employeeService.GetAllEmployees()
                .Where(e => e.Active)
                .OrderBy(e => e.CompletedProjects)
                .FirstOrDefault();
            if (employees != null)
            {
                Console.WriteLine($"A legkevesebb projekten dolgozó aktív alkalmazott: {employees.Name}, projektek száma: {employees.CompletedProjects}");
            }
            else
            {
                Console.WriteLine("Nincs adat megfelelő alkalmazottról.");
            }
        }

    }
}
