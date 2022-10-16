using MediatR;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;

namespace Application.FakeDataService.Commands
{
    public class InsertFakeEmployeesHandler : IRequestHandler<InsertFakeEmployeesCommand,string>
    {   
        private readonly IFakeEmployeeDataGenerator _fakeEmployeeDataGenerator;
        public InsertFakeEmployeesHandler(IFakeEmployeeDataGenerator fakeEmployeeDataGenerator)
        {   
            _fakeEmployeeDataGenerator = fakeEmployeeDataGenerator;
        }
        public async Task<string> Handle(InsertFakeEmployeesCommand request, CancellationToken cancellationToken)
        {
            var employees = await _fakeEmployeeDataGenerator.GenerateFakeData(request.noOfRecords, true,ORMFrameWork.Dapper);
            return "Success";
        }
    }
}
