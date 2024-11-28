using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Department
    {

        
        public string Name { get; set; }
        [Key]
        public string DepartmentCode { get; set; }
        public string HeadOfDepartment { get; set; }
    }

}
