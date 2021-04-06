using System;
using System.Security.Claims;

namespace Services.Interfaceses
{
    public interface IJwtTokenBuilder
    {
        string BuildToken(DateTime issuedAt, int expirationInMinutes, Claim[] claims = null);
    }
}
