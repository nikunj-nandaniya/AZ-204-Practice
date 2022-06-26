using Microsoft.Azure.Cosmos;

namespace CosmosDB
{
    internal class Program
    {
        private static string _connectionStinrg = "";
        private static string _dbName = "Niksdb";
        private static string _containerName = "Course";
        private static string _partitionKey = "/courseid";

        static void Main(string[] args)
        {
            AddItem();
            Console.ReadKey();
        }
        private static void CreateDB()
        {
            CosmosClient cosmosClient = new CosmosClient(_connectionStinrg);
            cosmosClient.CreateDatabaseAsync(_dbName).GetAwaiter().GetResult();
            CreateContainer(cosmosClient);
        }
        private static void CreateContainer(CosmosClient cosmosClient)
        {
            Database database = cosmosClient.GetDatabase(_dbName);
            database.CreateContainerAsync(_containerName, _partitionKey).GetAwaiter().GetResult();
        }
        private static void AddItem()
        {
            CosmosClient cosmosClient = new CosmosClient(_connectionStinrg);
            Container container = cosmosClient.GetContainer(_dbName, _containerName);

            Course course = new Course 
            {
                id = "9", 
                courseid = "C0009",
                CourseName = "Course9", 
                Rating = 4.5m,
                Orders = new List<Order>() 
                { 
                    new Order { OrderId="1", Price=10000 },
                    new Order { OrderId="2", Price=50000 },                
                }
            
            };

            container.CreateItemAsync<Course>(course, new PartitionKey(course.courseid)).GetAwaiter().GetResult();

            Console.WriteLine("Item Created.");
        }
        private static void AddBulkItem()
        {
            CosmosClient cosmosClient = new CosmosClient(_connectionStinrg, new CosmosClientOptions() { AllowBulkExecution = true });
            Container container = cosmosClient.GetContainer(_dbName, _containerName);

            List<Task> lsttasks = new List<Task>();

            List<Course> lstCourses = new List<Course>()
            {
                new Course { id = "2",courseid = "C0002",CourseName ="Course1",Rating =4.5m },
                new Course { id = "3", courseid = "C0003", CourseName = "Course2", Rating = 4.6m },
                new Course { id = "4", courseid = "C0004", CourseName = "Course3", Rating = 4.4m },
                new Course { id = "5", courseid = "C0005", CourseName = "Course4", Rating = 4.8m },
                new Course { id = "6", courseid = "C0006", CourseName = "Course5", Rating = 4.9m }
            };

            foreach (Course course in lstCourses)
            {
                lsttasks.Add(container.CreateItemAsync<Course>(course, new PartitionKey(course.courseid)));
            }

            Task.WhenAll(lsttasks).GetAwaiter().GetResult();

            Console.WriteLine("Bulk Items Created.");
        }
        private static void GetCourses()
        {
            CosmosClient cosmosClient = new CosmosClient(_connectionStinrg, new CosmosClientOptions() { AllowBulkExecution = true });
            Container container = cosmosClient.GetContainer(_dbName, _containerName);
            String query = "SELECT * FROM Course Order By Course.id";

            QueryDefinition queryDefinition = new QueryDefinition(query);

            FeedIterator<Course> feedIterator = container.GetItemQueryIterator<Course>(queryDefinition);

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<Course> feedResponse = feedIterator.ReadNextAsync().GetAwaiter().GetResult();

                foreach (Course course in feedResponse)
                {
                    Console.WriteLine($"id is {course.id}");
                    Console.WriteLine($"Course id is {course.courseid}");
                    Console.WriteLine($"Course Name is {course.CourseName}");
                    Console.WriteLine($"Course Rating is {course.Rating}");
                }
            }
        }
        private static void UpdateCourse()
        {
            CosmosClient cosmosClient = new CosmosClient(_connectionStinrg, new CosmosClientOptions() { AllowBulkExecution = true });
            Container container = cosmosClient.GetContainer(_dbName, _containerName);

            string _id = "6";
            string _partitionKey = "C0006";

            ItemResponse<Course> response = container.ReadItemAsync<Course>(_id,new PartitionKey(_partitionKey)).GetAwaiter().GetResult();

            Course course = response.Resource;

            course.Rating = 4.5m;

            container.ReplaceItemAsync<Course>(course, course.id, new PartitionKey(_partitionKey)).GetAwaiter().GetResult();

            Console.WriteLine("Record has been updated.");
        }
        private static void DeleteCourse()
        {
            CosmosClient cosmosClient = new CosmosClient(_connectionStinrg, new CosmosClientOptions() { AllowBulkExecution = true });
            Container container = cosmosClient.GetContainer(_dbName, _containerName);

            string _id = "6";
            string _partitionKey = "C0006";

            ItemResponse<Course> response = container.DeleteItemAsync<Course>(_id, new PartitionKey(_partitionKey)).GetAwaiter().GetResult();
            Console.WriteLine("Record has been deleted.");
        }
        private static void AddItemSP()
        {
            CosmosClient cosmosClient = new CosmosClient(_connectionStinrg, new CosmosClientOptions() { AllowBulkExecution = true });
            Container container = cosmosClient.GetContainer(_dbName, _containerName);

            dynamic[] _items = new dynamic[]
            {
                new { id="7", courseid = "Course0007", CourseName = "Course7", Rating = 4.9m}
            };

            string output = container.Scripts.ExecuteStoredProcedureAsync<string>("AddItem",new PartitionKey("Course0007"),new[] { _items }).GetAwaiter().GetResult();

            Console.WriteLine(output);
        }
        private static void TestTrigger()
        {
            CosmosClient cosmosClient = new CosmosClient(_connectionStinrg);
            Container container = cosmosClient.GetContainer(_dbName, _containerName);

            Course course = new Course()
            {
                 id="8", courseid = "Course0008", CourseName = "Course8", Rating = 4.6m
            };

            container.CreateItemAsync<Course>(course, null,new ItemRequestOptions { PreTriggers = new List<string> { "AddTimestamp" } } ).GetAwaiter().GetResult();
            Console.WriteLine("Item Created");                       
        }
    }
}
