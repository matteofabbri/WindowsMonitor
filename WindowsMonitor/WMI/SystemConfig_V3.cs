using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class SystemConfig_V3
    {
		public uint Flags { get; private set; }

        public static IEnumerable<SystemConfig_V3> Retrieve(string remote, string username, string password)
        {
            var options = new ConnectionOptions
            {
                Impersonation = ImpersonationLevel.Impersonate,
                Username = username,
                Password = password
            };

            var managementScope = new ManagementScope(new ManagementPath($"\\\\{remote}\\root\\wmi"), options);
            managementScope.Connect();

            return Retrieve(managementScope);
        }

        public static IEnumerable<SystemConfig_V3> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<SystemConfig_V3> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM SystemConfig_V3");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new SystemConfig_V3
                {
                     Flags = (uint) (managementObject.Properties["Flags"]?.Value ?? default(uint))
                };
        }
    }
}