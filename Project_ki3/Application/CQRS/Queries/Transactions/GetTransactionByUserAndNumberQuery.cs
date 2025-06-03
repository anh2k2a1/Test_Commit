using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.CQRS.Queries.Transactions;

public class GetTransactionByUserAndNumberQuery : IRequest<IEnumerable<Transaction>>
{
    public string UserId { get; }
    public int Number { get; }

    public GetTransactionByUserAndNumberQuery(string userId, int number)
    {
        UserId = userId;
        Number = number;
    }
}
