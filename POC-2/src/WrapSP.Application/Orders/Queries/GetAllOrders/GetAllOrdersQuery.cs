using MediatR;
using WrapSP.Application.DTOs;

namespace WrapSP.Application.Orders.Queries.GetAllOrders;

public sealed record GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>;
