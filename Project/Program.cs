using ConsoleTools;
using System.Xml.Linq;

namespace Project
{
    internal class Program
    {
        public class Employee
        {
           
        }

        public class Department
        {
            
        }
        static void Main(string[] args)
        {
            //   var subMenu = new ConsoleMenu(args, level: 1)
            //.Add("Sub_One", () => SomeAction("Sub_One"))
            //.Add("Sub_Two", () => SomeAction("Sub_Two"))
            //.Add("Sub_Three", () => SomeAction("Sub_Three"))
            //.Add("Sub_Four", () => SomeAction("Sub_Four"))
            //.Add("Sub_Close", ConsoleMenu.Close)
            //.Configure(config =>
            //{
            //    config.Selector = "--> ";
            //    config.EnableFilter = true;
            //    config.Title = "Submenu";
            //    config.EnableBreadcrumb = true;
            //    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            //});

            //   var menu = new ConsoleMenu(args, level: 0)
            //     .Add("One", () => SomeAction("One"))
            //     .Add("Two", () => SomeAction("Two"))
            //     .Add("Three", () => SomeAction("Three"))
            //     .Add("Sub", subMenu.Show)
            //     .Add("Change me", (thisMenu) => thisMenu.CurrentItem.Name = "I am changed!")
            //     .Add("Close", ConsoleMenu.Close)
            //     .Add("Action then Close", (thisMenu) => { SomeAction("Close"); thisMenu.CloseMenu(); })
            //     .Add("Exit", () => Environment.Exit(0))
            //     .Configure(config =>
            //     {
            //         config.Selector = "--> ";
            //         config.EnableFilter = true;
            //         config.Title = "Main menu";
            //         config.EnableWriteTitle = true;
            //         config.EnableBreadcrumb = true;
            //     });

            //   menu.Show();



            //var xml = XDocument.Load("employees-departments.xml");
            //foreach (var item in
            //    xml.Element("Employees")!.Elements("Employee"))
            //{
            //    Console.WriteLine(item.Element("name")?.Value);
            //}

            string filePath = "employees-departments.xml";
            XDocument doc = XDocument.Load(filePath);

            var employees = doc.Descendants("Employee").Select(emp => new Employee
            {
                EmployeeId = emp.Attribute("employeeid")?.Value,
                Name = emp.Element("Name")?.Value,
                BirthYear = int.Parse(emp.Element("BirthYear")?.Value ?? "0"),
                StartYear = int.Parse(emp.Element("StartYear")?.Value ?? "0"),
                CompletedProjects = int.Parse(emp.Element("CompletedProjects")?.Value ?? "0"),
                Active = bool.Parse(emp.Element("Active")?.Value ?? "false"),
                Retired = bool.Parse(emp.Element("Retired")?.Value ?? "false"),
                Email = emp.Element("Email")?.Value,
                Phone = emp.Element("Phone")?.Value,
                Job = emp.Element("Job")?.Value,
                Level = emp.Element("Level")?.Value,
                Salary = int.Parse(emp.Element("Salary")?.Value ?? "0"),
                Commission = emp.Element("Commission")?.Attribute("currency")?.Value == "eur" ?
                    ConvertToHUF(decimal.Parse(emp.Element("Commission")?.Value ?? "0")) :
                    decimal.Parse(emp.Element("Commission")?.Value ?? "0"),
                Departments = emp.Element("Departments")?.Elements("Department").Select(dept => new Department
                {
                    Name = dept.Element("Name")?.Value,
                    DepartmentCode = dept.Element("DepartmentCode")?.Value,
                    HeadOfDepartment = dept.Element("HeadOfDepartment")?.Value
                }).ToList()
            }).ToList();

            
            foreach (var emp in employees)
            {
                Console.WriteLine($"Employee ID: {emp.EmployeeId}, Name: {emp.Name}, Commission: {emp.Commission} HUF");
            }

        }

        static decimal ConvertToHUF(decimal eurAmount)
        {
            decimal exchangeRate = 400; 
            return eurAmount * exchangeRate;
        }
        private static void SomeAction(string v)
        {
            throw new NotImplementedException();
        }
    }
}
