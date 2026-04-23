using l7TeamVN.SaaS.SharedKernel.Results;
using MediatR;

namespace l7TeamVN.SaaS.Application.Messaging;

public interface ICommand : IRequest<Result> 
{ 

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>> 
{ 

}