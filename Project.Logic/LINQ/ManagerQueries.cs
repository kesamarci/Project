﻿using Project.Data;
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
            Console.WriteLine("4. Vissza");

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
            // Hasonlóan az alkalmazottakra vonatkozó lekérdezéseket is hozzáadhatjuk.
        }

        private void ShowMixedQueries()
        {
            // A vegyes lekérdezéseket is külön menüpontként kezeljük.
        }
        
    }
}