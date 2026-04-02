using MediatR;

namespace WrapSP.Application.Orders.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(int OrderId) : IRequest<bool>;
