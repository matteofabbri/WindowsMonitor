using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class MS_SMHBA_FC_PHY
    {
		public uint MaxFrameSize { get; private set; }
		public uint PhySpeed { get; private set; }
		public uint PhySupportSpeed { get; private set; }
		public byte PhyType { get; private set; }

        public static IEnumerable<MS_SMHBA_FC_PHY> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<MS_SMHBA_FC_PHY> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<MS_SMHBA_FC_PHY> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MS_SMHBA_FC_PHY");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new MS_SMHBA_FC_PHY
                {
                     MaxFrameSize = (uint) (managementObject.Properties["MaxFrameSize"]?.Value ?? default(uint)),
		 PhySpeed = (uint) (managementObject.Properties["PhySpeed"]?.Value ?? default(uint)),
		 PhySupportSpeed = (uint) (managementObject.Properties["PhySupportSpeed"]?.Value ?? default(uint)),
		 PhyType = (byte) (managementObject.Properties["PhyType"]?.Value ?? default(byte))
                };
        }
    }
}