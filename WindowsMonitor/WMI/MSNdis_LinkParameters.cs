using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class MSNdis_LinkParameters
    {
		public uint AutoNegotiationFlags { get; private set; }
		public dynamic Header { get; private set; }
		public uint MediaDuplexState { get; private set; }
		public uint PauseFunctions { get; private set; }
		public ulong RcvLinkSpeed { get; private set; }
		public ulong XmitLinkSpeed { get; private set; }

        public static IEnumerable<MSNdis_LinkParameters> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<MSNdis_LinkParameters> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<MSNdis_LinkParameters> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSNdis_LinkParameters");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new MSNdis_LinkParameters
                {
                     AutoNegotiationFlags = (uint) (managementObject.Properties["AutoNegotiationFlags"]?.Value ?? default(uint)),
		 Header = (dynamic) (managementObject.Properties["Header"]?.Value ?? default(dynamic)),
		 MediaDuplexState = (uint) (managementObject.Properties["MediaDuplexState"]?.Value ?? default(uint)),
		 PauseFunctions = (uint) (managementObject.Properties["PauseFunctions"]?.Value ?? default(uint)),
		 RcvLinkSpeed = (ulong) (managementObject.Properties["RcvLinkSpeed"]?.Value ?? default(ulong)),
		 XmitLinkSpeed = (ulong) (managementObject.Properties["XmitLinkSpeed"]?.Value ?? default(ulong))
                };
        }
    }
}