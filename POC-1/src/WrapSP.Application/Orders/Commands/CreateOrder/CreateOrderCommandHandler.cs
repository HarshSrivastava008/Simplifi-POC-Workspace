using System.Data;
using Dapper;
using MediatR;
using WrapSP.Application.Common.Interfaces;

namespace WrapSP.Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CreateOrderCommandHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var newOrderId = await connection.ExecuteScalarAsync<int>(
            "sp_CreateOrder",
            new
            {
                request.CustomerName,
                request.ProductName,
                request.Quantity,
                request.Price
            },
            commandType: CommandType.StoredProcedure);

        return newOrderId;
    }
}
