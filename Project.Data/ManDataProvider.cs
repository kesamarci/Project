using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public interface IManagerDataProvider
    {
        IEnumerable<Manager> GetAllManagers();
        Manager GetManagerById(string managerId);
        void AddManager(Manager manager);
        void UpdateManager(Manager manager);
        void DeleteManager(string managerid);
    }
    public class ManagerDataProvider : IManagerDataProvider
    {
        private readonly DataDbContext _context;

        public ManagerDataProvider(DataDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Manager> GetAllManagers()
        {
            return _context.Managers.ToList();
        }

        public Manager GetManagerById(string managerId)
        {
            return _context.Managers
                           .FirstOrDefault(m => m.ManagerId == managerId)!;
        }

        public void AddManager(Manager manager)
        {
            var existingManager = _context.Managers.Find(manager.ManagerId);
            if (existingManager == null)
            {
                _context.Managers.Add(manager);
            }
            else
            {
                _context.Entry(existingManager).CurrentValues.SetValues(manager);
            }
            _context.SaveChanges();
        }

        public void UpdateManager(Manager manager)
        {
            _context.Managers.Update(manager);
            _context.SaveChanges();
        }

        public void DeleteManager(string managerid)
        {
            var manager = _context.Managers.Find(managerid);
            if (manager != null)
            {
                _context.Managers.Remove(manager);
                _context.SaveChanges();
            }
        }
    }
}
