using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class MSParallel_AllocFreeCounts
    {
		public bool Active { get; private set; }
		public string InstanceName { get; private set; }
		public uint PortAllocates { get; private set; }
		public uint PortFrees { get; private set; }

        public static IEnumerable<MSParallel_AllocFreeCounts> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<MSParallel_AllocFreeCounts> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<MSParallel_AllocFreeCounts> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSParallel_AllocFreeCounts");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new MSParallel_AllocFreeCounts
                {
                     Active = (bool) (managementObject.Properties["Active"]?.Value ?? default(bool)),
		 InstanceName = (string) (managementObject.Properties["InstanceName"]?.Value ?? default(string)),
		 PortAllocates = (uint) (managementObject.Properties["PortAllocates"]?.Value ?? default(uint)),
		 PortFrees = (uint) (managementObject.Properties["PortFrees"]?.Value ?? default(uint))
                };
        }
    }
}