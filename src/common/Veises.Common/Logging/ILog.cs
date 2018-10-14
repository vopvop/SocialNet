using System;
using JetBrains.Annotations;

namespace Veises.Common.Logging
{
    public interface ILog
    {
        void LogInfo([NotNull] string message);

        void LogWarning([NotNull] string message);

        void LogError(Exception e, [CanBeNull] string message);

        void LogDebug([NotNull] string message);

        void LogTrace([NotNull] string message);
    }

    public interface ILog<T> : ILog
    {
        
    }
}