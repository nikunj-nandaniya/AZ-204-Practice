using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageQueueFunction
{
    public class Order
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
