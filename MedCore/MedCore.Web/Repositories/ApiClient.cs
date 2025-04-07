using MedCore.Domain.Base;
using MedCore.Web.Interfaces;
using Newtonsoft.Json;

namespace MedCore.Web.Repositories
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;

        public ApiClient(IConfiguration config, ILogger<ApiClient> logger)
        {
            _logger = logger;

            var baseUrl = config["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("No se encontró la URL base de la API en la configuración.");
            }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            return await HandleResponse<T>(response);
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            return await HandleResponse<TResponse>(response);
        }

        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            return await HandleResponse<TResponse>(response);
        }

        public async Task<TResponse?> DeleteAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = JsonContent.Create(data)
            };
            var response = await _httpClient.SendAsync(request);
            return await HandleResponse<TResponse>(response);
        }

        private async Task<T?> HandleResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) return default;
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            return result != null && result.Success
                ? JsonConvert.DeserializeObject<T>(result.Data?.ToString() ?? "")
                : default;
        }
    }
}
