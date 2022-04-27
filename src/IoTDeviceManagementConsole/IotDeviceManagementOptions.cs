using System.ComponentModel.DataAnnotations;

namespace IoTDeviceManagementConsole
{
    public class IotDeviceManagementOptions
    {
        [Required]
        public IoTHubOptions IoTHub { get; set; }

        public IoTDeviceOptions Devices { get; set; }
    }

    public class IoTHubOptions
    {
        public string ConnectionString { get; set; }
    }

    public class IoTDeviceOptions
    {
        public string DeviceId { get; set; }

        public string ConnectionString { get; set; }
    }
}
