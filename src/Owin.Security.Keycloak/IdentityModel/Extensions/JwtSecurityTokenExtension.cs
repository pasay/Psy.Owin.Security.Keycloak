﻿using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Psy.Owin.Security.Keycloak.IdentityModel.Extensions
{
    internal static class JwtSecurityTokenExtension
    {
        public static JObject GetPayloadJObject(this JwtSecurityToken token)
        {
            // TODO: Optimize? Reduce code duplication
            return JObject.Parse(Base64UrlEncoder.Decode(token.RawPayload));
        }
    }
}