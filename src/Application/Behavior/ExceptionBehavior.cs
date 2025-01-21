using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behavior
{
    public class ExceptionBehavior<TRequest, TResponse>() 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            return await next();
        }
    }
}