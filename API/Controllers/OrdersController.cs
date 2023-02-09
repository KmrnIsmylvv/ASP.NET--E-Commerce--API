using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Address = Core.Entities.OrderAggregate.Address;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers;

[Authorize]
public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
    {
        var email = HttpContext.User.RetrieveEmailFromPrincipal();

        var address = _mapper.Map<Address>(orderDto.ShipToAddress);

        var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

        if (order is null) return BadRequest(new ApiResponse(400, "Problem creating order"));

        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderReturnDto>>> GetOrdersForUser()
    {
        var email = HttpContext.User.RetrieveEmailFromPrincipal();

        var orders = await _orderService.GetOrdersForUserAsync(email);

        return Ok(_mapper.Map<IReadOnlyList<OrderReturnDto>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderReturnDto>> GetOrderByIdForUser(int id)
    {
        var email = HttpContext.User.RetrieveEmailFromPrincipal();

        var order = await _orderService.GetOrderById(id, email);

        if (order is null) return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<OrderReturnDto>(order));
    }

    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<DeliveryMethod>> GetDeliveryMethods()
    {
        return Ok(await _orderService.GetDeliveryMethodsAsync());
    }
}