using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBasketRepository _basketRepo;

    public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
    {
        _unitOfWork = unitOfWork;
        _basketRepo = basketRepo;
    }

    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId,
        Address shippingAddress)
    {
        // get basket from the repo
        var basket = await _basketRepo.GetBasketAsync(basketId);

        // get items from the product repo
        var orderItems = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            orderItems.Add(orderItem);
        }

        // get delivery method from repo
        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

        // calc subtotal
        var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

        // create an order
        var order = new Order(orderItems, buyerEmail, shippingAddress, deliveryMethod, subtotal);
        _unitOfWork.Repository<Order>().Add(order);

        // save to db
        var result = await _unitOfWork.Complete();

        if (result <= 0) return null;

        // delete basket
        await _basketRepo.DeleteBasketAsync(basketId);

        // return order
        return order;
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