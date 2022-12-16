

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Data
{
    public class EMSContextSeed
    {
        public static async Task SeedAsync(
            EMSContext context,
            ILoggerFactory loggerFactory,
            IFakeEmployeeDataGenerator fakeEmployeeGenerator,
            IWebHostEnvironment hostEnvironment)
        {

            try
            {
                if (!context.Companies.Any())
                {
                    var companyData = File.ReadAllText(hostEnvironment.WebRootPath + "/SeedData/companies.json");
                    var company = JsonSerializer.Deserialize<Company>(companyData);

                    List<Designation> designations= new();
                    if (!context.Designations.Any())
                    {
                        var designationData = File.ReadAllText(hostEnvironment.WebRootPath + "/SeedData/designations.json");
                        designations = JsonSerializer.Deserialize<List<Designation>>(designationData);

                        foreach (var designation in designations)
                        {
                            context.Designations.Add(designation);
                        }
                    }

                    List<EmployeeType> empTypes= new();
                    if (!context.EmployeeTypes.Any())
                    {
                        var empTypeData = File.ReadAllText(hostEnvironment.WebRootPath + "/SeedData/employeetypes.json");
                        empTypes = JsonSerializer.Deserialize<List<EmployeeType>>(empTypeData);

                        foreach (var emp in empTypes)
                        {
                            context.EmployeeTypes.Add(emp);
                        }
                    }

                    if (!context.Departments.Any())
                    {
                        var departmentData = File.ReadAllText(hostEnvironment.WebRootPath + "/SeedData/departments.json");
                        var departments = JsonSerializer.Deserialize<List<Department>>(departmentData);
                        company.Departments = new();
                        foreach (var department in departments)
                        {
                            department.Employees = new();
                            switch (department.Name.ToLower())
                            {
                                case "development":
                                    // generate fake data
                                    var developerEmployees = await fakeEmployeeGenerator.GenerateFakeData(10);
                                    foreach (var employee in developerEmployees)
                                    {
                                        employee.Designation = designations.Find(d => d.Name.ToLower() == "developer");
                                        employee.EmployeeType = empTypes.First(et => et.Name.ToLower() == "contract");
                                        department.Employees.Add(employee);
                                    }
                                    var seniorDeveloperEmployees = await fakeEmployeeGenerator.GenerateFakeData(10);
                                    foreach (var employee in seniorDeveloperEmployees)
                                    {
                                        employee.Designation = designations.Find(d => d.Name.ToLower() == "senior developer");
                                        employee.EmployeeType = empTypes.First(et => et.Name.ToLower() == "permanent");
                                        department.Employees.Add(employee);
                                    }
                                    break;
                                case "hr":
                                    // generate fake data
                                    var hrEmployees = await fakeEmployeeGenerator.GenerateFakeData(10);
                                    foreach (var employee in hrEmployees)
                                    {
                                        employee.Designation = designations.Find(d => d.Name.ToLower() == "hr manager");
                                        employee.EmployeeType = empTypes.First(et => et.Name.ToLower() == "permanent");
                                        department.Employees.Add(employee);
                                    }
                                    break;
                                case "sales":
                                    // generate fake data
                                    var salesEmployees = await fakeEmployeeGenerator.GenerateFakeData(10);
                                    foreach (var employee in salesEmployees)
                                    {
                                        employee.Designation = designations.Find(d => d.Name.ToLower() == "sales manager");
                                        employee.EmployeeType = empTypes.First(et => et.Name.ToLower() == "permanent");
                                        department.Employees.Add(employee);
                                    }
                                    break;
                                case "support":
                                    // generate fake data
                                    var supportEmployees = await fakeEmployeeGenerator.GenerateFakeData(10);
                                    foreach (var employee in supportEmployees)
                                    {
                                        employee.Designation = designations.Find(d => d.Name.ToLower() == "support manager");
                                        employee.EmployeeType = empTypes.First(et => et.Name.ToLower() == "contract");
                                        department.Employees.Add(employee);
                                    }
                                    break;
                                default:
                                    break;

                            }
                            company.Departments.Add(department);
                        }
                    }
                    context.Companies.Add(company);
                    await context.SaveChangesAsync();
                }   

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<EMSContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
