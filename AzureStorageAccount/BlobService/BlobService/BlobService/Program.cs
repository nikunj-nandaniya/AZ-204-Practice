// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using System.IO;

namespace HelloWorld
{
    class Program
    {
        private static string _connection_stirng = "";
        private static string _container_name = "niksblobcontainer";
        private static string _blob_name = "BlobTesting.txt";
        private static string _blob_path = "H:\\Screen Shots\\MAX.JPG";

        static void Main(string[] args)
        {
            try
            {
                //ReadBlob();
                TestLease();
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        public static void ReadBlob()
        {
            Uri _blob_Uri = GenerateSAS();
            BlobClient _client =new BlobClient(_blob_Uri);

            BlobProperties blobProperties = _client.GetProperties();

            Console.WriteLine("Blob Access Tier is " + blobProperties.AccessTier);
            Console.WriteLine("Size is " + blobProperties.ContentLength);

            //IDictionary<string, string> _metadata = blobProperties.Metadata;

            //foreach (var item in _metadata)
            //{
            //    Console.WriteLine(item.Key);
            //    Console.WriteLine(item.Value);            
            //}

            //_metadata.Add("Tier", "1");
            //_client.SetMetadata(_metadata);

            //_client.DownloadTo(_blob_path);
        }

        public static void TestLease()
        {
            try
            {
                Uri _blob_Uri = GenerateSAS();
                BlobClient _client = new BlobClient(_blob_Uri);

                MemoryStream _memoryStream = new MemoryStream();

                _client.DownloadTo(_memoryStream);
                _memoryStream.Position = 0;
                StreamReader _streamReader = new StreamReader(_memoryStream);
                Console.WriteLine(_streamReader.ReadToEnd());

                BlobLeaseClient _blob_lease_client = _client.GetBlobLeaseClient();
                BlobLease _lease = _blob_lease_client.Acquire(TimeSpan.FromSeconds(30));

                Console.WriteLine($"The lease is {_lease.LeaseId}");

                StreamWriter _streamWriter = new StreamWriter(_memoryStream);
                _streamWriter.Write("This change by Code with Lease Feature.");
                _streamWriter.Flush();

                BlobUploadOptions _blobUploadOptions = new BlobUploadOptions()
                {
                    Conditions = new BlobRequestConditions()
                    {
                        LeaseId = _lease.LeaseId
                    }                
                };

                _memoryStream.Position = 0;
                _client.Upload(_memoryStream, _blobUploadOptions);
                _blob_lease_client.Release();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static Uri GenerateSAS()
        {
            BlobServiceClient _service_client = new BlobServiceClient(_connection_stirng);
            // _service_client.CreateBlobContainer(_container_name);

            BlobContainerClient _container_client = _service_client.GetBlobContainerClient(_container_name);

            BlobClient _blob_client = _container_client.GetBlobClient(_blob_name);
            //_blob_client.Upload(_blob_path);
            //_blob_client.DownloadTo(_blob_path);

            //foreach (BlobItem item in _container_client.GetBlobs())
            //{
            //    Console.WriteLine(item.Name);
            //}

            BlobSasBuilder _builder = new BlobSasBuilder()
            {
                BlobContainerName = _container_name,
                BlobName = _blob_name,
                Resource= "b"
            };

            _builder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.List| BlobSasPermissions.Write | BlobSasPermissions.All);
            _builder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
            return _blob_client.GenerateSasUri(_builder);
        }
    }
}