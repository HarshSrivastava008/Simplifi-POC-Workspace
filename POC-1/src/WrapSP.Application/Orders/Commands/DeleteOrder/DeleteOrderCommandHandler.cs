using System.Data;
using Dapper;
using MediatR;
using WrapSP.Application.Common.Interfaces;

namespace WrapSP.Application.Orders.Commands.DeleteOrder;

public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DeleteOrderCommandHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "sp_DeleteOrder",
            new { request.OrderId },
            commandType: CommandType.StoredProcedure);

        return rowsAffected > 0;
    }
}
