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
        void DeleteManager(string id);
    }
    public class ManService : IManagerService
    {
        private readonly IManagerService _managerService;
        public ManService(IManagerService managerService)
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
        public void DeleteManager(string id)
        {
            _managerService.DeleteManager(id);
        }
    }
}
