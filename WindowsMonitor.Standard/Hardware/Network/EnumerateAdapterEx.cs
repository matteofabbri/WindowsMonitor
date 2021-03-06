using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Hardware.Network
{
    /// <summary>
    /// </summary>
    public sealed class EnumerateAdapterEx
    {
		public bool Active { get; private set; }
		public dynamic EnumerateAdapter { get; private set; }
		public string InstanceName { get; private set; }

        public static IEnumerable<EnumerateAdapterEx> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<EnumerateAdapterEx> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<EnumerateAdapterEx> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSNdis_EnumerateAdapterEx");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new EnumerateAdapterEx
                {
                     Active = (bool) (managementObject.Properties["Active"]?.Value ?? default(bool)),
		 EnumerateAdapter = (dynamic) (managementObject.Properties["EnumerateAdapter"]?.Value ?? default(dynamic)),
		 InstanceName = (string) (managementObject.Properties["InstanceName"]?.Value ?? default(string))
                };
        }
    }
}