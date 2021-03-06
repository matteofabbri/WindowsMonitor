using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Performance.Formatted.Network
{
    /// <summary>
    /// </summary>
    public sealed class CountersDns64Global
    {
		public ulong AaaAqueriesFailed { get; private set; }
		public ulong AaaAqueriesSuccessful { get; private set; }
		public ulong AaaaSynthesizedrecords { get; private set; }
		public string Caption { get; private set; }
		public string Description { get; private set; }
		public ulong FrequencyObject { get; private set; }
		public ulong FrequencyPerfTime { get; private set; }
		public ulong FrequencySys100Ns { get; private set; }
		public ulong Ip6ArpAqueriesMatched { get; private set; }
		public string Name { get; private set; }
		public ulong OtherqueriesFailed { get; private set; }
		public ulong OtherqueriesSuccessful { get; private set; }
		public ulong TimestampObject { get; private set; }
		public ulong TimestampPerfTime { get; private set; }
		public ulong TimestampSys100Ns { get; private set; }

        public static IEnumerable<CountersDns64Global> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<CountersDns64Global> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<CountersDns64Global> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM Win32_PerfFormattedData_Counters_DNS64Global");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new CountersDns64Global
                {
                     AaaAqueriesFailed = (ulong) (managementObject.Properties["AAAAqueriesFailed"]?.Value ?? default(ulong)),
		 AaaAqueriesSuccessful = (ulong) (managementObject.Properties["AAAAqueriesSuccessful"]?.Value ?? default(ulong)),
		 AaaaSynthesizedrecords = (ulong) (managementObject.Properties["AAAASynthesizedrecords"]?.Value ?? default(ulong)),
		 Caption = (string) (managementObject.Properties["Caption"]?.Value),
		 Description = (string) (managementObject.Properties["Description"]?.Value),
		 FrequencyObject = (ulong) (managementObject.Properties["Frequency_Object"]?.Value ?? default(ulong)),
		 FrequencyPerfTime = (ulong) (managementObject.Properties["Frequency_PerfTime"]?.Value ?? default(ulong)),
		 FrequencySys100Ns = (ulong) (managementObject.Properties["Frequency_Sys100NS"]?.Value ?? default(ulong)),
		 Ip6ArpAqueriesMatched = (ulong) (managementObject.Properties["IP6ARPAqueriesMatched"]?.Value ?? default(ulong)),
		 Name = (string) (managementObject.Properties["Name"]?.Value),
		 OtherqueriesFailed = (ulong) (managementObject.Properties["OtherqueriesFailed"]?.Value ?? default(ulong)),
		 OtherqueriesSuccessful = (ulong) (managementObject.Properties["OtherqueriesSuccessful"]?.Value ?? default(ulong)),
		 TimestampObject = (ulong) (managementObject.Properties["Timestamp_Object"]?.Value ?? default(ulong)),
		 TimestampPerfTime = (ulong) (managementObject.Properties["Timestamp_PerfTime"]?.Value ?? default(ulong)),
		 TimestampSys100Ns = (ulong) (managementObject.Properties["Timestamp_Sys100NS"]?.Value ?? default(ulong))
                };
        }
    }
}