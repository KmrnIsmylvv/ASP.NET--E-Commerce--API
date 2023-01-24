using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;
using Address = Core.Entities.Identity.Address;

namespace Core.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId,
        Address shippingAddress);

    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderById(int id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}