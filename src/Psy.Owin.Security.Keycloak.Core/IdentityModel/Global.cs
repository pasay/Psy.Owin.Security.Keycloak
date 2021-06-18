using System.Reflection;

namespace Psy.Owin.Security.Keycloak.IdentityModel
{
    public static class Global
    {
        public static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public static bool CheckVersion(string version)
        {
            return GetVersion() == version;
        }
    }
}