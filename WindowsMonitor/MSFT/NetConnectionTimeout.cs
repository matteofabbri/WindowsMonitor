using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.MSFT
{
    /// <summary>
    /// </summary>
    public sealed class NetConnectionTimeout
    {
		public uint Milliseconds { get; private set; }
		public byte[] SecurityDescriptor { get; private set; }
		public string Service { get; private set; }
		public ulong TimeCreated { get; private set; }

        public static IEnumerable<NetConnectionTimeout> Retrieve(string remote, string username, string password)
        {
            var options = new ConnectionOptions
            {
                Impersonation = ImpersonationLevel.Impersonate,
                Username = username,
                Password = password
            };

            var managementScope = new ManagementScope(new ManagementPath($"\\\\{remote}\\root\\cimv2"), options);
            managementScope.Connect();

            return Retrieve(managementScope);
        }

        public static IEnumerable<NetConnectionTimeout> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<NetConnectionTimeout> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSFT_NetConnectionTimeout");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new NetConnectionTimeout
                {
                     Milliseconds = (uint) (managementObject.Properties["Milliseconds"]?.Value ?? default(uint)),
		 SecurityDescriptor = (byte[]) (managementObject.Properties["SECURITY_DESCRIPTOR"]?.Value ?? new byte[0]),
		 Service = (string) (managementObject.Properties["Service"]?.Value),
		 TimeCreated = (ulong) (managementObject.Properties["TIME_CREATED"]?.Value ?? default(ulong))
                };
        }
    }
}