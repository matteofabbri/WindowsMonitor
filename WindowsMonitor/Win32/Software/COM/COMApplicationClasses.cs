using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Win32.Software.COM
{
    /// <summary>
    /// </summary>
    public sealed class COMApplicationClasses
    {
		public short GroupComponent { get; private set; }
		public short PartComponent { get; private set; }

        public static IEnumerable<COMApplicationClasses> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<COMApplicationClasses> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<COMApplicationClasses> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM Win32_COMApplicationClasses");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new COMApplicationClasses
                {
                     GroupComponent = (short) (managementObject.Properties["GroupComponent"]?.Value ?? default(short)),
		 PartComponent = (short) (managementObject.Properties["PartComponent"]?.Value ?? default(short))
                };
        }
    }
}