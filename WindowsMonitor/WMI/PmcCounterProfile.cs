using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class PmcCounterProfile
    {
		public uint Flags { get; private set; }
		public uint InstructionPointer { get; private set; }
		public ushort ProfileSource { get; private set; }
		public ushort Reserved { get; private set; }
		public uint ThreadId { get; private set; }

        public static IEnumerable<PmcCounterProfile> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<PmcCounterProfile> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<PmcCounterProfile> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM PmcCounterProfile");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new PmcCounterProfile
                {
                     Flags = (uint) (managementObject.Properties["Flags"]?.Value ?? default(uint)),
		 InstructionPointer = (uint) (managementObject.Properties["InstructionPointer"]?.Value ?? default(uint)),
		 ProfileSource = (ushort) (managementObject.Properties["ProfileSource"]?.Value ?? default(ushort)),
		 Reserved = (ushort) (managementObject.Properties["Reserved"]?.Value ?? default(ushort)),
		 ThreadId = (uint) (managementObject.Properties["ThreadId"]?.Value ?? default(uint))
                };
        }
    }
}