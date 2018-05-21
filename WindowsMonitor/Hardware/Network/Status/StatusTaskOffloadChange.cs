using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Hardware.Network.Status
{
    /// <summary>
    /// </summary>
    public sealed class StatusTaskOffloadChange
    {
		public bool Active { get; private set; }
		public string InstanceName { get; private set; }
		public uint NumberElements { get; private set; }
		public byte[] SecurityDescriptor { get; private set; }
		public byte[] TaskOffloadCapabilities { get; private set; }
		public ulong TimeCreated { get; private set; }

        public static IEnumerable<StatusTaskOffloadChange> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<StatusTaskOffloadChange> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<StatusTaskOffloadChange> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSNdis_StatusTaskOffloadChange");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new StatusTaskOffloadChange
                {
                     Active = (bool) (managementObject.Properties["Active"]?.Value ?? default(bool)),
		 InstanceName = (string) (managementObject.Properties["InstanceName"]?.Value ?? default(string)),
		 NumberElements = (uint) (managementObject.Properties["NumberElements"]?.Value ?? default(uint)),
		 SecurityDescriptor = (byte[]) (managementObject.Properties["SECURITY_DESCRIPTOR"]?.Value ?? new byte[0]),
		 TaskOffloadCapabilities = (byte[]) (managementObject.Properties["TaskOffloadCapabilities"]?.Value ?? new byte[0]),
		 TimeCreated = (ulong) (managementObject.Properties["TIME_CREATED"]?.Value ?? default(ulong))
                };
        }
    }
}