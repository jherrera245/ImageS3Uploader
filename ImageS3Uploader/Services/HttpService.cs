using Newtonsoft.Json;
using System.Text;

namespace ImageS3Uploader.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://mi-s3-service/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<TResponse> GetAsync<TResponse>(string endpoint)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            string responseData = await response.Content.ReadAsStringAsync();
            TResponse tResponse = JsonConvert.DeserializeObject<TResponse>(responseData);

            return tResponse;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode(); // Throw if not a success code.

            string responseData = await response.Content.ReadAsStringAsync();
            TResponse tResponse = JsonConvert.DeserializeObject<TResponse>(responseData);

            return tResponse;
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            string responseData = await response.Content.ReadAsStringAsync();
            TResponse tResponse = JsonConvert.DeserializeObject<TResponse>(responseData);

            return tResponse;
        }

        public async Task<TResponse> PatchAsync<TRequest, TResponse>(string endpoint, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), endpoint) { Content = content };

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string responseData = await response.Content.ReadAsStringAsync();
            TResponse tResponse = JsonConvert.DeserializeObject<TResponse>(responseData);

            return tResponse;
        }

        public async Task DeleteAsync(string endpoint)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
