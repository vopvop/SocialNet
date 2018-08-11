using System;

namespace Veises.Common.Service.Utils
{
    internal sealed class TimeService : ITimeService
    {
        public DateTime GetCurrentUtc()
        {
            return DateTime.UtcNow;
        }
    }
}