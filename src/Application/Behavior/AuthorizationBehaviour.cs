using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behavior
{
    public class AuthorizationBehaviour<TRequest, TResponse>(ILogger<AuthorizationBehaviour<TRequest, TResponse>> logger) 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            return await next();
        }
    }
}