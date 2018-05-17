using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class MSNdis_80211_ReceivedSignalStrengthEventTrigger
    {
		public bool Active { get; private set; }
		public string InstanceName { get; private set; }
		public int Ndis80211ReceivedSignalStrengthTrigger { get; private set; }

        public static IEnumerable<MSNdis_80211_ReceivedSignalStrengthEventTrigger> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<MSNdis_80211_ReceivedSignalStrengthEventTrigger> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<MSNdis_80211_ReceivedSignalStrengthEventTrigger> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSNdis_80211_ReceivedSignalStrengthEventTrigger");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new MSNdis_80211_ReceivedSignalStrengthEventTrigger
                {
                     Active = (bool) (managementObject.Properties["Active"]?.Value ?? default(bool)),
		 InstanceName = (string) (managementObject.Properties["InstanceName"]?.Value ?? default(string)),
		 Ndis80211ReceivedSignalStrengthTrigger = (int) (managementObject.Properties["Ndis80211ReceivedSignalStrengthTrigger"]?.Value ?? default(int))
                };
        }
    }
}