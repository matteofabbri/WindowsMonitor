using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Hardware.Network.Fddi
{
    /// <summary>
    /// </summary>
    public sealed class FddiShortCurrentAddress
    {
		public bool Active { get; private set; }
		public string InstanceName { get; private set; }
		public dynamic NdisCurrentAddress { get; private set; }

        public static IEnumerable<FddiShortCurrentAddress> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<FddiShortCurrentAddress> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<FddiShortCurrentAddress> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSNdis_FddiShortCurrentAddress");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new FddiShortCurrentAddress
                {
                     Active = (bool) (managementObject.Properties["Active"]?.Value ?? default(bool)),
		 InstanceName = (string) (managementObject.Properties["InstanceName"]?.Value ?? default(string)),
		 NdisCurrentAddress = (dynamic) (managementObject.Properties["NdisCurrentAddress"]?.Value ?? default(dynamic))
                };
        }
    }
}