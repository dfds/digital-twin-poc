using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System;
using System.Text;
using System.Threading.Tasks;

namespace IoTDeviceManagementConsole
{
    public class ApplicationFacade
    {
        static RegistryManager _registryManager;

        //static string DeviceConnectionString = "HostName=DigitalTwinsHub.azure-devices.net;DeviceId=ManualTestDevice;SharedAccessKey=pRsVzx5GkSqMD8aPJFT077YrSCw74NyAlBQJhuVZyhI=";
        static string DeviceConnectionString = "HostName=DigitalTwinsHub.azure-devices.net;DeviceId=IoTDevice01;SharedAccessKey=w+yWyXDFkL2l9r3D0Y5eMif6QNtjQX5P0NfmVhcRh4M=";

        static DeviceClient Client = null;

        public Task Init(string[] args)
        {
            var iotHubConnectionString = Environment.GetEnvironmentVariable("AZ_IOT_HUB_CONNECTIONSTRING") ?? "HostName=DigitalTwinsHub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=CFvgjUnyQ6sJvD/cv5JVmXy+EJ7Ay2hGGqbMXA5nPiw=";
            var targetDeviceId = Environment.GetEnvironmentVariable("AZ_IOT_DEVICE_ID") ?? "IoTDevice01";

            //Connect registry manager to Azure IoT Hub
            _registryManager = RegistryManager.CreateFromConnectionString(iotHubConnectionString);

            Client = DeviceClient.CreateFromConnectionString(DeviceConnectionString, Microsoft.Azure.Devices.Client.TransportType.Mqtt);

            // setup callback for "reboot" method
            Client.SetMethodHandlerAsync("reboot", onReboot, null).Wait();

            //Initiate reboot on target device.
            StartReboot(iotHubConnectionString, targetDeviceId).Wait();

            //Query digital twince in Azure for reboot status of device.
            QueryTwinRebootReported(targetDeviceId).Wait();

            Console.WriteLine("Reboot successful.");
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();

            Console.WriteLine("Exiting.");

            // as a good practice, remove the "reboot" handler
            Client.SetMethodHandlerAsync("reboot", null, null).Wait();
            Client.CloseAsync().Wait();

            return Task.CompletedTask;
        }

        private static async Task QueryTwinRebootReported(string targetDevice)
        {
            var twin = await _registryManager.GetTwinAsync(targetDevice);

            Console.WriteLine(twin.Properties.Reported.ToJson());
        }

        private static async Task StartReboot(string iotHubConnectionString, string targetDevice)
        {
            var client = ServiceClient.CreateFromConnectionString(iotHubConnectionString);
            var method = new CloudToDeviceMethod("reboot");

            method.ResponseTimeout = TimeSpan.FromSeconds(30);

            CloudToDeviceMethodResult result = await client.InvokeDeviceMethodAsync(targetDevice, method);

            Console.WriteLine("Invoked firmware update on device.");
        }

        private static Task<MethodResponse> onReboot(MethodRequest methodRequest, object userContext)
        {
            // In a production device, you would trigger a reboot 
            //   scheduled to start after this method returns.
            // For this sample, we simulate the reboot by writing to the console
            //   and updating the reported properties.
            try
            {
                Console.WriteLine("Rebooting!");

                // Update device twin with reboot time. 
                TwinCollection reportedProperties, reboot, lastReboot;
                lastReboot = new TwinCollection();
                reboot = new TwinCollection();
                reportedProperties = new TwinCollection();
                lastReboot["lastReboot"] = DateTime.Now;
                reboot["reboot"] = lastReboot;
                reportedProperties["iothubDM"] = reboot;
                Client.UpdateReportedPropertiesAsync(reportedProperties).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Error in sample: {0}", ex.Message);
            }

            string result = @"{""result"":""Reboot started.""}";
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
        }
    }
}
