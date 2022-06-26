using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;

namespace TableService
{
    internal class Program
    {

        private static string _connectionString = "";
        private static string _tableName = "Customers";
        private static string _partition_key = "Ahmedabad Updated";
        private static string _row_key = "1";

        static void Main(string[] args)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);

            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();

            CloudTable table =  cloudTableClient.GetTableReference(_tableName);

            //Customer customer = new Customer("Nikunj Updated", "Ahmedabad Updated", _row_key);

            //TableOperation operation = TableOperation.InsertOrMerge(customer);

            TableOperation operation = TableOperation.Retrieve<Customer>(_partition_key, _row_key);

            TableResult result = table.Execute(operation);

            Customer customer = result.Result as Customer;

            TableOperation deleteOperation = TableOperation.Delete(customer);

            TableResult deleteresult = table.Execute(deleteOperation);


            //List<Customer> lstCustomers = new List<Customer>()
            //{
            //    //new Customer("Bhavesh","Junagadh","2"),
            //    new Customer("Geeta","Ahmedabad","3"),
            //    new Customer("Heer","Ahmedabad","4"),
            //    //new Customer("Papa","Junagadh","5"),
            //    //new Customer("Mummy","Junagadh","6"),
            //};

            //TableBatchOperation batchOperation = new TableBatchOperation();
            //foreach (Customer _customer in lstCustomers)
            //{
            //    batchOperation.Insert(_customer);
            //}

            //Customer customer = new Customer("Nikunj", "Ahmedabad", "1");

            //TableOperation operation = TableOperation.Insert(customer);

            //TableResult result = table.Execute(operation);

            //TableBatchResult batchResult = table.ExecuteBatch(batchOperation);

            //table.CreateIfNotExists();

            //Customer customer = new Customer("Nikunj Updated",_partition_key,_row_key);

            //TableOperation operation = TableOperation.Retrieve<Customer>(_partition_key, _row_key);

            //TableResult result = table.Execute(operation);

            //Customer customer = result.Result as Customer;

            //Console.WriteLine($"Customer Name is { customer.CustomerName }");
            //Console.WriteLine($"Customer City is { customer.PartitionKey}");
            

            //Console.WriteLine("Batch Inserted.");

            Console.ReadKey();
        }
    }
}