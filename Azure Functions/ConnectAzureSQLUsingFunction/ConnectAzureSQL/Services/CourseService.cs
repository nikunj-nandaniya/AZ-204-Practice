using System.Data.SqlClient;
using System.Text.Json;
using ConnectAzureSQL.Models;

namespace ConnectAzureSQL.Services
{
    public class CourseService
    {
        private SqlConnection GetConnection(string _connection_string)
        {
            // Here we are creating the SQL connection
            return new SqlConnection(_connection_string);
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            // Here we now make a call to the Azure Function
            // Make sure to replace the URL with your Azure Function
            string functionurl = "https://niksfunctionapp.azurewebsites.net/api/Function1?code=hYAsXad_8-pDTDw3lINawxDlZh0XixE6P6zndkLvHJ79AzFuzHrYdQ==";
            // We are using the HttpClient class to send a request to the Azure Function and to get a response
            using (HttpClient _client = new HttpClient())
            {
                HttpResponseMessage _response = await _client.GetAsync(functionurl);
                string _content = await _response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<Course>>(_content);
            }
        }
    }
}
