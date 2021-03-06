using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Performance.Formatted.Network.AspNet
{
    /// <summary>
    /// </summary>
    public sealed class Aspnet4030319AspneTv4030319
    {
		public uint ApplicationRestarts { get; private set; }
		public uint ApplicationsRunning { get; private set; }
		public uint AuditFailureEventsRaised { get; private set; }
		public uint AuditSuccessEventsRaised { get; private set; }
		public string Caption { get; private set; }
		public string Description { get; private set; }
		public uint ErrorEventsRaised { get; private set; }
		public ulong FrequencyObject { get; private set; }
		public ulong FrequencyPerfTime { get; private set; }
		public ulong FrequencySys100Ns { get; private set; }
		public uint InfrastructureErrorEventsRaised { get; private set; }
		public string Name { get; private set; }
		public uint RequestErrorEventsRaised { get; private set; }
		public uint RequestExecutionTime { get; private set; }
		public uint RequestsCurrent { get; private set; }
		public uint RequestsDisconnected { get; private set; }
		public uint RequestsInNativeQueue { get; private set; }
		public uint RequestsQueued { get; private set; }
		public uint RequestsRejected { get; private set; }
		public uint RequestWaitTime { get; private set; }
		public uint StateServerSessionsAbandoned { get; private set; }
		public uint StateServerSessionsActive { get; private set; }
		public uint StateServerSessionsTimedOut { get; private set; }
		public uint StateServerSessionsTotal { get; private set; }
		public ulong TimestampObject { get; private set; }
		public ulong TimestampPerfTime { get; private set; }
		public ulong TimestampSys100Ns { get; private set; }
		public uint WorkerProcessesRunning { get; private set; }
		public uint WorkerProcessRestarts { get; private set; }

        public static IEnumerable<Aspnet4030319AspneTv4030319> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<Aspnet4030319AspneTv4030319> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<Aspnet4030319AspneTv4030319> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM Win32_PerfFormattedData_ASPNET4030319_ASPNETv4030319");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new Aspnet4030319AspneTv4030319
                {
                     ApplicationRestarts = (uint) (managementObject.Properties["ApplicationRestarts"]?.Value ?? default(uint)),
		 ApplicationsRunning = (uint) (managementObject.Properties["ApplicationsRunning"]?.Value ?? default(uint)),
		 AuditFailureEventsRaised = (uint) (managementObject.Properties["AuditFailureEventsRaised"]?.Value ?? default(uint)),
		 AuditSuccessEventsRaised = (uint) (managementObject.Properties["AuditSuccessEventsRaised"]?.Value ?? default(uint)),
		 Caption = (string) (managementObject.Properties["Caption"]?.Value),
		 Description = (string) (managementObject.Properties["Description"]?.Value),
		 ErrorEventsRaised = (uint) (managementObject.Properties["ErrorEventsRaised"]?.Value ?? default(uint)),
		 FrequencyObject = (ulong) (managementObject.Properties["Frequency_Object"]?.Value ?? default(ulong)),
		 FrequencyPerfTime = (ulong) (managementObject.Properties["Frequency_PerfTime"]?.Value ?? default(ulong)),
		 FrequencySys100Ns = (ulong) (managementObject.Properties["Frequency_Sys100NS"]?.Value ?? default(ulong)),
		 InfrastructureErrorEventsRaised = (uint) (managementObject.Properties["InfrastructureErrorEventsRaised"]?.Value ?? default(uint)),
		 Name = (string) (managementObject.Properties["Name"]?.Value),
		 RequestErrorEventsRaised = (uint) (managementObject.Properties["RequestErrorEventsRaised"]?.Value ?? default(uint)),
		 RequestExecutionTime = (uint) (managementObject.Properties["RequestExecutionTime"]?.Value ?? default(uint)),
		 RequestsCurrent = (uint) (managementObject.Properties["RequestsCurrent"]?.Value ?? default(uint)),
		 RequestsDisconnected = (uint) (managementObject.Properties["RequestsDisconnected"]?.Value ?? default(uint)),
		 RequestsInNativeQueue = (uint) (managementObject.Properties["RequestsInNativeQueue"]?.Value ?? default(uint)),
		 RequestsQueued = (uint) (managementObject.Properties["RequestsQueued"]?.Value ?? default(uint)),
		 RequestsRejected = (uint) (managementObject.Properties["RequestsRejected"]?.Value ?? default(uint)),
		 RequestWaitTime = (uint) (managementObject.Properties["RequestWaitTime"]?.Value ?? default(uint)),
		 StateServerSessionsAbandoned = (uint) (managementObject.Properties["StateServerSessionsAbandoned"]?.Value ?? default(uint)),
		 StateServerSessionsActive = (uint) (managementObject.Properties["StateServerSessionsActive"]?.Value ?? default(uint)),
		 StateServerSessionsTimedOut = (uint) (managementObject.Properties["StateServerSessionsTimedOut"]?.Value ?? default(uint)),
		 StateServerSessionsTotal = (uint) (managementObject.Properties["StateServerSessionsTotal"]?.Value ?? default(uint)),
		 TimestampObject = (ulong) (managementObject.Properties["Timestamp_Object"]?.Value ?? default(ulong)),
		 TimestampPerfTime = (ulong) (managementObject.Properties["Timestamp_PerfTime"]?.Value ?? default(ulong)),
		 TimestampSys100Ns = (ulong) (managementObject.Properties["Timestamp_Sys100NS"]?.Value ?? default(ulong)),
		 WorkerProcessesRunning = (uint) (managementObject.Properties["WorkerProcessesRunning"]?.Value ?? default(uint)),
		 WorkerProcessRestarts = (uint) (managementObject.Properties["WorkerProcessRestarts"]?.Value ?? default(uint))
                };
        }
    }
}