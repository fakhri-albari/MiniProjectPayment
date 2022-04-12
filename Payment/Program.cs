using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using Payment.EventHub;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
    internal class Program
    {

        private const string ehubNamespaceConnectionString = "Endpoint=sb://onboarding-evennthub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=O3fUmlNPjHkPGLzna3yLYsk5k2IMTn8vPwSqW7XMbOA=";
        private const string createOrderEventHub = "create-order-event";
        private const string finishOrderEventHub = "finish-order-event";
        private const string blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=stronboarding;AccountKey=LpOvJ57eM7avDsfoRE6ev9lhKfvxt5fYRDHkqGZR3TBAdkS1bIwNeHDTn+Gy5TAG1gZCNEbsAmq0mt5gNxXdFA==;EndpointSuffix=core.windows.net";

        static async Task Main()
        {
            CreateOrderReceiver createOrderReceiver = new CreateOrderReceiver(ehubNamespaceConnectionString, blobStorageConnectionString, createOrderEventHub);
            FinishOrderReceiver finishOrderReceiver = new FinishOrderReceiver(ehubNamespaceConnectionString, blobStorageConnectionString, finishOrderEventHub);

            createOrderReceiver.start();
            finishOrderReceiver.start();

            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }
    }
}
