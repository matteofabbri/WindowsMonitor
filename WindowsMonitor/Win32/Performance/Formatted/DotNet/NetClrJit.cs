using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Win32.Performance.Formatted.DotNet
{
    /// <summary>
    /// </summary>
    public sealed class NetClrJit
    {
		public string Caption { get; private set; }
		public string Description { get; private set; }
		public ulong FrequencyObject { get; private set; }
		public ulong FrequencyPerfTime { get; private set; }
		public ulong FrequencySys100Ns { get; private set; }
		public uint IlBytesJittedPersec { get; private set; }
		public string Name { get; private set; }
		public uint NumberofIlBytesJitted { get; private set; }
		public uint NumberofMethodsJitted { get; private set; }
		public uint PercentTimeinJit { get; private set; }
		public uint StandardJitFailures { get; private set; }
		public ulong TimestampObject { get; private set; }
		public ulong TimestampPerfTime { get; private set; }
		public ulong TimestampSys100Ns { get; private set; }
		public uint TotalNumberofIlBytesJitted { get; private set; }

        public static IEnumerable<NetClrJit> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<NetClrJit> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<NetClrJit> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM Win32_PerfFormattedData_NETFramework_NETCLRJit");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new NetClrJit
                {
                     Caption = (string) (managementObject.Properties["Caption"]?.Value ?? default(string)),
		 Description = (string) (managementObject.Properties["Description"]?.Value ?? default(string)),
		 FrequencyObject = (ulong) (managementObject.Properties["Frequency_Object"]?.Value ?? default(ulong)),
		 FrequencyPerfTime = (ulong) (managementObject.Properties["Frequency_PerfTime"]?.Value ?? default(ulong)),
		 FrequencySys100Ns = (ulong) (managementObject.Properties["Frequency_Sys100NS"]?.Value ?? default(ulong)),
		 IlBytesJittedPersec = (uint) (managementObject.Properties["ILBytesJittedPersec"]?.Value ?? default(uint)),
		 Name = (string) (managementObject.Properties["Name"]?.Value ?? default(string)),
		 NumberofIlBytesJitted = (uint) (managementObject.Properties["NumberofILBytesJitted"]?.Value ?? default(uint)),
		 NumberofMethodsJitted = (uint) (managementObject.Properties["NumberofMethodsJitted"]?.Value ?? default(uint)),
		 PercentTimeinJit = (uint) (managementObject.Properties["PercentTimeinJit"]?.Value ?? default(uint)),
		 StandardJitFailures = (uint) (managementObject.Properties["StandardJitFailures"]?.Value ?? default(uint)),
		 TimestampObject = (ulong) (managementObject.Properties["Timestamp_Object"]?.Value ?? default(ulong)),
		 TimestampPerfTime = (ulong) (managementObject.Properties["Timestamp_PerfTime"]?.Value ?? default(ulong)),
		 TimestampSys100Ns = (ulong) (managementObject.Properties["Timestamp_Sys100NS"]?.Value ?? default(ulong)),
		 TotalNumberofIlBytesJitted = (uint) (managementObject.Properties["TotalNumberofILBytesJitted"]?.Value ?? default(uint))
                };
        }
    }
}