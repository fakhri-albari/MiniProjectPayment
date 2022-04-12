using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Payment.EventHub
{
    public class FinishOrderReceiver
    {
        private BlobContainerClient storage;
        private EventProcessorClient processor;

        public FinishOrderReceiver(string namespaceConnection, string storageConnection, string eventHubName)
        {
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
            storage = new BlobContainerClient(storageConnection, eventHubName);
            processor = new EventProcessorClient(storage, consumerGroup, namespaceConnection, eventHubName);
        }

        public async void start()
        {
            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;
            await processor.StartProcessingAsync();
        }

        async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            Console.WriteLine(Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);

            //To Do
            //1. Convert message to Payment model
            //2. Send Payment to DB using PaymentServices
        }

        Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
