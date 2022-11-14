using Application.Options;
using Application.Statics;
using Domain.Entities;
using Domain.Entities.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public FakeEmployeeDataGenerator(
            IGenericDapperRepository dapper,
            IUnitOfWork unitOfWork,
            ILogger<FakeEmployeeDataGenerator> logger,
            IOptions<AppConfigurationOptions> options,
            UserManager<AppUser> userManager
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
            int minDepartmentId, maxDepartmentId, minDesignationId, maxDesignationId,minEmployeeTypeId,maxEmployeeTypeId;
            minDepartmentId = maxDepartmentId = minDesignationId = maxDesignationId = minEmployeeTypeId = maxEmployeeTypeId = 0;

            if (insertRecords)
            {
                List<DbParam> outPutParams = new();

                outPutParams.Add(new DbParam { Name = "minDepartmentId", Value = null, DataType = DbType.Int32 });
                outPutParams.Add(new DbParam { Name = "maxDepartmentId", Value = null, DataType = DbType.Int32 });
                outPutParams.Add(new DbParam { Name = "minDesignationId", Value = null, DataType = DbType.Int32 });
                outPutParams.Add(new DbParam { Name = "maxDesignationId", Value = null, DataType = DbType.Int32 });
                outPutParams.Add(new DbParam { Name = "minEmployeeTypeId", Value = null, DataType = DbType.Int32 });
                outPutParams.Add(new DbParam { Name = "maxEmployeeTypeId", Value = null, DataType = DbType.Int32 });
                // Must have Department and Designation data for the min and max to work

                var outPut = await _dapper.ExecuteWithParams("usp_GetMinAndMaxIdValues",null,outPutParams);

                minDepartmentId = Convert.ToInt32(outPut["minDepartmentId"]);
                maxDepartmentId = Convert.ToInt32(outPut["maxDepartmentId"]);
                minDesignationId = Convert.ToInt32(outPut["minDesignationId"]);
                maxDesignationId = Convert.ToInt32(outPut["maxDesignationId"]);
                minEmployeeTypeId =Convert.ToInt32(outPut["minEmployeeTypeId"]);
                maxEmployeeTypeId =Convert.ToInt32(outPut["maxEmployeeTypeId"]);
                
            }
            Stopwatch stopwatch = new Stopwatch();
            if (ormFrameWork == ORMFrameWork.Dapper)
            {
                stopwatch.Start();
                employees = await InsertDataWithDapper(noOfRecords, insertRecords, minDepartmentId, maxDepartmentId, minDesignationId, maxDesignationId,minEmployeeTypeId,maxEmployeeTypeId);
                stopwatch.Stop();
                _logger.LogInformation("Elapsed Time is {0} ms Dapper", stopwatch.ElapsedMilliseconds);
            }
            else if (ormFrameWork == ORMFrameWork.EF)
            {
                stopwatch.Start();
                employees = await InsertDataWithEF(noOfRecords, insertRecords, minDepartmentId, maxDepartmentId, minDesignationId, maxDesignationId,minEmployeeTypeId,maxEmployeeTypeId);
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
                                                            int maxDesignationId,
                                                            int minEmployeeTypeId,
                                                            int maxEmployeeTypeId)
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
            empTable.Columns.Add("EmployeeTypeId", typeof(int));
            for (int i = 0; i < noOfRecords; i++)
            {
                
                Employee employee = new();
                employee.FirstName = Name.First();
                employee.LastName = Name.Last();
                employee.Age = RandomNumber.Next(8, 100);
                employee.Basic = RandomNumber.Next(2000, 10000);
                employee.ImageUrl = _options.Value.NoImageEmployeePath;
                if (insertRecords)
                {
                    employee.DepartmentId = RandomNumber.Next(minDepartmentId, maxDepartmentId);
                    employee.DesignationId = RandomNumber.Next(minDesignationId, maxDesignationId);
                    employee.EmployeeTypeId = RandomNumber.Next(minEmployeeTypeId, maxEmployeeTypeId);
                }
                empTable.Rows.Add(employee.FirstName,employee.LastName,employee.Age,employee.
                   Basic,employee.ImageUrl,employee.DepartmentId,employee.DesignationId);
            }
            int count = await _dapper.BulkInsertAsync(empTable, "usp_BulkInsertEmployee", "EmployeeUDT");
            return employees;
        }
        private async Task<List<Employee>> InsertDataWithEF(int noOfRecords,
                                                            bool insertRecords,
                                                            int minDepartmentId,
                                                            int maxDepartmentId,
                                                            int minDesignationId,
                                                            int maxDesignationId,
                                                            int minEmployeeTypeId,
                                                            int maxEmployeeTypeId)
        {
            List<Employee> employees = new();
            for (int i = 0; i < noOfRecords; i++)
            {
                Employee employee = new();
                employee.FirstName = Name.First();
                employee.LastName = Name.Last();
                employee.Age = RandomNumber.Next(8, 100);
                //employee.Salary.Basic = RandomNumber.Next(2000, 10000);
                employee.ImageUrl = _options.Value.NoImageEmployeePath;

                //Create a user info in the identity table for the employee
                var user = new AppUser
                {
                    NormalizedUserName = employee.FirstName + " " + employee.LastName,
                    Email = $"{employee.FirstName}_{employee.LastName}_{employee.Age}.gmail.com",
                    UserName = $"{employee.FirstName}_{employee.LastName}_{employee.Age}.gmail.com",
                    DisplayName = employee.FirstName
                };
                var result = await _userManager.CreateAsync(user, "Pa$$w0rd");
                if (result.Succeeded)
                {
                    var createdUser = await _userManager.FindByEmailAsync(user.Email);
                    employee.UserID = createdUser.Id;
                    //Assign the user role
                    if (!await _userManager.IsInRoleAsync(createdUser, Role.User))
                    {
                        await _userManager.AddToRoleAsync(createdUser, Role.User);
                    }
                }

                if (insertRecords)
                {
                    employee.DepartmentId = RandomNumber.Next(minDepartmentId, maxDepartmentId);
                    employee.DesignationId = RandomNumber.Next(minDesignationId, maxDesignationId);
                    employee.EmployeeTypeId = RandomNumber.Next(minEmployeeTypeId, maxEmployeeTypeId);
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
