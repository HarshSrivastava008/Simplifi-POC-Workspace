using MediatR;

namespace WrapSP.Application.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(
    int OrderId,
    string CustomerName,
    string ProductName,
    int Quantity,
    decimal Price) : IRequest<bool>;
