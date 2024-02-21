using Dapper;
using Inventory.Products.DataAccess.Models.Configurations;
using Inventory.Products.DataAccess.Helpers.IHelpers;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Inventory.Products.DataAccess.Models.Dtos;

namespace Inventory.Products.DataAccess.Helpers
{
    public class DapperHelper : IDapperHelper
    {
        #region Properties

        private LogicConfiguration _logicConfiguration;

        #endregion

        public DapperHelper(IOptions<LogicConfiguration> logicConfig)
        {
            _logicConfiguration = logicConfig.Value;
        }

        #region Public Methods

        public async Task<List<T>> QueryAsync<T>(string proc, DynamicParameters p, CommandType commandType = CommandType.StoredProcedure, int commandTimeOut = 6000)
        {
            using IDbConnection connection = GetConnection();
            connection.Open();
            var response = await connection.QueryAsync<T>(proc, p, commandType: commandType, commandTimeout: commandTimeOut);
            connection.Close();
            return response.ToList();
        }

        public List<T> Query<T>(string proc, DynamicParameters p, CommandType commandType = CommandType.StoredProcedure, int commandTimeOut = 6000)
        {
            using IDbConnection connection = GetConnection();
            connection.Open();
            var response = connection.Query<T>(proc, p, commandType: commandType, commandTimeout: commandTimeOut).ToList();
            connection.Close();
            return response;
        }

        public async Task<int> ExecuteAsync(string proc, DynamicParameters p, CommandType commandType = CommandType.StoredProcedure, int commandTimeOut = 6000)
        {
            using IDbConnection connection = GetConnection();
            connection.Open();
            var response = await connection.ExecuteAsync(proc, p, commandType: commandType, commandTimeout: commandTimeOut);
            connection.Close();
            return response;
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string proc, DynamicParameters p, CommandType commandType = CommandType.StoredProcedure, int commandTimeOut = 6000)
        {
            using IDbConnection connection = GetConnection();
            connection.Open();
            var response = await connection.QueryFirstOrDefaultAsync<T>(proc, p, commandType: commandType, commandTimeout: commandTimeOut);
            connection.Close();
            return response;
        }

        public async Task<SpResponseDto<List<T>>> ExecuteSp<T>(string query, DynamicParameters parameters, CommandType cmdType = CommandType.StoredProcedure, int commandTimeout = 6000)
        {
            using IDbConnection connection = GetConnection();
            connection.Open();

            var resultado = connection.QueryMultiple(query, parameters, commandType: cmdType, commandTimeout: commandTimeout);
            var list = await resultado.ReadAsync<T>();
            var count = resultado.Read().FirstOrDefault()!.Total;
            connection.Close();
            var result = new SpResponseDto<List<T>>()
            {
                Data = list.ToList(),
                Total = count
            };
            return result;
        }

        #endregion

        #region Utilitarian Methods

        private IDbConnection GetConnection()
        {
            return new SqlConnection(_logicConfiguration.DatabaseConnection);
        }

        #endregion
    }
}
