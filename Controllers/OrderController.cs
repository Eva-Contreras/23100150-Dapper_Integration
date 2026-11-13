using Microsoft.AspNetCore.Mvc;
using Orders.Api.models;
using Orders.Api.models.dtos;
using Orders.Api.services;

[ApiController]
[Route("api/[controller]")]

public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
    {
        var orderId = await _orderService.CreateOrderAsync(createOrderDto);
        return StatusCode(201, new { OrderId = orderId });
    }

    [HttpGet("{orderId:int}")]
    public async Task<ActionResult<Order>> GetOrderById(int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);

        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Order>>> GetAllOrders(
        [FromQuery] string? status,
        [FromQuery] int? customerId,
        [FromQuery] string? orderNumber)
    {
        var orders = await _orderService.GetAllOrdersAsync(
            status,
            customerId,
            orderNumber);
        return Ok(orders);
    }

    [HttpPut]
    public async Task<IActionResult> PutOrderFields(ChangeOrderDto changeOrderDto)
    {
        await _orderService.UpdateOrderAsync(changeOrderDto);
        return Ok(new { Message = "Se cambiaron los campos correctamente" });
    }

    [HttpDelete("{orderId:int}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        await _orderService.DeleteOrderAsync(orderId);
        return NoContent();
    }
}