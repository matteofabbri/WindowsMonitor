using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class V1_DriverMajorFunctionCall
    {
		public uint FileObject { get; private set; }
		public uint Flags { get; private set; }
		public uint Irp { get; private set; }
		public uint MajorFunction { get; private set; }
		public uint MinorFunction { get; private set; }
		public uint RoutineAddr { get; private set; }
		public uint UniqMatchId { get; private set; }

        public static IEnumerable<V1_DriverMajorFunctionCall> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<V1_DriverMajorFunctionCall> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<V1_DriverMajorFunctionCall> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM V1_DriverMajorFunctionCall");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new V1_DriverMajorFunctionCall
                {
                     FileObject = (uint) (managementObject.Properties["FileObject"]?.Value ?? default(uint)),
		 Flags = (uint) (managementObject.Properties["Flags"]?.Value ?? default(uint)),
		 Irp = (uint) (managementObject.Properties["Irp"]?.Value ?? default(uint)),
		 MajorFunction = (uint) (managementObject.Properties["MajorFunction"]?.Value ?? default(uint)),
		 MinorFunction = (uint) (managementObject.Properties["MinorFunction"]?.Value ?? default(uint)),
		 RoutineAddr = (uint) (managementObject.Properties["RoutineAddr"]?.Value ?? default(uint)),
		 UniqMatchId = (uint) (managementObject.Properties["UniqMatchId"]?.Value ?? default(uint))
                };
        }
    }
}