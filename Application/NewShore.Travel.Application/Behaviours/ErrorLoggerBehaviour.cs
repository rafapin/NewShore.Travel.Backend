using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.Logging;

namespace NewShore.Travel.Application.Behaviours
{
    public class ErrorLoggerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        private readonly ILogger<ErrorLoggerBehaviour<TRequest, TResponse>> _logger;
        public ErrorLoggerBehaviour(ILogger<ErrorLoggerBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse nextResponse = default;
            try
            {
                nextResponse = await next();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message: {ex.Message} , StackTrace: ${ex.StackTrace}");
            }
            return nextResponse ?? throw new ArgumentNullException(nameof(nextResponse));
        }
    }
}
