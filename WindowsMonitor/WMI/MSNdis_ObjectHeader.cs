using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class MSNdis_ObjectHeader
    {
		public byte Revision { get; private set; }
		public ushort Size { get; private set; }
		public byte Type { get; private set; }

        public static IEnumerable<MSNdis_ObjectHeader> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<MSNdis_ObjectHeader> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<MSNdis_ObjectHeader> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSNdis_ObjectHeader");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new MSNdis_ObjectHeader
                {
                     Revision = (byte) (managementObject.Properties["Revision"]?.Value ?? default(byte)),
		 Size = (ushort) (managementObject.Properties["Size"]?.Value ?? default(ushort)),
		 Type = (byte) (managementObject.Properties["Type"]?.Value ?? default(byte))
                };
        }
    }
}