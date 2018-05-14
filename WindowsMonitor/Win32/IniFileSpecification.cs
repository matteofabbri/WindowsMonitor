using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.Win32
{
    /// <summary>
    /// </summary>
    public sealed class IniFileSpecification
    {
		public ushort Action { get; private set; }
		public string Caption { get; private set; }
		public string CheckID { get; private set; }
		public bool CheckMode { get; private set; }
		public uint CheckSum { get; private set; }
		public uint CRC1 { get; private set; }
		public uint CRC2 { get; private set; }
		public DateTime CreateTimeStamp { get; private set; }
		public string Description { get; private set; }
		public ulong FileSize { get; private set; }
		public string IniFile { get; private set; }
		public string key { get; private set; }
		public string MD5Checksum { get; private set; }
		public string Name { get; private set; }
		public string Section { get; private set; }
		public string SoftwareElementID { get; private set; }
		public ushort SoftwareElementState { get; private set; }
		public ushort TargetOperatingSystem { get; private set; }
		public string Value { get; private set; }
		public string Version { get; private set; }

        public static IEnumerable<IniFileSpecification> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<IniFileSpecification> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<IniFileSpecification> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM Win32_IniFileSpecification");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new IniFileSpecification
                {
                     Action = (ushort) (managementObject.Properties["Action"]?.Value ?? default(ushort)),
		 Caption = (string) (managementObject.Properties["Caption"]?.Value),
		 CheckID = (string) (managementObject.Properties["CheckID"]?.Value),
		 CheckMode = (bool) (managementObject.Properties["CheckMode"]?.Value ?? default(bool)),
		 CheckSum = (uint) (managementObject.Properties["CheckSum"]?.Value ?? default(uint)),
		 CRC1 = (uint) (managementObject.Properties["CRC1"]?.Value ?? default(uint)),
		 CRC2 = (uint) (managementObject.Properties["CRC2"]?.Value ?? default(uint)),
		 CreateTimeStamp = ManagementDateTimeConverter.ToDateTime (managementObject.Properties["CreateTimeStamp"]?.Value as string ?? "00010101000000.000000+060"),
		 Description = (string) (managementObject.Properties["Description"]?.Value),
		 FileSize = (ulong) (managementObject.Properties["FileSize"]?.Value ?? default(ulong)),
		 IniFile = (string) (managementObject.Properties["IniFile"]?.Value),
		 key = (string) (managementObject.Properties["key"]?.Value),
		 MD5Checksum = (string) (managementObject.Properties["MD5Checksum"]?.Value),
		 Name = (string) (managementObject.Properties["Name"]?.Value),
		 Section = (string) (managementObject.Properties["Section"]?.Value),
		 SoftwareElementID = (string) (managementObject.Properties["SoftwareElementID"]?.Value),
		 SoftwareElementState = (ushort) (managementObject.Properties["SoftwareElementState"]?.Value ?? default(ushort)),
		 TargetOperatingSystem = (ushort) (managementObject.Properties["TargetOperatingSystem"]?.Value ?? default(ushort)),
		 Value = (string) (managementObject.Properties["Value"]?.Value),
		 Version = (string) (managementObject.Properties["Version"]?.Value)
                };
        }
    }
}