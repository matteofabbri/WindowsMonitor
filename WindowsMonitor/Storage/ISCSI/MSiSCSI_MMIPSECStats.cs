using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Storage.ISCSI
{
    /// <summary>
    /// </summary>
    public sealed class MSiSCSI_MMIPSECStats
    {
		public ulong AcquireFailures { get; private set; }
		public ulong AcquireHeapSize { get; private set; }
		public bool Active { get; private set; }
		public ulong ActiveAcquire { get; private set; }
		public ulong ActiveReceive { get; private set; }
		public ulong AuthenticationFailures { get; private set; }
		public string Caption { get; private set; }
		public ulong ConnectionListSize { get; private set; }
		public string Description { get; private set; }
		public ulong Frequency_Object { get; private set; }
		public ulong Frequency_PerfTime { get; private set; }
		public ulong Frequency_Sys100NS { get; private set; }
		public ulong GetSPIFailures { get; private set; }
		public string InstanceName { get; private set; }
		public ulong InvalidCookiesReceived { get; private set; }
		public ulong InvalidPackets { get; private set; }
		public ulong KeyAdditionFailures { get; private set; }
		public ulong KeyAdditions { get; private set; }
		public ulong KeyUpdateFailures { get; private set; }
		public ulong KeyUpdates { get; private set; }
		public string Name { get; private set; }
		public ulong NegotiationFailures { get; private set; }
		public ulong OakleyMainMode { get; private set; }
		public ulong OakleyQuickMode { get; private set; }
		public ulong ReceiveFailures { get; private set; }
		public ulong ReceiveHeapSize { get; private set; }
		public ulong SendFailures { get; private set; }
		public ulong SoftAssociations { get; private set; }
		public ulong Timestamp_Object { get; private set; }
		public ulong Timestamp_PerfTime { get; private set; }
		public ulong Timestamp_Sys100NS { get; private set; }
		public ulong TotalGetSPI { get; private set; }

        public static IEnumerable<MSiSCSI_MMIPSECStats> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<MSiSCSI_MMIPSECStats> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<MSiSCSI_MMIPSECStats> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSiSCSI_MMIPSECStats");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new MSiSCSI_MMIPSECStats
                {
                     AcquireFailures = (ulong) (managementObject.Properties["AcquireFailures"]?.Value ?? default(ulong)),
		 AcquireHeapSize = (ulong) (managementObject.Properties["AcquireHeapSize"]?.Value ?? default(ulong)),
		 Active = (bool) (managementObject.Properties["Active"]?.Value ?? default(bool)),
		 ActiveAcquire = (ulong) (managementObject.Properties["ActiveAcquire"]?.Value ?? default(ulong)),
		 ActiveReceive = (ulong) (managementObject.Properties["ActiveReceive"]?.Value ?? default(ulong)),
		 AuthenticationFailures = (ulong) (managementObject.Properties["AuthenticationFailures"]?.Value ?? default(ulong)),
		 Caption = (string) (managementObject.Properties["Caption"]?.Value ?? default(string)),
		 ConnectionListSize = (ulong) (managementObject.Properties["ConnectionListSize"]?.Value ?? default(ulong)),
		 Description = (string) (managementObject.Properties["Description"]?.Value ?? default(string)),
		 Frequency_Object = (ulong) (managementObject.Properties["Frequency_Object"]?.Value ?? default(ulong)),
		 Frequency_PerfTime = (ulong) (managementObject.Properties["Frequency_PerfTime"]?.Value ?? default(ulong)),
		 Frequency_Sys100NS = (ulong) (managementObject.Properties["Frequency_Sys100NS"]?.Value ?? default(ulong)),
		 GetSPIFailures = (ulong) (managementObject.Properties["GetSPIFailures"]?.Value ?? default(ulong)),
		 InstanceName = (string) (managementObject.Properties["InstanceName"]?.Value ?? default(string)),
		 InvalidCookiesReceived = (ulong) (managementObject.Properties["InvalidCookiesReceived"]?.Value ?? default(ulong)),
		 InvalidPackets = (ulong) (managementObject.Properties["InvalidPackets"]?.Value ?? default(ulong)),
		 KeyAdditionFailures = (ulong) (managementObject.Properties["KeyAdditionFailures"]?.Value ?? default(ulong)),
		 KeyAdditions = (ulong) (managementObject.Properties["KeyAdditions"]?.Value ?? default(ulong)),
		 KeyUpdateFailures = (ulong) (managementObject.Properties["KeyUpdateFailures"]?.Value ?? default(ulong)),
		 KeyUpdates = (ulong) (managementObject.Properties["KeyUpdates"]?.Value ?? default(ulong)),
		 Name = (string) (managementObject.Properties["Name"]?.Value ?? default(string)),
		 NegotiationFailures = (ulong) (managementObject.Properties["NegotiationFailures"]?.Value ?? default(ulong)),
		 OakleyMainMode = (ulong) (managementObject.Properties["OakleyMainMode"]?.Value ?? default(ulong)),
		 OakleyQuickMode = (ulong) (managementObject.Properties["OakleyQuickMode"]?.Value ?? default(ulong)),
		 ReceiveFailures = (ulong) (managementObject.Properties["ReceiveFailures"]?.Value ?? default(ulong)),
		 ReceiveHeapSize = (ulong) (managementObject.Properties["ReceiveHeapSize"]?.Value ?? default(ulong)),
		 SendFailures = (ulong) (managementObject.Properties["SendFailures"]?.Value ?? default(ulong)),
		 SoftAssociations = (ulong) (managementObject.Properties["SoftAssociations"]?.Value ?? default(ulong)),
		 Timestamp_Object = (ulong) (managementObject.Properties["Timestamp_Object"]?.Value ?? default(ulong)),
		 Timestamp_PerfTime = (ulong) (managementObject.Properties["Timestamp_PerfTime"]?.Value ?? default(ulong)),
		 Timestamp_Sys100NS = (ulong) (managementObject.Properties["Timestamp_Sys100NS"]?.Value ?? default(ulong)),
		 TotalGetSPI = (ulong) (managementObject.Properties["TotalGetSPI"]?.Value ?? default(ulong))
                };
        }
    }
}