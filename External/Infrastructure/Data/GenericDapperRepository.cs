using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Types;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Data
{
    public class GenericDapperRepository : IGenericDapperRepository
    {
        //private IDbConnection _connection;
        private readonly IConfiguration _config;
        public GenericDapperRepository(IConfiguration config)
        {
            _config = config;
        }
        private string GetConnectionString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                IEnumerable<T> rows = await conn.QueryAsync<T>(storedProcedure, parameters,
                            commandType: CommandType.StoredProcedure);
                return rows.ToList();
            }
        }
        public async Task<int> SaveData<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                var affectedRows = await conn.ExecuteAsync(storedProcedure, parameters,
                            commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }
        public async Task UpdateData<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                await conn.ExecuteAsync(storedProcedure, parameters,
                            commandType: CommandType.StoredProcedure);

            }
        }
        public async Task<int> CountData(string storedProcedure)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                var count = await conn.ExecuteScalarAsync<int>(storedProcedure, commandType: CommandType.StoredProcedure);
                return count;
            }
        }
        public async Task<int> CountDataSqlAsync(string sql)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                var count = await conn.ExecuteScalarAsync<int>(sql, commandType: CommandType.Text);
                return count;
            }
        }

        // Returns a dictionary of output params with output param as key and return value as value
        public async Task<Dictionary<string,object>> ExecuteWithParams(string storedProcedure,List<DbParam> inParams, List<DbParam> outParams)
        {
            Dictionary<string,object> output = new Dictionary<string,object>();
            var p = new DynamicParameters();

            if(inParams != null)
            {
                foreach(var inParam in inParams)
                {
                    p.Add("@" + inParam.Name, inParam.Value);
                }
            }

            if (outParams != null)
            {
                foreach (var outParam in outParams)
                {
                    p.Add("@" + outParam.Name, null, outParam.DataType,ParameterDirection.Output);
                }
            }


            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                await conn.ExecuteAsync(storedProcedure,p,commandType: CommandType.StoredProcedure);
                if(outParams != null)
                {
                    foreach(var outParam in outParams)
                    {
                        if(outParam.DataType == DbType.Int32)
                        {
                            output.Add(outParam.Name, p.Get<int>("@" + outParam.Name));
                        }
                    }
                }
            }
            return output;
        }


        public async Task<int> BulkInsertAsync(DataTable data,string storedProcedure, string UDT)
        {   
            var p = new
            {
                employee = data.AsTableValuedParameter(UDT)
            };
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                var count = await conn.ExecuteAsync(storedProcedure, p,commandType: CommandType.StoredProcedure);
                return count;
            }

        }
        
    }
}
