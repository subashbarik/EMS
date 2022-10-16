using Domain.Entities;
using Domain.Specifications;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureTests
{
    public class GenericEFRepositoryTests
    {
        [Fact]
        public async void GenericEFRepository_GetByIdAsync_With_Id_PresentInDB()
        {
            
            int id = 1;
            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<EMSContext>()
                    .Setup(x => x.Set<Employee>().FindAsync(id))
                    .ReturnsAsync(GetEmployee(id));

                // Act
                var cls = mock.Create<GenericEFRepository<Employee>>();
                var expected = GetEmployee(id);
                var actual = await cls.GetByIdAsync(id);

                // Assert

                Assert.True(expected != null);
                Assert.True(actual != null);

                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
                Assert.Equal(expected.Age, actual.Age);
                Assert.Equal(expected.Salary, actual.Salary);
                Assert.Equal(expected.DepartmentId, actual.DepartmentId);
                Assert.Equal(expected.DesignationId, actual.DesignationId);
            }


        }

        [Fact]
        public async void GenericEFRepository_GetByIdAsync_With_Id_Not_PresentInDB()
        {
            // id does not exists in the database
            int id = -1;
            using(var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<EMSContext>()
                    .Setup(x => x.Set<Employee>().FindAsync(id))
                    .ReturnsAsync(GetEmployee(id));

                // Act
                var cls = mock.Create<GenericEFRepository<Employee>>();
                var expected = GetEmployee(id);
                var actual = await cls.GetByIdAsync(id);

                //Assert

                Assert.True(expected == null);
                Assert.True(actual == null);
            }
        }
        [Fact]
        public void GenericEFRepository_ApplySpecification()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange

                EmployeeSpecParams employeeSpecParams = new EmployeeSpecParams();
                employeeSpecParams.DepartmentId = 1;
                employeeSpecParams.DesignationId = 1;
                employeeSpecParams.Search = "M";

                var spec = new EmployeesWithDepartmentAndDesignationSpecification(employeeSpecParams);

                
                mock.Mock<EMSContext>()
                .Setup(x => x.Set<Employee>().AsQueryable())
                .Returns(GetIQuerable());


                // Act
                var cls = mock.Create<GenericEFRepository<Employee>>();
                //var actual = await cls.ListAsync(spec);


                //Assert


            }
        }



        private Employee GetEmployee(int id)
        {
            List<Employee> employees = new()
            {
                new Employee {Id=1,FirstName="Subash",LastName="Barik",Age=43,DepartmentId=1,DesignationId=1} ,
                new Employee {Id=2,FirstName="Nirupama",LastName="Pradhan",Age=37,DepartmentId=2,DesignationId=2},
                new Employee {Id=3,FirstName="Sunayana",LastName="Barik",Age=12,DepartmentId=3,DesignationId=3}
            };

            var employee = employees.FirstOrDefault(e => e.Id == id);
            return employee;
        }
        private List<Employee> GetEmployees()
        {
            List<Employee> employees = new()
            {
                new Employee {Id=1,FirstName="Subash",LastName="Barik",Age=43,DepartmentId=1,DesignationId=1},
                new Employee {Id=2,FirstName="Nirupama",LastName="Pradhan",Age=37,DepartmentId=2,DesignationId=2},
                new Employee {Id=3,FirstName="Sunayana",LastName="Barik",Age=12,DepartmentId=3,DesignationId=3},
                new Employee {Id=4,FirstName="Mita",LastName="Barik",Age=43,DepartmentId=1,DesignationId=1},
            };
            return employees;
        }

        private IQueryable<Employee> GetIQuerable()
        {
            return GetEmployees().AsQueryable();
        }

        
    }
}
