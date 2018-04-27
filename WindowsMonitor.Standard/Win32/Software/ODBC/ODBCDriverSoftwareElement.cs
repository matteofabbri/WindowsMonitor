using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Win32
{
    /// <summary>
    /// </summary>
    public sealed class ODBCDriverSoftwareElement
    {
		public short Check { get; private set; }
		public short Element { get; private set; }
		public ushort Phase { get; private set; }

        public static IEnumerable<ODBCDriverSoftwareElement> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<ODBCDriverSoftwareElement> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<ODBCDriverSoftwareElement> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM Win32_ODBCDriverSoftwareElement");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new ODBCDriverSoftwareElement
                {
                     Check = (short) (managementObject.Properties["Check"]?.Value ?? default(short)),
		 Element = (short) (managementObject.Properties["Element"]?.Value ?? default(short)),
		 Phase = (ushort) (managementObject.Properties["Phase"]?.Value ?? default(ushort))
                };
        }
    }
}