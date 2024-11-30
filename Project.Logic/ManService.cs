using Project.Data;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Logic
{
    public interface IManagerService
    {
        Manager GetManagerById(string id);
        IEnumerable<Manager> GetAllManagers();
        void AddManager(Manager manager);
        void UpdateManager(Manager manager);
        void DeleteManager(string managerid);
    }
    public class ManService : IManagerService
    {
        private readonly IManagerDataProvider _managerService;
        public ManService(IManagerDataProvider managerService)
        {
            _managerService = managerService;
        }
        public Manager GetManagerById(string id)
        {
            return _managerService.GetManagerById(id);
        }
        public IEnumerable<Manager> GetAllManagers()
        {
            return _managerService.GetAllManagers();
        }
        public void AddManager(Manager manager)
        {
            _managerService.AddManager(manager);
        }
        public void UpdateManager(Manager manager)
        {
            _managerService.UpdateManager(manager);
        }
        public void DeleteManager(string managerid)
        {
            _managerService.DeleteManager(managerid);
        }
    }
    public class ManagerMenu
    {
        private readonly IManagerService _managerService;

        public ManagerMenu(IManagerService managerService)
        {
            _managerService = managerService;
        }

        public void Show()
        {
            Console.WriteLine("Válassz egy műveletet a Menedzserekhez:");
            Console.WriteLine("1. Új menedzser hozzáadása");
            Console.WriteLine("2. Menedzserek listázása");
            Console.WriteLine("3. Menedzser módosítása");
            Console.WriteLine("4. Menedzser törlése");
            Console.WriteLine("5. Vissza");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddManager();
                    break;
                case "2":
                    ListManagers();
                    break;
                case "3":
                    UpdateManager();
                    break;
                case "4":
                    DeleteManager();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Érvénytelen választás.");
                    break;
            }
        }

        private void AddManager()
        {
            Console.WriteLine("Adja meg a menedzser ID-t:");
            var managerid = Console.ReadLine();

            Console.WriteLine("Adja meg a menedzser nevét:");
            var name = Console.ReadLine();
            Console.WriteLine("Adja meg a menedzser születési évét:");
            var birth =int.Parse( Console.ReadLine());

            Console.WriteLine("Adja meg a menedzser kezdését:");
            var start = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Adja meg a menedzsernek van e MBA-e:");
            bool mba =bool.Parse( Console.ReadLine());

            var manager = new Manager
            {
                ManagerId = managerid,
                Name = name,
                BirthYear = birth,
                StartOfEmployment = start,
                HasMBA = mba
            };

            _managerService.AddManager(manager);
            Console.WriteLine("Menedzser hozzáadva.");
            Console.ReadKey();
        }

        private void ListManagers()
        {
            var managers = _managerService.GetAllManagers();
            foreach (var manager in managers)
            {
                Console.WriteLine($"ID: {manager.ManagerId}, Név: {manager.Name}, Születési év:{manager.BirthYear} , Kezdési év: {manager.StartOfEmployment.Year} , MBA: {manager.HasMBA}");
            }
            Console.ReadKey();

        }

        private void UpdateManager()
        {
            Console.WriteLine("Adja meg a menedzser ID-ját:");
            var id = Console.ReadLine();

            var manager = _managerService.GetManagerById(id);
            if (manager == null)
            {
                Console.WriteLine("Menedzser nem található.");
                return;
            }

            Console.WriteLine("Adja meg az új nevet:");
            manager.Name = Console.ReadLine();
            Console.WriteLine("Adja meg a menedzser születési évét:");
            manager.BirthYear = int.Parse(Console.ReadLine());

            Console.WriteLine("Adja meg az új kezdési évet:");
            manager.StartOfEmployment = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Adja meg a menedzsernek van e MBA-e:");
            manager.HasMBA = bool.Parse(Console.ReadLine());

            _managerService.UpdateManager(manager);
            Console.WriteLine("Menedzser módosítva.");
            Console.ReadKey();
        }

        private void DeleteManager()
        {
            Console.WriteLine("Adja meg a menedzser ID-ját a törléshez:");
            var id = Console.ReadLine();
            _managerService.DeleteManager(id);
            Console.WriteLine("Menedzser törölve.");
            Console.ReadKey();
        }
    }
}
