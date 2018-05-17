using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class SystemConfig_V3_Services
    {
		public string DisplayName { get; private set; }
		public uint Flags { get; private set; }
		public string LoadOrderGroup { get; private set; }
		public uint ProcessId { get; private set; }
		public string ProcessName { get; private set; }
		public string ServiceName { get; private set; }
		public uint ServiceState { get; private set; }
		public uint SubProcessTag { get; private set; }
		public string SvchostGroup { get; private set; }

        public static IEnumerable<SystemConfig_V3_Services> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<SystemConfig_V3_Services> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<SystemConfig_V3_Services> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM SystemConfig_V3_Services");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new SystemConfig_V3_Services
                {
                     DisplayName = (string) (managementObject.Properties["DisplayName"]?.Value ?? default(string)),
		 Flags = (uint) (managementObject.Properties["Flags"]?.Value ?? default(uint)),
		 LoadOrderGroup = (string) (managementObject.Properties["LoadOrderGroup"]?.Value ?? default(string)),
		 ProcessId = (uint) (managementObject.Properties["ProcessId"]?.Value ?? default(uint)),
		 ProcessName = (string) (managementObject.Properties["ProcessName"]?.Value ?? default(string)),
		 ServiceName = (string) (managementObject.Properties["ServiceName"]?.Value ?? default(string)),
		 ServiceState = (uint) (managementObject.Properties["ServiceState"]?.Value ?? default(uint)),
		 SubProcessTag = (uint) (managementObject.Properties["SubProcessTag"]?.Value ?? default(uint)),
		 SvchostGroup = (string) (managementObject.Properties["SvchostGroup"]?.Value ?? default(string))
                };
        }
    }
}