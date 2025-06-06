using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace MyOrdersAPI.DataService
{

    public class DataService : IDataService
    {
        private readonly string _connectionString;
        public DataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> ExecuteAsync(string storedProcedure, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> ExecuteScalarAsync(string storedProcedure, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<T>> QueryRawSqlAsync<T>(string sql, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<T>(sql, parameters);
        }

    }

}

