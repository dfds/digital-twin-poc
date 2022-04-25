using Microsoft.Azure.Devices;
using System;
using System.Threading.Tasks;

namespace IoTDeviceManagementConsole
{
    internal class Program
    {
        static RegistryManager _registryManager;

        static Task Main(string[] args)
        {
            var iotHubConnectionString = Environment.GetEnvironmentVariable("AZ_IOT_HUB_CONNECTIONSTRING");
            var targetDeviceId = Environment.GetEnvironmentVariable("AZ_IOT_DEVICE_ID");

            //Connect registry manager to Azure IoT Hub
            _registryManager = RegistryManager.CreateFromConnectionString(iotHubConnectionString);
            
            //Initiate reboot on target device.
            StartReboot(iotHubConnectionString, targetDeviceId).Wait();

            //Query digital twince in Azure for reboot status of device.
            QueryTwinRebootReported(targetDeviceId).Wait();

            Console.WriteLine("IoTDeviceManagementConsole booted!");
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();

            return Task.CompletedTask;
        }

        public static async Task QueryTwinRebootReported(string targetDevice)
        {
            var twin = await _registryManager.GetTwinAsync(targetDevice);

            Console.WriteLine(twin.Properties.Reported.ToJson());
        }

        public static async Task StartReboot(string iotHubConnectionString, string targetDevice)
        {
            var client = ServiceClient.CreateFromConnectionString(iotHubConnectionString);
            var method = new CloudToDeviceMethod("reboot");

            method.ResponseTimeout = TimeSpan.FromSeconds(30);

            CloudToDeviceMethodResult result = await client.InvokeDeviceMethodAsync(targetDevice, method);

            Console.WriteLine("Invoked firmware update on device.");
        }
    }
}
