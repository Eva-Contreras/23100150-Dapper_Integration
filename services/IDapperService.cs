using Dapper;
using Orders.Api.models.dtos;
using Orders.Api.responses;

namespace Orders.Api.services
{
    public interface IDapperService
    {
        Task<DapperResponse> ExecuteQueryAsync<T>(string query, StoredProcedureInf parameters, string connectionId, bool hasArray = false);
        Task<DapperResponse> ExecuteStoredProcedureAsync<T>(StoredProcedureInf qData, DynamicParameters parameters, bool hasArray = false);
        Task<DapperResponse> ExecuteCommandAsync(StoredProcedureInf qData, DynamicParameters parameters);
    }
}