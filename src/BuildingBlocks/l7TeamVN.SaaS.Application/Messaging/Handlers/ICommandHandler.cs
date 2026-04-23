using l7TeamVN.SaaS.SharedKernel.Results;
using MediatR;

namespace l7TeamVN.SaaS.Application.Messaging.Handlers;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand
{
}
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>
{
}