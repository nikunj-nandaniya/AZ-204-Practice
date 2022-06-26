using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace QueueService
{
    internal class Program
    {

        private static string _connectionSting = "";
        private static string _queueName = "niksqueue";
        static void Main(string[] args)
        {
            //ReadMessage();
            SendMessageToQueue();
            Console.ReadKey();
        }
        public static void SendMessageToQueue()
        {
            QueueClient client = new QueueClient(_connectionSting, _queueName);
            string message = string.Empty;
            string tempMessage = string.Empty;

            if (client.Exists())
            {
                for (int i = 0; i < 5; i++)
                {
                    tempMessage = $"This is Test Message from Nikunj {i}";
                    var txtbytes = System.Text.Encoding.UTF8.GetBytes(tempMessage);
                    message = System.Convert.ToBase64String(txtbytes);

                    client.SendMessage(message);
                }
            }

            Console.WriteLine("All the Messages have been sent.");
        }

        public static void PeakMessage()
        {
            QueueClient client = new QueueClient(_connectionSting, _queueName);            

            if (client.Exists())
            {
                PeekedMessage[] messages = client.PeekMessages(10);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                foreach (PeekedMessage message in messages)
                {
                    Console.WriteLine($"Message ID is {message.MessageId}");
                    Console.WriteLine($"Message Inserted Onm {message.InsertedOn}");
                    Console.WriteLine($"Message body is {message.Body.ToString()}");
                    Console.WriteLine($"End of the Message");
                }
            }
        }

        public static void ReadMessage()
        {
            QueueClient client = new QueueClient(_connectionSting, _queueName);

            if (client.Exists())
            {
                QueueMessage queueMessage = client.ReceiveMessage();
                Console.WriteLine(queueMessage.Body.ToString());

                client.DeleteMessage(queueMessage.MessageId,queueMessage.PopReceipt);
                Console.WriteLine("Message Deleted");
            }
        }
    }
}