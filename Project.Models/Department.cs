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

        [Key]
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
        public string HeadOfDepartment { get; set; }
    }

}
