using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace StorageQueueFunction
{
    public class Function1
    {
        [FunctionName("Function1")]

        [return: Table("Orders",Connection = "ConnectionString") ]
        public Order Run([QueueTrigger("niksqueue", Connection = "ConnectionString")]JObject myQueueItem, ILogger log)
        {
            Order order = new Order();

            order.PartitionKey = myQueueItem["Category"].ToString();
            order.RowKey = myQueueItem["OrderID"].ToString();
            order.Quantity = Convert.ToInt32(myQueueItem["Quantity"]);
            order.UnitPrice = Convert.ToDecimal(myQueueItem["UnitPrice"]);

            return (order);
            //log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

        }
    }
}
