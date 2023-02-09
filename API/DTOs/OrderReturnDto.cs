using System;
using System.Collections.Generic;
using Core.Entities.OrderAggregate;

namespace API.DTOs;

public class OrderReturnDto
{
    public int Id { get; set; }
    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } 
    public Address ShipToAddress { get; set; }
    public string DeliveryMethod { get; set; }
    public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public string OrderStatus { get; set; }
    
    
    
    
    
}