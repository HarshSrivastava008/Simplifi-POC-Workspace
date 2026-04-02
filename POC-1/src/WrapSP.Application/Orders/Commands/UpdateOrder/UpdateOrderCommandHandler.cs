using System.Data;
using Dapper;
using MediatR;
using WrapSP.Application.Common.Interfaces;

namespace WrapSP.Application.Orders.Commands.UpdateOrder;

public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UpdateOrderCommandHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "sp_UpdateOrder",
            new
            {
                request.OrderId,
                request.CustomerName,
                request.ProductName,
                request.Quantity,
                request.Price
            },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}
