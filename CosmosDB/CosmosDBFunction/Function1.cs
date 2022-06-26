using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CosmosDBFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([CosmosDBTrigger(
            databaseName: "Niksdb",
            collectionName: "Course",
            ConnectionStringSetting = "ConnectionString",
            LeaseCollectionName = "leases",CreateLeaseCollectionIfNotExists =true)]IReadOnlyList<Document> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {

                foreach (var course in input)
                {
                    Console.WriteLine($"id is{course.Id}");
                    Console.WriteLine($"Course Name is{ course.GetPropertyValue<string>("CourseName") }");
                }
            }
        }
    }
}
