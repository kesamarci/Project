using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Department
    {
        public Department(string name, string departmentCode, string headOfDepartment)
        {
            Name = name;
            DepartmentCode = departmentCode;
            HeadOfDepartment = headOfDepartment;
        }

        public string Name { get; set; }
        public string DepartmentCode { get; set; }
        public string HeadOfDepartment { get; set; }
    }

}
