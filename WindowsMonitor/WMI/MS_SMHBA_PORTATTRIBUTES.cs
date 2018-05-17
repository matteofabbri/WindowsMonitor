using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class MS_SMHBA_PORTATTRIBUTES
    {
		public string OSDeviceName { get; private set; }
		public byte[] PortSpecificAttributes { get; private set; }
		public uint PortSpecificAttributesSize { get; private set; }
		public uint PortState { get; private set; }
		public uint PortType { get; private set; }
		public ulong Reserved { get; private set; }

        public static IEnumerable<MS_SMHBA_PORTATTRIBUTES> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<MS_SMHBA_PORTATTRIBUTES> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<MS_SMHBA_PORTATTRIBUTES> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MS_SMHBA_PORTATTRIBUTES");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new MS_SMHBA_PORTATTRIBUTES
                {
                     OSDeviceName = (string) (managementObject.Properties["OSDeviceName"]?.Value ?? default(string)),
		 PortSpecificAttributes = (byte[]) (managementObject.Properties["PortSpecificAttributes"]?.Value ?? new byte[0]),
		 PortSpecificAttributesSize = (uint) (managementObject.Properties["PortSpecificAttributesSize"]?.Value ?? default(uint)),
		 PortState = (uint) (managementObject.Properties["PortState"]?.Value ?? default(uint)),
		 PortType = (uint) (managementObject.Properties["PortType"]?.Value ?? default(uint)),
		 Reserved = (ulong) (managementObject.Properties["Reserved"]?.Value ?? default(ulong))
                };
        }
    }
}