using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Hardware.Audio.PortCls
{
    /// <summary>
    /// </summary>
    public sealed class PortClsPowerPolicy
    {
		public byte[] SecurityDescriptor { get; private set; }
		public ulong TimeCreated { get; private set; }

        public static IEnumerable<PortClsPowerPolicy> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<PortClsPowerPolicy> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<PortClsPowerPolicy> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM PortCls_PowerPolicy");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new PortClsPowerPolicy
                {
                     SecurityDescriptor = (byte[]) (managementObject.Properties["SECURITY_DESCRIPTOR"]?.Value ?? new byte[0]),
		 TimeCreated = (ulong) (managementObject.Properties["TIME_CREATED"]?.Value ?? default(ulong))
                };
        }
    }
}