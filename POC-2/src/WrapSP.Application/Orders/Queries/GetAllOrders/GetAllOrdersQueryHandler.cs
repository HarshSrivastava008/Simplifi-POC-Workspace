using System.Data;
using Dapper;
using MediatR;
using WrapSP.Application.Common.Interfaces;
using WrapSP.Application.DTOs;

namespace WrapSP.Application.Orders.Queries.GetAllOrders;

public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetAllOrdersQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<OrderDto>(
            "sp_GetAllOrders",
            commandType: CommandType.StoredProcedure);
    }
}
