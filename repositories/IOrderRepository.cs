using Orders.Api.models.dtos;
using Orders.Api.responses;

namespace Orders.Api.repositories
{
    public interface IOrderRepository
    {
        Task<DapperResponse> GetAllAsync(
            string? status = null,
            int? customerId = null,
            string? orderNumber = null);

        Task<DapperResponse> GetByIdAsync(int orderId);

        Task<DapperResponse> CreateAsync(CreateOrderDto order);

        Task<DapperResponse> UpdateAsync(ChangeOrderDto order);

        Task<DapperResponse> DeleteAsync(int orderId);
    }
}