using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared
{
    public static class AppSettings
    {
        private static IConfiguration? config;
        // netId variable is used to distinguish between the UIC and UIUC user.
        // The netId is passed as a parameter to the API, which is then assigned to this variable.
        // This variable is used in the DataAccess layer to choose the corresponding database.
        public static string netId = "uic.edu";

        public static void SetConfig(IConfiguration configuration)
        {
            config = configuration;
        }

        public static string GetConnectionString(string name)
        {
            return config?.GetConnectionString(name) ?? "";
        }
    }
}
