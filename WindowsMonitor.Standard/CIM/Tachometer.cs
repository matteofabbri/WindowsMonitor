using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;

namespace WindowsMonitor.CIM
{
    /// <summary>
    /// </summary>
    public sealed class Tachometer
    {
		public int Accuracy { get; private set; }
		public ushort Availability { get; private set; }
		public string Caption { get; private set; }
		public uint ConfigManagerErrorCode { get; private set; }
		public bool ConfigManagerUserConfig { get; private set; }
		public string CreationClassName { get; private set; }
		public int CurrentReading { get; private set; }
		public string Description { get; private set; }
		public string DeviceID { get; private set; }
		public bool ErrorCleared { get; private set; }
		public string ErrorDescription { get; private set; }
		public DateTime InstallDate { get; private set; }
		public bool IsLinear { get; private set; }
		public uint LastErrorCode { get; private set; }
		public int LowerThresholdCritical { get; private set; }
		public int LowerThresholdFatal { get; private set; }
		public int LowerThresholdNonCritical { get; private set; }
		public int MaxReadable { get; private set; }
		public int MinReadable { get; private set; }
		public string Name { get; private set; }
		public int NominalReading { get; private set; }
		public int NormalMax { get; private set; }
		public int NormalMin { get; private set; }
		public string PNPDeviceID { get; private set; }
		public ushort[] PowerManagementCapabilities { get; private set; }
		public bool PowerManagementSupported { get; private set; }
		public uint Resolution { get; private set; }
		public string Status { get; private set; }
		public ushort StatusInfo { get; private set; }
		public string SystemCreationClassName { get; private set; }
		public string SystemName { get; private set; }
		public int Tolerance { get; private set; }
		public int UpperThresholdCritical { get; private set; }
		public int UpperThresholdFatal { get; private set; }
		public int UpperThresholdNonCritical { get; private set; }

        public static IEnumerable<Tachometer> Retrieve(string remote, string username, string password)
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

        public static IEnumerable<Tachometer> Retrieve()
        {
            var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
            return Retrieve(managementScope);
        }

        public static IEnumerable<Tachometer> Retrieve(ManagementScope managementScope)
        {
            var objectQuery = new ObjectQuery("SELECT * FROM CIM_Tachometer");
            var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            var objectCollection = objectSearcher.Get();

            foreach (ManagementObject managementObject in objectCollection)
                yield return new Tachometer
                {
                     Accuracy = (int) (managementObject.Properties["Accuracy"]?.Value ?? default(int)),
		 Availability = (ushort) (managementObject.Properties["Availability"]?.Value ?? default(ushort)),
		 Caption = (string) (managementObject.Properties["Caption"]?.Value ?? default(string)),
		 ConfigManagerErrorCode = (uint) (managementObject.Properties["ConfigManagerErrorCode"]?.Value ?? default(uint)),
		 ConfigManagerUserConfig = (bool) (managementObject.Properties["ConfigManagerUserConfig"]?.Value ?? default(bool)),
		 CreationClassName = (string) (managementObject.Properties["CreationClassName"]?.Value ?? default(string)),
		 CurrentReading = (int) (managementObject.Properties["CurrentReading"]?.Value ?? default(int)),
		 Description = (string) (managementObject.Properties["Description"]?.Value ?? default(string)),
		 DeviceID = (string) (managementObject.Properties["DeviceID"]?.Value ?? default(string)),
		 ErrorCleared = (bool) (managementObject.Properties["ErrorCleared"]?.Value ?? default(bool)),
		 ErrorDescription = (string) (managementObject.Properties["ErrorDescription"]?.Value ?? default(string)),
		 InstallDate = (DateTime) (managementObject.Properties["InstallDate"]?.Value ?? default(DateTime)),
		 IsLinear = (bool) (managementObject.Properties["IsLinear"]?.Value ?? default(bool)),
		 LastErrorCode = (uint) (managementObject.Properties["LastErrorCode"]?.Value ?? default(uint)),
		 LowerThresholdCritical = (int) (managementObject.Properties["LowerThresholdCritical"]?.Value ?? default(int)),
		 LowerThresholdFatal = (int) (managementObject.Properties["LowerThresholdFatal"]?.Value ?? default(int)),
		 LowerThresholdNonCritical = (int) (managementObject.Properties["LowerThresholdNonCritical"]?.Value ?? default(int)),
		 MaxReadable = (int) (managementObject.Properties["MaxReadable"]?.Value ?? default(int)),
		 MinReadable = (int) (managementObject.Properties["MinReadable"]?.Value ?? default(int)),
		 Name = (string) (managementObject.Properties["Name"]?.Value ?? default(string)),
		 NominalReading = (int) (managementObject.Properties["NominalReading"]?.Value ?? default(int)),
		 NormalMax = (int) (managementObject.Properties["NormalMax"]?.Value ?? default(int)),
		 NormalMin = (int) (managementObject.Properties["NormalMin"]?.Value ?? default(int)),
		 PNPDeviceID = (string) (managementObject.Properties["PNPDeviceID"]?.Value ?? default(string)),
		 PowerManagementCapabilities = (ushort[]) (managementObject.Properties["PowerManagementCapabilities"]?.Value ?? new ushort[0]),
		 PowerManagementSupported = (bool) (managementObject.Properties["PowerManagementSupported"]?.Value ?? default(bool)),
		 Resolution = (uint) (managementObject.Properties["Resolution"]?.Value ?? default(uint)),
		 Status = (string) (managementObject.Properties["Status"]?.Value ?? default(string)),
		 StatusInfo = (ushort) (managementObject.Properties["StatusInfo"]?.Value ?? default(ushort)),
		 SystemCreationClassName = (string) (managementObject.Properties["SystemCreationClassName"]?.Value ?? default(string)),
		 SystemName = (string) (managementObject.Properties["SystemName"]?.Value ?? default(string)),
		 Tolerance = (int) (managementObject.Properties["Tolerance"]?.Value ?? default(int)),
		 UpperThresholdCritical = (int) (managementObject.Properties["UpperThresholdCritical"]?.Value ?? default(int)),
		 UpperThresholdFatal = (int) (managementObject.Properties["UpperThresholdFatal"]?.Value ?? default(int)),
		 UpperThresholdNonCritical = (int) (managementObject.Properties["UpperThresholdNonCritical"]?.Value ?? default(int))
                };
        }
    }
}