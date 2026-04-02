using MediatR;
using WrapSP.Application.DTOs;

namespace WrapSP.Application.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(int OrderId) : IRequest<OrderDto?>;
