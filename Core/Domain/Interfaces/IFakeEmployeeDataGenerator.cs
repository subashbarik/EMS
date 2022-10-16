using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IFakeEmployeeDataGenerator
    {
        Task<List<Employee>> GenerateFakeData(int noOfRecords,bool insertRecords=false, ORMFrameWork ormFrameWork = ORMFrameWork.EF);
    }
}
