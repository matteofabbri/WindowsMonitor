using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Hardware.SystemConfig.V2
{
    /// <summary>
    /// </summary>
    public sealed class TelemetryInfo
    {
		public uint Flags { get; private set; }
		public dynamic MachineId { get; private set; }

        public static IEnumerable<TelemetryInfo> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<TelemetryInfo> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<TelemetryInfo> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM SystemConfig_V2_TelemetryInfo");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new TelemetryInfo
                {
                     Flags = (uint) (managementObject.Properties["Flags"]?.Value ?? default(uint)),
		 MachineId = (dynamic) (managementObject.Properties["MachineId"]?.Value ?? default(dynamic))
                };
        }
    }
}