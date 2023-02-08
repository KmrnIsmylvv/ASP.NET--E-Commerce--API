using Core.Entities.OrderAggregate;
using Address = Core.Entities.Identity.Address;

namespace Core.Specifications;

public class OrderWithItemsAndOrderingSpecification : BaseSpecification<Order>
{
    public OrderWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
    {
        AddInclude(o=>o.OrderItems);
        AddInclude(o=>o.DeliveryMethod);
        AddOrderByDescending(o=>o.OrderDate);
    }

    public OrderWithItemsAndOrderingSpecification(int id, string email) 
        : base(o=>o.Id == id && o.BuyerEmail == email)
    {
        AddInclude(o=>o.OrderItems);
        AddInclude(o=>o.DeliveryMethod);
    }
}