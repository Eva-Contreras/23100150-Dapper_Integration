using Orders.Api.models;
using Orders.Api.models.dtos;

namespace Orders.Api.services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<IReadOnlyList<Order>> GetAllOrdersAsync(
            string? status = null,
            int? customerId = null,
            string? orderNumber = null);
        Task UpdateOrderAsync(ChangeOrderDto changeOrderDto);
        Task DeleteOrderAsync(int orderId);
    }
}