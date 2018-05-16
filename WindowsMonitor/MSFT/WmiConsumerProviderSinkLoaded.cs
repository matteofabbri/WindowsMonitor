using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.MSFT
{
    /// <summary>
    /// </summary>
    public sealed class WmiConsumerProviderSinkLoaded
    {
		public short Consumer { get; private set; }
		public string Machine { get; private set; }
		public string Namespace { get; private set; }
		public string ProviderName { get; private set; }
		public byte[] SecurityDescriptor { get; private set; }
		public ulong TimeCreated { get; private set; }

        public static IEnumerable<WmiConsumerProviderSinkLoaded> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<WmiConsumerProviderSinkLoaded> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<WmiConsumerProviderSinkLoaded> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSFT_WmiConsumerProviderSinkLoaded");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new WmiConsumerProviderSinkLoaded
                {
                     Consumer = (short) (managementObject.Properties["Consumer"]?.Value ?? default(short)),
		 Machine = (string) (managementObject.Properties["Machine"]?.Value),
		 Namespace = (string) (managementObject.Properties["Namespace"]?.Value),
		 ProviderName = (string) (managementObject.Properties["ProviderName"]?.Value),
		 SecurityDescriptor = (byte[]) (managementObject.Properties["SECURITY_DESCRIPTOR"]?.Value ?? new byte[0]),
		 TimeCreated = (ulong) (managementObject.Properties["TIME_CREATED"]?.Value ?? default(ulong))
                };
        }
    }
}