using Application.Dtos;
using Application.EmployeeService.Queries;
using AutoMapper;
using Domain.Interfaces;
using Domain.Specifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using TestDataProvider.EmployeeTestDataProviderService;
using TestDataProvider.Interfaces;

namespace ApplicationTests
{
    public class GetAllEmployeeHandlerTests
    {
        private readonly IEmployeeTestService _employeeTestService;
        public GetAllEmployeeHandlerTests()
        {
            _employeeTestService = new EmployeeTestService();
        }
        [Fact]
        public async void GetAllEmployeeHandler_Handle_FetchAll_Employee()
        {
            using (var mock = AutoMock.GetStrict())
            {
                //Arrange
                int employeeCount = 100;
                var employeesDtos = _employeeTestService.GetEmployeesDto();


                mock.Mock<IGenericDapperRepository>()
                    .Setup(x => x.CountData("usp_GetEmployeeCount"))
                    .ReturnsAsync(employeeCount);
               
                mock.Mock<IGenericDapperRepository>()
                    .Setup(x => x.LoadData<EmployeeDto, object>("usp_GetAllEmployees", It.IsAny<object>()))
                    .ReturnsAsync(employeesDtos);

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<IReadOnlyList<EmployeeDto>, IReadOnlyList<EmployeeDto>>(employeesDtos))
                    .Returns(employeesDtos);

                mock.Mock<ILogger<GetAllEmployeeHandler>>();



                // Act
                //myMoq.Verify(x => x.CountData("usp_GetEmployeeCount"), Times.Exactly(1));
               
                var cls = mock.Create<GetAllEmployeeHandler>();
                EmployeeSpecParams employeeParams = new EmployeeSpecParams();
                employeeParams.PageIndex = 1;
                employeeParams.PageSize = 10;
                
                var queryClass = new GetAllEmployeeQuery(employeeParams);
                var cancelToken = new CancellationToken();

                var expected = _employeeTestService.GetPagedData();
                var actual = await cls.Handle(queryClass, cancelToken);

                // Assert
                Assert.True(expected != null);
                Assert.True(actual != null);

                Assert.Equal(expected.Count, actual.Count);

                Assert.Equal(expected.Data.Count, actual.Data.Count);

                // Fluent Assert
                actual.Should().NotBeNull();
                //actual.Should().Be(expected);
                actual.Count.Should().Be(expected.Count);
                actual.Count.Should().Be(100);

            }

        }
        [Fact]
        // Verifies that CountData method of IGenericDapperRepository is called once
        public void GetAllEmployeeHandler_Handle_CountData_Should_Call_Once()
        {
            //Arrange
            var unitOfWorkMoq = new Mock<IUnitOfWork>();
            var dapperMoq = new Mock<IGenericDapperRepository>();
            var mapperMoq = new Mock<IMapper>();

            
            EmployeeSpecParams employeeParams = new EmployeeSpecParams();
            employeeParams.PageIndex = 1;
            employeeParams.PageSize = 10;

            var queryClass = new GetAllEmployeeQuery(employeeParams);
            var cancelToken = new CancellationToken();


            var handler =  new GetAllEmployeeHandler(unitOfWorkMoq.Object,mapperMoq.Object,dapperMoq.Object,null);
            //Act
            var output = handler.Handle(queryClass, cancelToken);
            //Assert
            dapperMoq.Verify(x => x.CountData(It.IsAny<string>()),Times.Exactly(1));
        }
        [Fact]
        public void TestMethod1()
        {
            IEnumerable<int> collection = new[] { 1, 2, 3 };
            //Assert.ThrowsAny<Exception>

        }

    }
    
}
