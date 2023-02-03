using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;


namespace Managed_Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static string blob_url = "https://appstore100011.blob.core.windows.net/data/sample.txt";
        private static string download_path = "C:\\tmp\\nik.txt";

        public async Task<ActionResult> GetFile()
        {

            var _credential = new DefaultAzureCredential(includeInteractiveCredentials: true);
            Uri blob_uri = new Uri(blob_url);

            MemoryStream ms = new MemoryStream();
            CloudBlobClient BlobClient = 

            //storageAccount.CreateCloudBlobClient();
            //CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
            //BlobClient _client = new BlobClient(blob_uri, _credential);
            //CloudBlobContainer container = _client.

            if (await container.ExistsAsync())
            {
                CloudBlob file = container.GetBlobReference("nik.txt");

                if (await file.ExistsAsync())
                {
                    await file.DownloadToStreamAsync(ms);
                    Stream blobStream = file.OpenReadAsync().Result;
                    return File(blobStream, file.Properties.ContentType, file.Name);
                }
                else
                {
                    return Content("File does not exist");
                }
            }
            else
            {
                return Content("Container does not exist");
            }

        }
    }
}