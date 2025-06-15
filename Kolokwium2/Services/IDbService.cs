using Kolokwium2.DTOs;

namespace Kolokwium2.Services;

public interface IDbService
{
    Task<OrderDto> GetOrderById(int orderId);
    Task FulfillOrder(int orderId, FulfillOrderDto dto);
}