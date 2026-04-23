using l7TeamVN.SaaS.SharedKernel.Results;
using MediatR;

namespace l7TeamVN.SaaS.Application.Messaging.Handlers;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>  where TQuery : IQuery<TResponse>
{
}
