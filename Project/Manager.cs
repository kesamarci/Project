using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Manager
    {
        public Manager(string name, string managerId, int birthYear, DateTime startOfEmployment, bool hasMBA)
        {
            Name = name;
            ManagerId = managerId;
            BirthYear = birthYear;
            StartOfEmployment = startOfEmployment;
            HasMBA = hasMBA;
        }

        public string Name { get; set; }
        public string ManagerId { get; set; }
        public int BirthYear { get; set; }
        public DateTime StartOfEmployment { get; set; }
        public bool HasMBA { get; set; }
    }
}
