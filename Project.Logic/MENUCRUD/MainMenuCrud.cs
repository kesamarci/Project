using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Logic.MENUCRUD
{


    public class MainMenuCrud
    {
        private readonly IEmployeeService _employeeService;
        private readonly IManagerService _managerService;
        private readonly IDepartmentService _departmentService;

        public MainMenuCrud(IEmployeeService employeeService, IManagerService managerService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _managerService = managerService;
            _departmentService = departmentService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Válasszon egy műveletet:");
                Console.WriteLine("1. Alkalmazottak kezelése");
                Console.WriteLine("2. Menedzserek kezelése");
                Console.WriteLine("3. Részlegek kezelése");
                Console.WriteLine("4. Kilépés");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var employeeMenu = new EmployeeMenu(_employeeService, _departmentService);
                        employeeMenu.Show();
                        break;
                    case "2":
                        var managerMenu = new ManagerMenu(_managerService);
                        managerMenu.Show();
                        break;
                    case "3":
                        var departmentMenu = new DepartmentMenu(_departmentService);
                        departmentMenu.Show();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás.");
                        break;
                }
            }
        }
    }

}

