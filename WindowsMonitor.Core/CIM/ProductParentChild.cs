using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.CIM
{
    /// <summary>
    /// </summary>
    public sealed class ProductParentChild
    {
		public short Child { get; private set; }
		public short Parent { get; private set; }

        public static IEnumerable<ProductParentChild> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<ProductParentChild> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<ProductParentChild> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM CIM_ProductParentChild");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new ProductParentChild
                {
                     Child = (short) (managementObject.Properties["Child"]?.Value ?? default(short)),
		 Parent = (short) (managementObject.Properties["Parent"]?.Value ?? default(short))
                };
        }
    }
}