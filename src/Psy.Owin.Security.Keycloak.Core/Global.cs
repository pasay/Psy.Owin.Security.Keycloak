using Psy.Owin.Security.Keycloak.IdentityModel.Utilities.Caching;
using System.Collections.Concurrent;

namespace Psy.Owin.Security.Keycloak
{
    internal static class Global
    {
        public static StateCache StateCache { get; } = new StateCache();

        public static ConcurrentDictionary<string, KeycloakAuthenticationOptions> KeycloakOptionStore { get; } = new ConcurrentDictionary<string, KeycloakAuthenticationOptions>();
    }
}