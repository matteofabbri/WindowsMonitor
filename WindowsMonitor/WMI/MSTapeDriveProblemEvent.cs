using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class MSTapeDriveProblemEvent
    {
		public bool Active { get; private set; }
		public uint DriveProblemType { get; private set; }
		public string InstanceName { get; private set; }
		public byte[] SECURITY_DESCRIPTOR { get; private set; }
		public byte[] TapeData { get; private set; }
		public ulong TIME_CREATED { get; private set; }

        public static IEnumerable<MSTapeDriveProblemEvent> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<MSTapeDriveProblemEvent> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<MSTapeDriveProblemEvent> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM MSTapeDriveProblemEvent");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new MSTapeDriveProblemEvent
                {
                     Active = (bool) (managementObject.Properties["Active"]?.Value ?? default(bool)),
		 DriveProblemType = (uint) (managementObject.Properties["DriveProblemType"]?.Value ?? default(uint)),
		 InstanceName = (string) (managementObject.Properties["InstanceName"]?.Value ?? default(string)),
		 SECURITY_DESCRIPTOR = (byte[]) (managementObject.Properties["SECURITY_DESCRIPTOR"]?.Value ?? new byte[0]),
		 TapeData = (byte[]) (managementObject.Properties["TapeData"]?.Value ?? new byte[0]),
		 TIME_CREATED = (ulong) (managementObject.Properties["TIME_CREATED"]?.Value ?? default(ulong))
                };
        }
    }
}