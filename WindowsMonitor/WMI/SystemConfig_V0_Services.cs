using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class SystemConfig_V0_Services
    {
		public char[] DisplayName { get; private set; }
		public uint Flags { get; private set; }
		public uint ProcessId { get; private set; }
		public char[] ProcessName { get; private set; }
		public char[] ServiceName { get; private set; }

        public static IEnumerable<SystemConfig_V0_Services> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<SystemConfig_V0_Services> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<SystemConfig_V0_Services> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM SystemConfig_V0_Services");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new SystemConfig_V0_Services
                {
                     DisplayName = (char[]) (managementObject.Properties["DisplayName"]?.Value ?? new char[0]),
		 Flags = (uint) (managementObject.Properties["Flags"]?.Value ?? default(uint)),
		 ProcessId = (uint) (managementObject.Properties["ProcessId"]?.Value ?? default(uint)),
		 ProcessName = (char[]) (managementObject.Properties["ProcessName"]?.Value ?? new char[0]),
		 ServiceName = (char[]) (managementObject.Properties["ServiceName"]?.Value ?? new char[0])
                };
        }
    }
}