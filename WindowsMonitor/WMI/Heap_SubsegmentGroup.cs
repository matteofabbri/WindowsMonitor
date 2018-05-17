using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.WMI
{
    /// <summary>
    /// </summary>
    public sealed class Heap_SubsegmentGroup
    {
		public dynamic BlockSize { get; private set; }
		public uint HeapHandle { get; private set; }
		public uint SubSegment { get; private set; }
		public dynamic SubSegmentSize { get; private set; }

        public static IEnumerable<Heap_SubsegmentGroup> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<Heap_SubsegmentGroup> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\wmi"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<Heap_SubsegmentGroup> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM Heap_SubsegmentGroup");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new Heap_SubsegmentGroup
                {
                     BlockSize = (dynamic) (managementObject.Properties["BlockSize"]?.Value ?? default(dynamic)),
		 HeapHandle = (uint) (managementObject.Properties["HeapHandle"]?.Value ?? default(uint)),
		 SubSegment = (uint) (managementObject.Properties["SubSegment"]?.Value ?? default(uint)),
		 SubSegmentSize = (dynamic) (managementObject.Properties["SubSegmentSize"]?.Value ?? default(dynamic))
                };
        }
    }
}