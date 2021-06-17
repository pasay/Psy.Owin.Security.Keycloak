﻿using Psy.Owin.Security.Keycloak.Middleware;
using Owin;

namespace Psy.Owin.Security.Keycloak
{
    public static class AppBuilderExtension
    {
        public static IAppBuilder UseKeycloakAuthentication(this IAppBuilder app, KeycloakAuthenticationOptions options)
        {
            app.Use(typeof (KeycloakAuthenticationMiddleware), app, options);
            return app;
        }
    }
}