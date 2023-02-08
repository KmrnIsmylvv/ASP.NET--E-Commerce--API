using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);

        return await _unitOfWork.Repository<Order>().ListAsync(spec);
    }

    public async Task<Order> GetOrderById(int id, string buyerEmail)
    {
        var spec = new OrderWithItemsAndOrderingSpecification(id, buyerEmail);

        return _unitOfWork.Repository<Order>().GetEntityWithSpecs(spec);
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }
}