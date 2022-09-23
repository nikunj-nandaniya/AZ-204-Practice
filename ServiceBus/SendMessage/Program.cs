// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;
using ServiceBus;

namespace AzureServiceQueue
{
    class Program
    {
        private static string connection_string = "Endpoint=sb://niksservice.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=2Id1BEm3jMCSqMt15L1UITnYzXdTyWbB+1sLiXhYH6M=;EntityPath=topica";
        //private static string queue_name = "niks";
        private static string topic_name = "topica";


        static void Main(string[] args)
        {
            List<Order> _orders = new List<Order>()
            {
                new Order() {OrderID="1",Quantity=10,UnitPrice=9.99m},
                new Order() {OrderID="2",Quantity=15,UnitPrice=10.99m },
                new Order() {OrderID="3",Quantity=20,UnitPrice=11.99m},
                new Order() {OrderID="4",Quantity=25,UnitPrice=12.99m},
                new Order() {OrderID="5",Quantity=30,UnitPrice=13.99m }
            };

            ServiceBusClient _client = new ServiceBusClient(connection_string);
            //ServiceBusSender _sender = _client.CreateSender(queue_name);
            ServiceBusSender _sender = _client.CreateSender(topic_name);

            foreach (Order _order in _orders)
            {
                ServiceBusMessage _message = new ServiceBusMessage(_order.ToString());
                _message.ContentType = "application/json";

                if (Convert.ToInt32(_order.OrderID) > 3)
                {
                   _message.ApplicationProperties.Add("Subscription", "B");
                }
                else
                {
                    _message.ApplicationProperties.Add("Subscription", "A");
                }
                
                _sender.SendMessageAsync(_message).GetAwaiter().GetResult();

                Console.WriteLine(_message.Body + "Sent");
            }
        }
    }
}