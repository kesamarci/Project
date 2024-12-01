using ConsoleTools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Client;
using Project.Data;
using Project.Logic;
using Project.Models;
using Project.Models.Attributumos;
using System;
using System.Text.Json;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project
{
    public class Program
    {
        
        static void Main(string[] args)
        {

            

            var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<DataDbContext>();
                services.AddScoped<IEmployeeDataProvider, EmployeeDataProvider>();
                services.AddScoped<IManagerDataProvider, ManagerDataProvider>();
                services.AddScoped<IDepartmentDataProvider, DepartmentDataProvider>();

                // Services
                services.AddScoped<IEmployeeService, EmpService>();
                services.AddScoped<IManagerService, ManService>();
                services.AddScoped<IDepartmentService, DepService>();
            })
            .Build();
            host.Start();
           

            using IServiceScope serviceScope = host.Services.CreateScope();
            var depService = host.Services.GetRequiredService<IDepartmentService>();
            var empService = host.Services.GetRequiredService<IEmployeeService>();
            var manService = host.Services.GetRequiredService<IManagerService>();

            DataFetcher df = new DataFetcher();

            var menu2 = new MainMenuCrud(empService, manService, depService);
            var menu = new ConsoleMenu(args, level: 0)
                .Add("Adat import (XML)", () => ImportXmlToDatabase(empService, depService, "employees-departments.xml"))
                .Add("Adat import (WEB JSON)", () => ImportJsonToDatabase(manService, "managers.json"))
                .Add("Adat export", () => df.FetchDataFromProgram())
                .Add("CRUD", (menu) => menu2.Show())
                .Add("Grafikon", () => DisplaySalaryBarChart(empService.GetAllEmployees().ToList()))
                .Add("Lekérdezések", () => SomeAction("Six"))
                .Add("Exit", () => Environment.Exit(0));
              

            
           
                menu.Show();


           
        }
        public static void DisplaySalaryBarChart(List<Employee> employees)
        {
            Console.Clear();
            Console.WriteLine("Alkalmazottak fizetése oszlopdiagram formájában:\n");

            int maxSalary = employees.Max(e => e.Salary);
            int maxNameLength = employees.Max(e => e.Name.Length);

            foreach (var employee in employees)
            {
                string name = employee.Name.PadRight(maxNameLength);
                int barLength = (int)Math.Round((double)employee.Salary / maxSalary * 20); // 20 a max oszlophossz
                string bar = new string('█', barLength);
                Console.WriteLine($"{name} {bar} {employee.Salary:N0} HUF");
            }
            Console.ReadKey();
        }

        public static void ImportXmlToDatabase(IEmployeeService empdata,IDepartmentService depdata  ,string xmlFilePath)
            {
            Console.WriteLine("XML import kezdése..");
                List<Employee> employees = ProcessEmployeesFromXml(xmlFilePath);
                var depCache=new Dictionary<string, Department>();
                foreach (var employee in employees)
                {
                    var departmentAdd=new List<Department>();
                    foreach (var department in employee.Departments)
                    {
                        if (!depCache.TryGetValue(department.DepartmentCode, out var Departmenti))
                        {
                            Departmenti = depdata.GetAllDepartments().FirstOrDefault(d => d.DepartmentCode == department.DepartmentCode);
                            if (Departmenti == null)
                            {
                                Departmenti = department;
                                depCache[department.DepartmentCode] = Departmenti;
                            }
                            
                        }
                    departmentAdd.Add(Departmenti);
                    }
                    employee.Departments = departmentAdd;
                    empdata.AddEmployee(employee);
                }
                if (empdata.GetAllEmployees()!=null&&depdata.GetAllDepartments()!=null)
                {
                     Console.WriteLine("Sikeres Import");
                 Console.ReadKey();
                }
               



            }

            public static void ImportJsonToDatabase(IManagerService data, string jsonFilePath)
            {
                List<Manager> managers = ProcessManagersFromJson(jsonFilePath);

                foreach (var manager in managers)
                {
                   
                      
                         data.AddManager(manager);
                }
                

                Console.WriteLine("JSON adatok importálva az adatbázisba.");
                 Console.ReadKey();
            }

            public static List<Employee> ProcessEmployeesFromXml(string filePath)
            {
                XDocument doc = XDocument.Load(filePath);

                return doc.Descendants("Employee").Select(emp => new Employee
                {
                    Id = emp.Attribute("employeeid")?.Value,
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
                    Departments = emp.Element("Departments")!.Elements("Department").Select(dept => new Department
                    {
                        Name = dept.Element("Name")!.Value,
                        DepartmentCode = dept.Element("DepartmentCode")!.Value,
                        HeadOfDepartment = dept.Element("HeadOfDepartment")!.Value
                    }).ToList()
                }).ToList();
            }

            public static List<Manager> ProcessManagersFromJson(string filePath)
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Manager>>(jsonString);
            }

            
        
        //static List<Employee> ProcessEmployeesFromXml(string filePath)
        //{
        //    XDocument doc = XDocument.Load(filePath);
        //    var employees = doc.Descendants("Employee").Select(emp => new Employee
        //    {
        //        Id = emp.Attribute("employeeid")?.Value,
        //        Name = emp.Element("Name")?.Value,
        //        BirthYear = int.Parse(emp.Element("BirthYear")?.Value ?? "0"),
        //        StartYear = int.Parse(emp.Element("StartYear")?.Value ?? "0"),
        //        CompletedProjects = int.Parse(emp.Element("CompletedProjects")?.Value ?? "0"),
        //        Active = bool.Parse(emp.Element("Active")?.Value ?? "false"),
        //        Retired = bool.Parse(emp.Element("Retired")?.Value ?? "false"),
        //        Email = emp.Element("Email")?.Value,
        //        Phone = emp.Element("Phone")?.Value,
        //        Job = emp.Element("Job")?.Value,
        //        Level = emp.Element("Level")?.Value,
        //        Salary = int.Parse(emp.Element("Salary")?.Value ?? "0"),
        //        Commission = emp.Element("Commission")?.Attribute("currency")?.Value == "eur" ?
        //            ConvertToHUF(decimal.Parse(emp.Element("Commission")?.Value ?? "0")) :
        //            decimal.Parse(emp.Element("Commission")?.Value ?? "0"),
        //        Departments = emp.Element("Departments")!.Elements("Department").Select(dept => new Department
        //        {
        //            Name = dept.Element("Name")!.Value,
        //            DepartmentCode = dept.Element("DepartmentCode")!.Value,
        //            HeadOfDepartment = dept.Element("HeadOfDepartment")!.Value
        //        }).ToList()
        //    }).ToList();

        //    return employees;
        //}
        //private static void ImportXmlData()
        //{

        //    string xmlFilePath = "employees-departments.xml";
        //    List<Employee> employees = ProcessEmployeesFromXml(xmlFilePath);

        //    Console.WriteLine("XML adatok feldolgozva:");
        //    foreach (var emp in employees)
        //    {
        //        Console.WriteLine($"Employee ID: {emp.Id}, Name: {emp.Name}, Salary: {emp.Salary} HUF");
        //    }
        //    Console.WriteLine("\nNyomj meg egy gombot a folytatáshoz...");
        //    Console.ReadKey();
        //}
        //private static void ImportJsonData()
        //{

        //    string jsonFilePath = "managers.json"; 
        //    List<Manager> managers = GetManagersFromFile(jsonFilePath);
        //    Console.WriteLine("\nJSON adatok feldolgozva:");
        //    foreach (var manager in managers)
        //    {
        //        Console.WriteLine($"ID: {manager.ManagerId}, Name: {manager.Name}, Start Year: {manager.StartOfEmployment.ToShortDateString()}, Has MBA: {manager.HasMBA}");
        //    }
        //    Console.WriteLine("\nNyomj meg egy gombot a folytatáshoz...");
        //    Console.ReadKey(); 
        //}
        //private static List<Manager> GetManagersFromFile(string filePath)
        //{
        //    string jsonString = File.ReadAllText(filePath);
        //    return JsonSerializer.Deserialize<List<Manager>>(jsonString);
        //}

        static decimal ConvertToHUF(decimal eurAmount)
        {
            decimal exchangeRate = 410;
            return eurAmount * exchangeRate;
        }


        private static void SomeAction(string v)
        {
            throw new NotImplementedException();
        }
    }

    
}
