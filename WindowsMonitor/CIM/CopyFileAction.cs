using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.CIM
{
    /// <summary>
    /// </summary>
    public sealed class CopyFileAction
    {
		public string ActionID { get; private set; }
		public string Caption { get; private set; }
		public bool DeleteAfterCopy { get; private set; }
		public string Description { get; private set; }
		public string Destination { get; private set; }
		public ushort Direction { get; private set; }
		public string Name { get; private set; }
		public string SoftwareElementID { get; private set; }
		public ushort SoftwareElementState { get; private set; }
		public string Source { get; private set; }
		public ushort TargetOperatingSystem { get; private set; }
		public string Version { get; private set; }

        public static IEnumerable<CopyFileAction> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<CopyFileAction> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<CopyFileAction> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM CIM_CopyFileAction");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new CopyFileAction
                {
                     ActionID = (string) (managementObject.Properties["ActionID"]?.Value ?? default(string)),
		 Caption = (string) (managementObject.Properties["Caption"]?.Value ?? default(string)),
		 DeleteAfterCopy = (bool) (managementObject.Properties["DeleteAfterCopy"]?.Value ?? default(bool)),
		 Description = (string) (managementObject.Properties["Description"]?.Value ?? default(string)),
		 Destination = (string) (managementObject.Properties["Destination"]?.Value ?? default(string)),
		 Direction = (ushort) (managementObject.Properties["Direction"]?.Value ?? default(ushort)),
		 Name = (string) (managementObject.Properties["Name"]?.Value ?? default(string)),
		 SoftwareElementID = (string) (managementObject.Properties["SoftwareElementID"]?.Value ?? default(string)),
		 SoftwareElementState = (ushort) (managementObject.Properties["SoftwareElementState"]?.Value ?? default(ushort)),
		 Source = (string) (managementObject.Properties["Source"]?.Value ?? default(string)),
		 TargetOperatingSystem = (ushort) (managementObject.Properties["TargetOperatingSystem"]?.Value ?? default(ushort)),
		 Version = (string) (managementObject.Properties["Version"]?.Value ?? default(string))
                };
        }
    }
}