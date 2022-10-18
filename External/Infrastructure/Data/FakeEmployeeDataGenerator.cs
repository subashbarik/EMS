using Application.Options;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Types;
using Faker;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Diagnostics;


namespace Infrastructure.Data
{
    public class FakeEmployeeDataGenerator : IFakeEmployeeDataGenerator
    {
        private readonly IGenericDapperRepository _dapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IOptions<AppConfigurationOptions> _options;
        private readonly UserManager<IdentityUser> _userManager;

        public FakeEmployeeDataGenerator(
            IGenericDapperRepository dapper,
            IUnitOfWork unitOfWork,
            ILogger<FakeEmployeeDataGenerator> logger,
            IOptions<AppConfigurationOptions> options,
            UserManager<IdentityUser> userManager
            )
        {
            _dapper = dapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _options = options;
            _userManager = userManager;
        }
        public async Task<List<Employee>> GenerateFakeData(int noOfRecords,
                                                           bool insertRecords = false,
                                                           ORMFrameWork ormFrameWork = ORMFrameWork.EF)
        {
            List<Employee> employees = new();
            int minDepartmentId, maxDepartmentId, minDesignationId, maxDesignationId;
            minDepartmentId = maxDepartmentId = minDesignationId = maxDesignationId = 0;

            if (insertRecords)
            {
                List<DbParam> outPutParams = new();

                outPutParams.Add(new DbParam { Name = "minDepartmentId", Value = null, DataType = DbType.Int32 });
                outPutParams.Add(new DbParam { Name = "maxDepartmentId", Value = null, DataType = DbType.Int32 });
                outPutParams.Add(new DbParam { Name = "minDesignationId", Value = null, DataType = DbType.Int32 });
                outPutParams.Add(new DbParam { Name = "maxDesignationId", Value = null, DataType = DbType.Int32 });

                // Must have Department and Designation data for the min and max to work

                var outPut = await _dapper.ExecuteWithParams("usp_GetMinAndMaxIdValues",null,outPutParams);

                minDepartmentId = Convert.ToInt32(outPut["minDepartmentId"]);
                maxDepartmentId = Convert.ToInt32(outPut["maxDepartmentId"]);
                minDesignationId = Convert.ToInt32(outPut["minDesignationId"]);
                maxDesignationId = Convert.ToInt32(outPut["maxDesignationId"]);
                
            }
            Stopwatch stopwatch = new Stopwatch();
            if (ormFrameWork == ORMFrameWork.Dapper)
            {
                stopwatch.Start();
                employees = await InsertDataWithDapper(noOfRecords, insertRecords, minDepartmentId, maxDepartmentId, minDesignationId, maxDesignationId);
                stopwatch.Stop();
                _logger.LogInformation("Elapsed Time is {0} ms Dapper", stopwatch.ElapsedMilliseconds);
            }
            else if (ormFrameWork == ORMFrameWork.EF)
            {
                stopwatch.Start();
                employees = await InsertDataWithEF(noOfRecords, insertRecords, minDepartmentId, maxDepartmentId, minDesignationId, maxDesignationId);
                stopwatch.Stop();
                _logger.LogInformation("Elapsed Time is {0} ms EF", stopwatch.ElapsedMilliseconds);
            }
            return employees;
        }
        private async Task<List<Employee>> InsertDataWithDapper(int noOfRecords,
                                                            bool insertRecords,
                                                            int minDepartmentId,
                                                            int maxDepartmentId,
                                                            int minDesignationId,
                                                            int maxDesignationId)
        {
            List<Employee> employees = new();
            var empTable = new DataTable();
            empTable.Columns.Add("FirstName", typeof(string));
            empTable.Columns.Add("LastName", typeof(string));
            empTable.Columns.Add("Age", typeof(int));
            empTable.Columns.Add("Salary", typeof(double));
            empTable.Columns.Add("ImageUrl", typeof(string));
            empTable.Columns.Add("DepartmentId", typeof(int));
            empTable.Columns.Add("DesignationId", typeof(int));

            for (int i = 0; i < noOfRecords; i++)
            {
                
                Employee employee = new();
                employee.FirstName = Name.First();
                employee.LastName = Name.Last();
                employee.Age = RandomNumber.Next(8, 100);
                employee.Salary = RandomNumber.Next(2000, 10000);
                employee.ImageUrl = _options.Value.NoImageEmployeePath;
                if (insertRecords)
                {
                    employee.DepartmentId = RandomNumber.Next(minDepartmentId, maxDepartmentId);
                    employee.DesignationId = RandomNumber.Next(minDesignationId, maxDesignationId);
                }
                empTable.Rows.Add(employee.FirstName,employee.LastName,employee.Age,employee.Salary,employee.ImageUrl,employee.DepartmentId,employee.DesignationId);
            }
            int count = await _dapper.BulkInsertAsync(empTable, "usp_BulkInsertEmployee", "EmployeeUDT");
            return employees;
        }
        private async Task<List<Employee>> InsertDataWithEF(int noOfRecords,
                                                            bool insertRecords,
                                                            int minDepartmentId,
                                                            int maxDepartmentId,
                                                            int minDesignationId,
                                                            int maxDesignationId)
        {
            List<Employee> employees = new();
            for (int i = 0; i < noOfRecords; i++)
            {
                Employee employee = new();
                employee.FirstName = Name.First();
                employee.LastName = Name.Last();
                employee.Age = RandomNumber.Next(8, 100);
                employee.Salary = RandomNumber.Next(2000, 10000);
                employee.ImageUrl = _options.Value.NoImageEmployeePath;

                //Create a user info in the identity table for the employee
                var user = new IdentityUser
                {
                    NormalizedUserName = employee.FirstName + " " + employee.LastName,
                    Email = $"fakemail_{employee.FirstName}_{employee.LastName}_{employee.Age}.gmail.com",
                    UserName = employee.FirstName,
                };
                var result = await _userManager.CreateAsync(user, "Pa$$w0rd");
                if (result.Succeeded)
                {
                    var createdUser = await _userManager.FindByEmailAsync(user.Email);
                    employee.UserID = createdUser.Id;
                    //Assign the user role
                    if (!await _userManager.IsInRoleAsync(createdUser, "User"))
                    {
                        await _userManager.AddToRoleAsync(createdUser, "User");
                    }
                }

                if (insertRecords)
                {
                    employee.DepartmentId = RandomNumber.Next(minDepartmentId, maxDepartmentId);
                    employee.DesignationId = RandomNumber.Next(minDesignationId, maxDesignationId);
                }
                employees.Add(employee);
                if (insertRecords)
                {
                    _unitOfWork.Repository<Employee>().Add(employee);
                }
            }
            if (insertRecords && noOfRecords > 0)
            {
                await _unitOfWork.Complete();
            }
            return employees;
        }
    }
}
