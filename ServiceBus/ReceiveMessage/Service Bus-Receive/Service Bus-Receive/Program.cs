using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBus_Queue_Receive
{
    class Program
    {
        private static string connection_string = "Endpoint=sb://niksservice.servicebus.windows.net/;SharedAccessKeyName=Receive;SharedAccessKey=hdnCRr+p22GPtH5QAmxxB0g7WLEOrpl92TpCNtsrs2Y=;EntityPath=topica";
        //private static string queue_name = "niks";
        private static string topic_name = "topica";
        private static string Subscription_name = "SubscriptionA";

        static async Task Main(string[] args)
        {
            ServiceBusClient _client = new ServiceBusClient(connection_string);
            ServiceBusReceiver _receiver = _client.CreateReceiver(topic_name,Subscription_name, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

            //ServiceBusReceiver _receiver = _client.CreateReceiver(queue_name, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });

            var _messgaes = _receiver.ReceiveMessagesAsync(200000);

            foreach (var _message in _messgaes.Result)
            {
                Console.WriteLine($"The Sequence number is {_message.SequenceNumber}");
                Console.WriteLine(_message.Body);
            }
        }
    }
}