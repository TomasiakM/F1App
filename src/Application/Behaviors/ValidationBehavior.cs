using Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Behaviors;
public class ValidationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        Console.WriteLine($"{typeof (IValidator<TRequest>)}");
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var validationErrors = validationResult.Errors
                .GroupBy(
                    e => e.PropertyName,
                    e => e.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Value = errorMessages.First()
                    })
                .ToDictionary(e => e.Key, e => e.Value);

            throw new ValidationFailedException(validationErrors);
        }

        return await next();
    }
}
