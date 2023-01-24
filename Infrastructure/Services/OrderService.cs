using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Address = Core.Entities.Identity.Address;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    public Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress)
    {
        throw new System.NotImplementedException();
    }

    public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        throw new System.NotImplementedException();
    }

    public Task<Order> GetOrderById(int id, string buyerEmail)
    {
        throw new System.NotImplementedException();
    }

    public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        throw new System.NotImplementedException();
    }
}