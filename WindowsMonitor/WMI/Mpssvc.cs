using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class Mpssvc
    {
		public uint Flags { get; private set; }
		public uint Level { get; private set; }

        public static IEnumerable<Mpssvc> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<Mpssvc> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<Mpssvc> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM Mpssvc");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new Mpssvc
                {
                     Flags = (uint) (managementObject.Properties["Flags"]?.Value ?? default(uint)),
		 Level = (uint) (managementObject.Properties["Level"]?.Value ?? default(uint))
                };
        }
    }
}