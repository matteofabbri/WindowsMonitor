using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.CIM
{
    /// <summary>
    /// </summary>
    public sealed class Rack
    {
		public bool AudibleAlarm { get; private set; }
		public string BreachDescription { get; private set; }
		public string CableManagementStrategy { get; private set; }
		public string Caption { get; private set; }
		public string CountryDesignation { get; private set; }
		public string CreationClassName { get; private set; }
		public float Depth { get; private set; }
		public string Description { get; private set; }
		public float Height { get; private set; }
		public bool HotSwappable { get; private set; }
		public DateTime InstallDate { get; private set; }
		public bool LockPresent { get; private set; }
		public string Manufacturer { get; private set; }
		public string Model { get; private set; }
		public string Name { get; private set; }
		public string OtherIdentifyingInfo { get; private set; }
		public string PartNumber { get; private set; }
		public bool PoweredOn { get; private set; }
		public bool Removable { get; private set; }
		public bool Replaceable { get; private set; }
		public ushort SecurityBreach { get; private set; }
		public string SerialNumber { get; private set; }
		public string[] ServiceDescriptions { get; private set; }
		public ushort[] ServicePhilosophy { get; private set; }
		public string Sku { get; private set; }
		public string Status { get; private set; }
		public string Tag { get; private set; }
		public ushort TypeOfRack { get; private set; }
		public string Version { get; private set; }
		public bool VisibleAlarm { get; private set; }
		public float Weight { get; private set; }
		public float Width { get; private set; }

        public static IEnumerable<Rack> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<Rack> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<Rack> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM CIM_Rack");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new Rack
                {
                     AudibleAlarm = (bool) (managementObject.Properties["AudibleAlarm"]?.Value ?? default(bool)),
		 BreachDescription = (string) (managementObject.Properties["BreachDescription"]?.Value),
		 CableManagementStrategy = (string) (managementObject.Properties["CableManagementStrategy"]?.Value),
		 Caption = (string) (managementObject.Properties["Caption"]?.Value),
		 CountryDesignation = (string) (managementObject.Properties["CountryDesignation"]?.Value),
		 CreationClassName = (string) (managementObject.Properties["CreationClassName"]?.Value),
		 Depth = (float) (managementObject.Properties["Depth"]?.Value ?? default(float)),
		 Description = (string) (managementObject.Properties["Description"]?.Value),
		 Height = (float) (managementObject.Properties["Height"]?.Value ?? default(float)),
		 HotSwappable = (bool) (managementObject.Properties["HotSwappable"]?.Value ?? default(bool)),
		 InstallDate = ManagementDateTimeConverter.ToDateTime (managementObject.Properties["InstallDate"]?.Value as string ?? "00010102000000.000000+060"),
		 LockPresent = (bool) (managementObject.Properties["LockPresent"]?.Value ?? default(bool)),
		 Manufacturer = (string) (managementObject.Properties["Manufacturer"]?.Value),
		 Model = (string) (managementObject.Properties["Model"]?.Value),
		 Name = (string) (managementObject.Properties["Name"]?.Value),
		 OtherIdentifyingInfo = (string) (managementObject.Properties["OtherIdentifyingInfo"]?.Value),
		 PartNumber = (string) (managementObject.Properties["PartNumber"]?.Value),
		 PoweredOn = (bool) (managementObject.Properties["PoweredOn"]?.Value ?? default(bool)),
		 Removable = (bool) (managementObject.Properties["Removable"]?.Value ?? default(bool)),
		 Replaceable = (bool) (managementObject.Properties["Replaceable"]?.Value ?? default(bool)),
		 SecurityBreach = (ushort) (managementObject.Properties["SecurityBreach"]?.Value ?? default(ushort)),
		 SerialNumber = (string) (managementObject.Properties["SerialNumber"]?.Value),
		 ServiceDescriptions = (string[]) (managementObject.Properties["ServiceDescriptions"]?.Value ?? new string[0]),
		 ServicePhilosophy = (ushort[]) (managementObject.Properties["ServicePhilosophy"]?.Value ?? new ushort[0]),
		 Sku = (string) (managementObject.Properties["SKU"]?.Value),
		 Status = (string) (managementObject.Properties["Status"]?.Value),
		 Tag = (string) (managementObject.Properties["Tag"]?.Value),
		 TypeOfRack = (ushort) (managementObject.Properties["TypeOfRack"]?.Value ?? default(ushort)),
		 Version = (string) (managementObject.Properties["Version"]?.Value),
		 VisibleAlarm = (bool) (managementObject.Properties["VisibleAlarm"]?.Value ?? default(bool)),
		 Weight = (float) (managementObject.Properties["Weight"]?.Value ?? default(float)),
		 Width = (float) (managementObject.Properties["Width"]?.Value ?? default(float))
                };
        }
    }
}