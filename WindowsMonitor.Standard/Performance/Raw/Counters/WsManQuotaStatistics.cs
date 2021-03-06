using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Performance.Raw.Counters
{
    /// <summary>
    /// </summary>
    public sealed class WsManQuotaStatistics
    {
		public uint ActiveOperations { get; private set; }
		public uint ActiveShells { get; private set; }
		public uint ActiveUsers { get; private set; }
		public string Caption { get; private set; }
		public string Description { get; private set; }
		public ulong FrequencyObject { get; private set; }
		public ulong FrequencyPerfTime { get; private set; }
		public ulong FrequencySys100Ns { get; private set; }
		public string Name { get; private set; }
		public uint ProcessId { get; private set; }
		public uint SystemQuotaViolationsPerSecond { get; private set; }
		public ulong TimestampObject { get; private set; }
		public ulong TimestampPerfTime { get; private set; }
		public ulong TimestampSys100Ns { get; private set; }
		public uint TotalRequestsPerSecond { get; private set; }
		public uint UserQuotaViolationsPerSecond { get; private set; }

        public static IEnumerable<WsManQuotaStatistics> Retrieve(string remote, string username, string password)
        {
            var options = new ConnectionOptions
            {
                Impersonation = ImpersonationLevel.Impersonate,
                Username = username,
                Password = password
            };

            var managementScope = new ManagementScope(new ManagementPath($"\\\\{remote}\\root\\cimv2"), options);
            managementScope.Connect();

            return Retrieve(managementScope);
        }

        public static IEnumerable<WsManQuotaStatistics> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<WsManQuotaStatistics> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM Win32_PerfRawData_Counters_WSManQuotaStatistics");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new WsManQuotaStatistics
                {
                     ActiveOperations = (uint) (managementObject.Properties["ActiveOperations"]?.Value ?? default(uint)),
		 ActiveShells = (uint) (managementObject.Properties["ActiveShells"]?.Value ?? default(uint)),
		 ActiveUsers = (uint) (managementObject.Properties["ActiveUsers"]?.Value ?? default(uint)),
		 Caption = (string) (managementObject.Properties["Caption"]?.Value),
		 Description = (string) (managementObject.Properties["Description"]?.Value),
		 FrequencyObject = (ulong) (managementObject.Properties["Frequency_Object"]?.Value ?? default(ulong)),
		 FrequencyPerfTime = (ulong) (managementObject.Properties["Frequency_PerfTime"]?.Value ?? default(ulong)),
		 FrequencySys100Ns = (ulong) (managementObject.Properties["Frequency_Sys100NS"]?.Value ?? default(ulong)),
		 Name = (string) (managementObject.Properties["Name"]?.Value),
		 ProcessId = (uint) (managementObject.Properties["ProcessID"]?.Value ?? default(uint)),
		 SystemQuotaViolationsPerSecond = (uint) (managementObject.Properties["SystemQuotaViolationsPerSecond"]?.Value ?? default(uint)),
		 TimestampObject = (ulong) (managementObject.Properties["Timestamp_Object"]?.Value ?? default(ulong)),
		 TimestampPerfTime = (ulong) (managementObject.Properties["Timestamp_PerfTime"]?.Value ?? default(ulong)),
		 TimestampSys100Ns = (ulong) (managementObject.Properties["Timestamp_Sys100NS"]?.Value ?? default(ulong)),
		 TotalRequestsPerSecond = (uint) (managementObject.Properties["TotalRequestsPerSecond"]?.Value ?? default(uint)),
		 UserQuotaViolationsPerSecond = (uint) (managementObject.Properties["UserQuotaViolationsPerSecond"]?.Value ?? default(uint))
                };
        }
    }
}