using Dapper;
using Inventory.Products.DataAccess.Models.Dtos;
using System.Data;

namespace Inventory.Products.DataAccess.Helpers.IHelpers
{
    public interface IDapperHelper
    {
        Task<List<T>> QueryAsync<T>(string proc, DynamicParameters p, CommandType commandType = CommandType.StoredProcedure, int commandTimeOut = 6000);
        List<T> Query<T>(string proc, DynamicParameters p, CommandType commandType = CommandType.StoredProcedure, int commandTimeOut = 6000);
        Task<int> ExecuteAsync(string proc, DynamicParameters p, CommandType commandType = CommandType.StoredProcedure, int commandTimeOut = 6000);
        Task<T?> QueryFirstOrDefaultAsync<T>(string proc, DynamicParameters p, CommandType commandType = CommandType.StoredProcedure, int commandTimeOut = 6000);
        Task<SpResponseDto<List<T>>> ExecuteSp<T>(string query, DynamicParameters parameters, CommandType cmdType = CommandType.StoredProcedure, int commandTimeout = 6000);
    }
}
