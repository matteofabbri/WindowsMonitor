using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.HyperVisor
{
    /// <summary>
    /// </summary>
    public sealed class HvPpmExpectedUtility
    {
		public ulong ExcessBusyTime { get; private set; }
		public ulong IdleTime { get; private set; }
		public ulong LpIndex { get; private set; }
		public ulong PercentageOfMaxFrequency { get; private set; }
		public ulong PerfCheckTime { get; private set; }

        public static IEnumerable<HvPpmExpectedUtility> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<HvPpmExpectedUtility> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<HvPpmExpectedUtility> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM HvPpmExpectedUtility");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new HvPpmExpectedUtility
                {
                     ExcessBusyTime = (ulong) (managementObject.Properties["ExcessBusyTime"]?.Value ?? default(ulong)),
		 IdleTime = (ulong) (managementObject.Properties["IdleTime"]?.Value ?? default(ulong)),
		 LpIndex = (ulong) (managementObject.Properties["LpIndex"]?.Value ?? default(ulong)),
		 PercentageOfMaxFrequency = (ulong) (managementObject.Properties["PercentageOfMaxFrequency"]?.Value ?? default(ulong)),
		 PerfCheckTime = (ulong) (managementObject.Properties["PerfCheckTime"]?.Value ?? default(ulong))
                };
        }
    }
}