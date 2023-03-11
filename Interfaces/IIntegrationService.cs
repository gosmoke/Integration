using System.Threading;
using System.Threading.Tasks;

namespace Integration
{
    public interface IIntegrationService
    {
        string Token { get; set; }

        Task<AuthenticationResponse> PostLoginAsync(string encryptedCredentials);

        Task<T> GetAsync<T>(string requestUrl);
        Task<T> GetAsync<T>(string requestUrl, CancellationToken cancellationToken);

        Task<T> PostAsync<T>(string requestUrl);
        Task<T> PostAsync<T>(string requestUrl, CancellationToken cancellationToken);
        Task<T> PostAsync<T>(string requestUrl, T request);
        Task<T> PostAsync<T>(string requestUrl, T request, CancellationToken cancellationToken);
        Task<TResponse> PostAsync<TRequest, TResponse>(string requestUrl, TRequest request);
        Task<TResponse> PostAsync<TRequest, TResponse>(string requestUrl, TRequest request, CancellationToken token);
        Task PostAsync(string requestUrl);
        Task PostAsync(string requestUrl, CancellationToken cancellationToken);

        Task<T> PutAsync<T>(string requestUrl);
        Task<T> PutAsync<T>(string requestUrl, CancellationToken cancellationToken);
        Task<T> PutAsync<T>(string requestUrl, T value);
        Task<T> PutAsync<T>(string requestUrl, T value, CancellationToken cancellationToken);
        Task<TResponse> PutAsync<TRequest, TResponse>(string requestUrl, TRequest request);
        Task<TResponse> PutAsync<TRequest, TResponse>(string requestUrl, TRequest request, CancellationToken cancellationToken);
        Task PutAsync(string requestUrl);
        Task PutAsync(string requestUrl, CancellationToken cancellationToken);

        Task<T> DeleteAsync<T>(string requestUrl);
        Task<T> DeleteAsync<T>(string requestUrl, CancellationToken cancellationToken);
        Task<T> DeleteAsync<T>(string requestUrl, T value);
        Task<T> DeleteAsync<T>(string requestUrl, T value, CancellationToken cancellationToken);
        Task<TResponse> DeleteAsync<TRequest, TResponse>(string requestUrl, TRequest value);
        Task<TResponse> DeleteAsync<TRequest, TResponse>(string requestUrl, TRequest value, CancellationToken cancellationToken);
        Task DeleteAsync(string requestUrl);
        Task DeleteAsync(string requestUrl, CancellationToken cancellationToken);
    }
}
