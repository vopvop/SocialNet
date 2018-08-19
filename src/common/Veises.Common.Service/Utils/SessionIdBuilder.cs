using System;

namespace Veises.Common.Service.Utils
{
    internal sealed class SessionIdBuilder
    {
        public static string Build()
        {
            return Guid.NewGuid().ToString("D");
        }
    }
}