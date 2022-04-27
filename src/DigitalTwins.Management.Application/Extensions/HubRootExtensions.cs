using DigitalTwins.Management.Domain.Aggregates;
using Microsoft.Azure.Devices;

namespace DigitalTwins.Management.Application.Extensions
{
    internal static class HubRootExtensions
    {
        internal static RegistryManager GetRegistryManager(this HubRoot hub) 
        { 
            return RegistryManager.CreateFromConnectionString(hub.ConnectionString);
        }

        internal static ServiceClient GetServiceClient(this HubRoot hub)
        {
            return ServiceClient.CreateFromConnectionString(hub.ConnectionString);
        }
    }
}
