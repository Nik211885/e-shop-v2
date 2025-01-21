using FluentValidation;
using MediatR;

namespace Application.Behavior
{
    public class ValidatorBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var validatorResult = await Task.WhenAll(
                validators.Select(x => x.ValidateAsync(context, cancellationToken)
                ));

            var fail = validatorResult
                .Where(x => x.Errors.Count != 0)
                .SelectMany(x => x.Errors)
                .ToList();
            if (fail.Count != 0)
            {
                throw new ValidationException(fail);
            }
            
            return await next();
        }
    }
}