using MediatR;
using Microsoft.AspNetCore.Mvc;
using WrapSP.Application.DTOs;
using WrapSP.Application.Orders.Commands.CreateOrder;
using WrapSP.Application.Orders.Commands.DeleteOrder;
using WrapSP.Application.Orders.Commands.UpdateOrder;
using WrapSP.Application.Orders.Queries.GetAllOrders;
using WrapSP.Application.Orders.Queries.GetOrderById;

namespace WrapSP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _mediator.Send(new GetAllOrdersQuery());
        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        return order is not null ? Ok(order) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = orderId }, orderId);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderRequest request)
    {
        var command = new UpdateOrderCommand(id, request.CustomerName, request.ProductName, request.Quantity, request.Price);
        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteOrderCommand(id));
        return success ? NoContent() : NotFound();
    }
}

public sealed record UpdateOrderRequest(
    string CustomerName,
    string ProductName,
    int Quantity,
    decimal Price);
