using l7TeamVN.SaaS.SharedKernel.Results;
using MediatR;

namespace l7TeamVN.SaaS.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
