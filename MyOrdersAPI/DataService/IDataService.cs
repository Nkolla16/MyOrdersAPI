using System;
namespace MyOrdersAPI.DataService
{

    public interface IDataService
    {
        Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, object parameters = null);
        Task<int> ExecuteAsync(string storedProcedure, object parameters = null);
        Task<int> ExecuteScalarAsync(string storedProcedure, object parameters = null);
        Task<IEnumerable<T>> QueryRawSqlAsync<T>(string sql, object parameters = null);


    }

}
