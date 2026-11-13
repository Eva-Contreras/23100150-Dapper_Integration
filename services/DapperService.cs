using System.Data;
using Dapper;
using Orders.Api.context;
using Orders.Api.models.dtos;
using Orders.Api.responses;

namespace Orders.Api.services
{
    public class DapperService : IDapperService
    {
        private readonly SqlDapperContext _dapperContext;
        public DapperService(SqlDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<DapperResponse> ExecuteQueryAsync<T>(string query, StoredProcedureInf parameters, string connectionId, bool hasArray = false)
        {
            try
            {
                dynamic? response;
                using IDbConnection connection = _dapperContext.CreateConnection(connectionId);
                if (hasArray)
                {
                    response = await connection.QueryAsync<T>(query, parameters);
                }
                else
                {
                    response = await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
                }
                return new DapperResponse
                {
                    Success = true,
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new DapperResponse
                {
                    Success = false,
                    InnerException = ex.Message
                };
            }
        }

        public async Task<DapperResponse> ExecuteStoredProcedureAsync<T>(StoredProcedureInf qData, DynamicParameters parameters, bool hasArray = false)
        {
            try
            {
                dynamic? response;
                var spName = $"{qData.SchemaName}.{qData.Name}";
                using IDbConnection connection = _dapperContext.CreateConnection(qData.IdConnectionString);
                if (hasArray)
                {
                    response = await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
                }
                else
                {
                    response = await connection.QuerySingleOrDefaultAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
                }
                return new DapperResponse
                {
                    Success = true,
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new DapperResponse
                {
                    Success = false,
                    InnerException = ex.Message
                };
            }
        }
        public async Task<DapperResponse> ExecuteCommandAsync(StoredProcedureInf qData, DynamicParameters parameters)
        {
            try
            {
                var spName = $"{qData.SchemaName}.{qData.Name}";
                using IDbConnection connection =
                    _dapperContext.CreateConnection(qData.IdConnectionString);

                await connection.ExecuteAsync(
                    spName,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return new DapperResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new DapperResponse
                {
                    Success = false,
                    InnerException = ex.Message
                };
            }
        }
    }
}