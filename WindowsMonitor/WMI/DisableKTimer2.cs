using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class DisableKTimer2
    {
		public uint DisableCallback { get; private set; }
		public uint DisableContext { get; private set; }
		public uint Flags { get; private set; }
		public uint Timer { get; private set; }
		public byte TimerFlags { get; private set; }

        public static IEnumerable<DisableKTimer2> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<DisableKTimer2> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<DisableKTimer2> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM DisableKTimer2");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new DisableKTimer2
                {
                     DisableCallback = (uint) (managementObject.Properties["DisableCallback"]?.Value ?? default(uint)),
		 DisableContext = (uint) (managementObject.Properties["DisableContext"]?.Value ?? default(uint)),
		 Flags = (uint) (managementObject.Properties["Flags"]?.Value ?? default(uint)),
		 Timer = (uint) (managementObject.Properties["Timer"]?.Value ?? default(uint)),
		 TimerFlags = (byte) (managementObject.Properties["TimerFlags"]?.Value ?? default(byte))
                };
        }
    }
}