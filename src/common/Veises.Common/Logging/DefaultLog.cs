using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Veises.Common.Logging
{
    internal sealed class DefaultLog<T> : ILog<T>
    {
        private readonly ILogger _logger;

        public DefaultLog([NotNull] ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(Exception e, string message)
        {
            _logger.LogError(e, message);
        }

        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }

        public void LogTrace(string message)
        {
            _logger.LogTrace(message);
        }
    }
}