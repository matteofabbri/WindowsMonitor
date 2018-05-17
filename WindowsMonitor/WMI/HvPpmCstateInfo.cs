using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class HvPpmCstateInfo
    {
		public ulong PowerConsumption0 { get; private set; }
		public ulong PowerConsumption1 { get; private set; }
		public ulong PowerConsumption2 { get; private set; }
		public ulong Type0 { get; private set; }
		public ulong Type1 { get; private set; }
		public ulong Type2 { get; private set; }

        public static IEnumerable<HvPpmCstateInfo> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<HvPpmCstateInfo> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<HvPpmCstateInfo> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM HvPpmCstateInfo");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new HvPpmCstateInfo
                {
                     PowerConsumption0 = (ulong) (managementObject.Properties["PowerConsumption0"]?.Value ?? default(ulong)),
		 PowerConsumption1 = (ulong) (managementObject.Properties["PowerConsumption1"]?.Value ?? default(ulong)),
		 PowerConsumption2 = (ulong) (managementObject.Properties["PowerConsumption2"]?.Value ?? default(ulong)),
		 Type0 = (ulong) (managementObject.Properties["Type0"]?.Value ?? default(ulong)),
		 Type1 = (ulong) (managementObject.Properties["Type1"]?.Value ?? default(ulong)),
		 Type2 = (ulong) (managementObject.Properties["Type2"]?.Value ?? default(ulong))
                };
        }
    }
}