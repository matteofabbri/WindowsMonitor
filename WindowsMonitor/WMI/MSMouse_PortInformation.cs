using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class MSMouse_PortInformation
    {
		public bool Active { get; private set; }
		public uint Buttons { get; private set; }
		public uint ConnectorType { get; private set; }
		public uint DataQueueSize { get; private set; }
		public uint ErrorCount { get; private set; }
		public uint HardwareType { get; private set; }
		public string InstanceName { get; private set; }

        public static IEnumerable<MSMouse_PortInformation> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<MSMouse_PortInformation> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<MSMouse_PortInformation> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSMouse_PortInformation");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new MSMouse_PortInformation
                {
                     Active = (bool) (managementObject.Properties["Active"]?.Value ?? default(bool)),
		 Buttons = (uint) (managementObject.Properties["Buttons"]?.Value ?? default(uint)),
		 ConnectorType = (uint) (managementObject.Properties["ConnectorType"]?.Value ?? default(uint)),
		 DataQueueSize = (uint) (managementObject.Properties["DataQueueSize"]?.Value ?? default(uint)),
		 ErrorCount = (uint) (managementObject.Properties["ErrorCount"]?.Value ?? default(uint)),
		 HardwareType = (uint) (managementObject.Properties["HardwareType"]?.Value ?? default(uint)),
		 InstanceName = (string) (managementObject.Properties["InstanceName"]?.Value ?? default(string))
                };
        }
    }
}