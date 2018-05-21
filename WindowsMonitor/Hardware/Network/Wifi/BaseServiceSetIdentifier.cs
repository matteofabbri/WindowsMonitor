using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Hardware.Network.Wifi
{
    /// <summary>
    /// </summary>
    public sealed class BaseServiceSetIdentifier
    {
		public bool Active { get; private set; }
		public string InstanceName { get; private set; }
		public byte[] Ndis80211MacAddress { get; private set; }

        public static IEnumerable<BaseServiceSetIdentifier> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<BaseServiceSetIdentifier> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<BaseServiceSetIdentifier> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSNdis_80211_BaseServiceSetIdentifier");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new BaseServiceSetIdentifier
                {
                     Active = (bool) (managementObject.Properties["Active"]?.Value ?? default(bool)),
		 InstanceName = (string) (managementObject.Properties["InstanceName"]?.Value ?? default(string)),
		 Ndis80211MacAddress = (byte[]) (managementObject.Properties["Ndis80211MacAddress"]?.Value ?? new byte[0])
                };
        }
    }
}