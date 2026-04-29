using FluentValidation;
using l7TeamVN.SaaS.SharedKernel.Results;
using MediatR;
using System.Reflection;

namespace l7TeamVN.SaaS.Application.Behaviors;


public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .Select(failure => new Error(
                failure.PropertyName,
                failure.ErrorMessage))
            .Distinct()
            .ToList();

        if (!validationErrors.Any()) return await next();

        validationErrors.Insert(0, new Error("Validation.Error","One or more validation errors occurred."));

        var errors = validationErrors.ToArray();

        return CreateValidationResult<TResponse>(errors);


    }
    private static TResult CreateValidationResult<TResult>(Error[] errors) where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (Result.Failure(errors) as TResult)!;
        }
        else
        {
            var valueType = typeof(TResult).GetGenericArguments()[0];

            var failureMethod = typeof(Result).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(m => m.Name == nameof(Result.Failure) && m.IsGenericMethod)
                .MakeGenericMethod(valueType);

            var validationResult = failureMethod.Invoke(null, new object[] { errors });

            return (TResult)validationResult!;
        }
    }
}
