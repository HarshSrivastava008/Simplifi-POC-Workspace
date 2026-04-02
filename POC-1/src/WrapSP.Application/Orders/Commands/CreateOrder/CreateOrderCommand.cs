using MediatR;

namespace WrapSP.Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    string CustomerName,
    string ProductName,
    int Quantity,
    decimal Price) : IRequest<int>;
