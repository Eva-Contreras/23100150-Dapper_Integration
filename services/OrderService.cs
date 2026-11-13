using Orders.Api.models;
using Orders.Api.models.dtos;
using Orders.Api.repositories;
using Orders.Api.responses;

namespace Orders.Api.services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var response = await _repository.CreateAsync(createOrderDto);
            EnsureSuccess(response);

            if (response.Result is int orderId)
            {
                return orderId;
            }

            throw new InvalidOperationException(
                "The database did not return the new order ID.");
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            var response = await _repository.GetByIdAsync(orderId);
            EnsureSuccess(response);

            if (response.Result is null)
            {
                return null;
            }

            if (response.Result is Order order)
            {
                return order;
            }

            throw new InvalidOperationException(
                "The database returned an unexpected order result.");
        }

        public async Task<IReadOnlyList<Order>> GetAllOrdersAsync(
            string? status = null,
            int? customerId = null,
            string? orderNumber = null)
        {
            var response = await _repository.GetAllAsync(
                status,
                customerId,
                orderNumber);
            EnsureSuccess(response);

            if (response.Result is IEnumerable<Order> orders)
            {
                return orders.ToList();
            }

            throw new InvalidOperationException(
                "The database did not return a list of orders.");
        }

        public async Task UpdateOrderAsync(ChangeOrderDto changeOrderDto)
        {
            var response = await _repository.UpdateAsync(changeOrderDto);
            EnsureSuccess(response);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var response = await _repository.DeleteAsync(orderId);
            EnsureSuccess(response);
        }

        private static void EnsureSuccess(DapperResponse response)
        {
            if (!response.Success)
            {
                throw new InvalidOperationException(
                    response.InnerException ?? "The database operation failed.");
            }
        }
    }
}