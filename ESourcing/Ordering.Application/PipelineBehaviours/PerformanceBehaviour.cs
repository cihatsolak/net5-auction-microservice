using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.PipelineBehaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        #region Fields
        private readonly Stopwatch _stopwatch;
        private readonly ILogger<TRequest> _logger;
        #endregion

        #region Ctor
        public PerformanceBehaviour(
            ILogger<TRequest> logger)
        {
            _stopwatch = new Stopwatch();
            _logger = logger;
        }
        #endregion

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _stopwatch.Start();
            var response = await next();
            _stopwatch.Stop();

            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            if (elapsedMilliseconds >= 550)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                   requestName, elapsedMilliseconds, request);
            }

            return response;
        }
    }
}
