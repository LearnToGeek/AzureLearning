using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TwoBrainTwentyFinger
{
    public class AzureHelper
    {
    }

    public static class ConnectionManager
    {
        public static string ServiceBusConnectionString()
        {
            string conStr = ConfigurationSettings.AppSettings["serviceBusNamespace"];

            return conStr;
        }

        public static string queuePath()
        {
            string path = ConfigurationSettings.AppSettings["queuePath"];

            return path;
        }
    }
}