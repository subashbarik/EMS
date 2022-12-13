using Domain.Types;
using System.Data;
namespace Domain.Interfaces
{
    public interface IGenericDapperRepository
    {
        
        Task<int> CountData(string storedProcedure);
        Task<int> CountDataAsync<T>(string storedProcedure, T parameters);
        Task<int> SaveData<T>(string storedProcedure, T parameters);
        Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters);
        Task<int> CountDataSqlAsync(string sql);
        Task<int> BulkInsertAsync(DataTable data, string storedProcedure, string UDT);
        Task<Dictionary<string, object>> ExecuteWithParams(string storedProcedure, List<DbParam> inParams, List<DbParam> outParams);
    }
}
