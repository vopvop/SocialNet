using System;
using JetBrains.Annotations;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtToken
    {
        public JwtToken(Guid tokenId, [NotNull] string tokenValue)
        {
            TokenId = tokenId;
            TokenValue = tokenValue ?? throw new ArgumentNullException(nameof(tokenValue));
        }

        public Guid TokenId { get; }

        [NotNull]
        public string TokenValue { get; }
    }
}