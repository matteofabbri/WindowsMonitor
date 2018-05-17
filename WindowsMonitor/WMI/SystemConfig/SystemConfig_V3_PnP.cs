using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class SystemConfig_V3_PnP
    {
		public uint DescriptionLength { get; private set; }
		public string DeviceDescription { get; private set; }
		public string DeviceID { get; private set; }
		public uint Flags { get; private set; }
		public string FriendlyName { get; private set; }
		public uint FriendlyNameLength { get; private set; }
		public uint IDLength { get; private set; }
		public string PdoName { get; private set; }

        public static IEnumerable<SystemConfig_V3_PnP> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<SystemConfig_V3_PnP> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<SystemConfig_V3_PnP> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM SystemConfig_V3_PnP");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new SystemConfig_V3_PnP
                {
                     DescriptionLength = (uint) (managementObject.Properties["DescriptionLength"]?.Value ?? default(uint)),
		 DeviceDescription = (string) (managementObject.Properties["DeviceDescription"]?.Value ?? default(string)),
		 DeviceID = (string) (managementObject.Properties["DeviceID"]?.Value ?? default(string)),
		 Flags = (uint) (managementObject.Properties["Flags"]?.Value ?? default(uint)),
		 FriendlyName = (string) (managementObject.Properties["FriendlyName"]?.Value ?? default(string)),
		 FriendlyNameLength = (uint) (managementObject.Properties["FriendlyNameLength"]?.Value ?? default(uint)),
		 IDLength = (uint) (managementObject.Properties["IDLength"]?.Value ?? default(uint)),
		 PdoName = (string) (managementObject.Properties["PdoName"]?.Value ?? default(string))
                };
        }
    }
}