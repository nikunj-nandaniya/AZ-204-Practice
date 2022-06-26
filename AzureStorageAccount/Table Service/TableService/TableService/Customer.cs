using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace TableService
{
    internal class Customer:TableEntity
    {
        public string CustomerName { get; set; }

        public Customer()
        { 
        
        }

        public Customer(string customerName, string customerCity,string customerId)
        {         
            CustomerName = customerName;
            PartitionKey = customerCity;
            RowKey = customerId;
        }
    }
}
