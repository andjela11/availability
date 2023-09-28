using FluentValidation;
using MediatR;

namespace Application.Validation;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errorDectionary = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .ToList();

        if (errorDectionary.Any())
        {
            throw new ValidationException(errorDectionary);
        }

        return await next();
    }
}
