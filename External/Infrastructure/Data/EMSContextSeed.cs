

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class EMSContextSeed
    {
        public static async Task SeedAsync(EMSContext context, ILoggerFactory loggerFactory, IFakeEmployeeDataGenerator fakeEmployeeGenerator)
        {

            try
            {
                if (!context.Companies.Any())
                {
                    var companyData = File.ReadAllText("../External/Infrastructure/Data/SeedData/companies.json");
                    var company = JsonSerializer.Deserialize<Company>(companyData);

                    List<Designation> designations= new();

                    if (!context.Designations.Any())
                    {
                        var designationData = File.ReadAllText("../External/Infrastructure/Data/SeedData/designations.json");
                        designations = JsonSerializer.Deserialize<List<Designation>>(designationData);

                        foreach (var designation in designations)
                        {
                            context.Designations.Add(designation);
                        }
                    }

                    if (!context.Departments.Any())
                    {
                        var departmentData = File.ReadAllText("../External/Infrastructure/Data/SeedData/departments.json");
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
                                        department.Employees.Add(employee);
                                    }
                                    var seniorDeveloperEmployees = await fakeEmployeeGenerator.GenerateFakeData(10);
                                    foreach (var employee in seniorDeveloperEmployees)
                                    {
                                        employee.Designation = designations.Find(d => d.Name.ToLower() == "senior developer");
                                        department.Employees.Add(employee);
                                    }
                                    break;
                                case "hr":
                                    // generate fake data
                                    var hrEmployees = await fakeEmployeeGenerator.GenerateFakeData(10);
                                    foreach (var employee in hrEmployees)
                                    {
                                        employee.Designation = designations.Find(d => d.Name.ToLower() == "hr manager");
                                        department.Employees.Add(employee);
                                    }
                                    break;
                                case "sales":
                                    // generate fake data
                                    var salesEmployees = await fakeEmployeeGenerator.GenerateFakeData(10);
                                    foreach (var employee in salesEmployees)
                                    {
                                        employee.Designation = designations.Find(d => d.Name.ToLower() == "sales manager");
                                        department.Employees.Add(employee);
                                    }
                                    break;
                                case "support":
                                    // generate fake data
                                    var supportEmployees = await fakeEmployeeGenerator.GenerateFakeData(10);
                                    foreach (var employee in supportEmployees)
                                    {
                                        employee.Designation = designations.Find(d => d.Name.ToLower() == "support manager");
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
