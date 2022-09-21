using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBus_Queue_Receive
{
    class Program
    {
        private static string connection_string = "Endpoint=sb://niksservice.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=XjRhcW0Z09svEbzsAsB7ECwUGyU+4GfpNQQua3/mg/M=;";
        private static string queue_name = "niks";
        static async Task Main(string[] args)
        {
            ServiceBusClient _client = new ServiceBusClient(connection_string);
            ServiceBusReceiver _receiver = _client.CreateReceiver(queue_name, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

            var _messgaes = _receiver.ReceiveMessagesAsync(2);

            foreach (var _message in _messgaes.Result)
            {
                Console.WriteLine($"The Sequence number is {_message.SequenceNumber}");
                Console.WriteLine(_message.Body);
            }
        }
    }
}