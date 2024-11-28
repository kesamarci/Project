using ConsoleTools;
using Project.Models;
using System.Text.Json;
using System.Xml.Linq;

namespace Project
{
    public class Program
    {
        
        static void Main(string[] args)
        {

            var menu = new ConsoleMenu(args, level: 0)
              .Add("Adat import (XML)", () => ImportXmlData())
              .Add("Adat import (WEB JSON)", () => ImportJsonData())
              .Add("Adat export", () => SomeAction("Three"))
              .Add("CRUD", () => SomeAction("Four"))
              .Add("Grafikon", () => SomeAction("Five"))
              .Add("Lekérdezések", () => SomeAction("Six"))
              .Add("Exit", () => Environment.Exit(0));
              

            
           
                menu.Show();
                
            

        }
        static List<Employee> ProcessEmployeesFromXml(string filePath)
        {
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

            return employees;
        }
        private static void ImportXmlData()
        {

            string xmlFilePath = "employees-departments.xml";
            List<Employee> employees = ProcessEmployeesFromXml(xmlFilePath);
            
            Console.WriteLine("XML adatok feldolgozva:");
            foreach (var emp in employees)
            {
                Console.WriteLine($"Employee ID: {emp.EmployeeId}, Name: {emp.Name}, Salary: {emp.Salary} HUF");
            }
            Console.WriteLine("\nNyomj meg egy gombot a folytatáshoz...");
            Console.ReadKey();
        }
        private static void ImportJsonData()
        {
           
            string jsonFilePath = "managers.json"; 
            List<Manager> managers = GetManagersFromFile(jsonFilePath);
            Console.WriteLine("\nJSON adatok feldolgozva:");
            foreach (var manager in managers)
            {
                Console.WriteLine($"ID: {manager.ManagerId}, Name: {manager.Name}, Start Year: {manager.StartOfEmployment.ToShortDateString()}, Has MBA: {manager.HasMBA}");
            }
            Console.WriteLine("\nNyomj meg egy gombot a folytatáshoz...");
            Console.ReadKey(); 
        }
        private static List<Manager> GetManagersFromFile(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Manager>>(jsonString);
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
