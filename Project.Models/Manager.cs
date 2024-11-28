using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Manager
    {

        
        public string Name { get; set; }
        [Key]
        public string ManagerId { get; set; }
        public int BirthYear { get; set; }
        public DateTime StartOfEmployment { get; set; }
        public bool HasMBA { get; set; }
    }
}
