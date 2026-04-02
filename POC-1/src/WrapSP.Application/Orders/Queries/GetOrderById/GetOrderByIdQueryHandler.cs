using System.Data;
using Dapper;
using MediatR;
using WrapSP.Application.Common.Interfaces;
using WrapSP.Application.DTOs;

namespace WrapSP.Application.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetOrderByIdQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<OrderDto>(
            "sp_GetOrderById",
            new { request.OrderId },
            commandType: CommandType.StoredProcedure);
    }
}
