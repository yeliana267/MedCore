namespace MedCore.Web.Interfaces
{
    public interface IApiClient
    {
        Task<T?> GetAsync<T>(string endpoint);
        Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<TResponse?> DeleteAsync<TRequest, TResponse>(string endpoint, TRequest data);
    }
}
