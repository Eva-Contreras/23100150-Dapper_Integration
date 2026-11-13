using System.Data;
using Dapper;
using Orders.Api.models;
using Orders.Api.models.dtos;
using Orders.Api.responses;
using Orders.Api.services;

namespace Orders.Api.repositories
{
    public class OrderRepository : IOrderRepository
    {
        private const string ConnectionId = "DefaultConnection";
        private readonly IDapperService _dapperService;

        public OrderRepository(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        public async Task<DapperResponse> GetAllAsync(
            string? status = null,
            int? customerId = null,
            string? orderNumber = null)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Status", status);
            parameters.Add("@CustomerId", customerId);
            parameters.Add("@OrderNumber", orderNumber);

            return await _dapperService.ExecuteStoredProcedureAsync<Order>(
                GetProcedure("usp_TransportOrders_GetAll"),
                parameters,
                hasArray: true);
        }

        public async Task<DapperResponse> GetByIdAsync(int orderId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", orderId);

            return await _dapperService.ExecuteStoredProcedureAsync<Order>(
                GetProcedure("usp_TransportOrders_GetById"),
                parameters);
        }

        public async Task<DapperResponse> CreateAsync(CreateOrderDto order)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderNumber", order.OrderNumber);
            parameters.Add("@CustomerId", order.CustomerId);
            parameters.Add("@VehicleId", order.VehicleId);
            parameters.Add("@OriginCity", order.OriginCity);
            parameters.Add("@DestinationCity", order.DestinationCity);
            parameters.Add("@CargoDescription", order.CargoDescription);
            parameters.Add("@WeightKg", order.WeightKg);
            parameters.Add("@FreightAmount", order.FreightAmount);
            parameters.Add("@CurrencyCode", order.CurrencyCode);
            parameters.Add("@Status", order.Status);
            parameters.Add("@PickupDate", order.PickupDate);
            parameters.Add("@DeliveryDate", order.DeliveryDate);
            parameters.Add("@Notes", order.Notes);
            parameters.Add(
                "@OrderId",
                dbType: DbType.Int32,
                direction: ParameterDirection.Output);

            var response = await _dapperService.ExecuteCommandAsync(
                GetProcedure("usp_TransportOrders_Create"),
                parameters);

            if (response.Success)
            {
                response.Result = parameters.Get<int>("@OrderId");
            }

            return response;
        }

        public Task<DapperResponse> UpdateAsync(ChangeOrderDto order)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", order.OrderId);
            parameters.Add("@OrderNumber", order.OrderNumber);
            parameters.Add("@CustomerId", order.CustomerId);
            parameters.Add("@VehicleId", order.VehicleId);
            parameters.Add("@OriginCity", order.OriginCity);
            parameters.Add("@DestinationCity", order.DestinationCity);
            parameters.Add("@CargoDescription", order.CargoDescription);
            parameters.Add("@WeightKg", order.WeightKg);
            parameters.Add("@FreightAmount", order.FreightAmount);
            parameters.Add("@CurrencyCode", order.CurrencyCode);
            parameters.Add("@Status", order.Status);
            parameters.Add("@PickupDate", order.PickupDate);
            parameters.Add("@DeliveryDate", order.DeliveryDate);
            parameters.Add("@Notes", order.Notes);

            return _dapperService.ExecuteCommandAsync(
                GetProcedure("usp_TransportOrders_Update"),
                parameters);
        }

        public Task<DapperResponse> DeleteAsync(int orderId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", orderId);

            return _dapperService.ExecuteCommandAsync(
                GetProcedure("usp_TransportOrders_Delete"),
                parameters);
        }

        private static StoredProcedureInf GetProcedure(string name)
        {
            return new StoredProcedureInf
            {
                IdConnectionString = ConnectionId,
                SchemaName = "dbo",
                Name = name
            };
        }
    }
}