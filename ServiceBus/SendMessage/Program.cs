// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;
using ServiceBus;

namespace AzureServiceQueue
{
    class Program
    {
        private static string connection_string = "Endpoint=sb://niksservice.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=XjRhcW0Z09svEbzsAsB7ECwUGyU+4GfpNQQua3/mg/M=;";
        private static string queue_name = "niksservice";

        static void Main(string[] args)
        {
            List<Order> _orders = new List<Order>()
            {
                new Order() {OrderID="O1",Quantity=10,UnitPrice=9.99m},
                new Order() {OrderID="O2",Quantity=15,UnitPrice=10.99m },
                new Order() {OrderID="O3",Quantity=20,UnitPrice=11.99m},
                new Order() {OrderID="O4",Quantity=25,UnitPrice=12.99m},
                new Order() {OrderID="O5",Quantity=30,UnitPrice=13.99m }
            };

            ServiceBusClient _client = new ServiceBusClient(connection_string);
            ServiceBusSender _sender = _client.CreateSender(queue_name);

            foreach (Order _order in _orders)
            {
                ServiceBusMessage _message = new ServiceBusMessage(_order.ToString());
                _message.ContentType = "application/json";
                _sender.SendMessageAsync(_message).GetAwaiter().GetResult();
            }
        }
    }
}