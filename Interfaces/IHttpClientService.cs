using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Integration
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> LoginAsync(string encodedCredentials, string uri);
        Task<HttpResponseMessage> GetAsync(string uri, string apiToken);
        Task<HttpResponseMessage> GetAsync(string uri, string apiToken, CancellationToken cancellationToken);
        Task<HttpResponseMessage> PostAsync(string uri, string apiToken);
        Task<HttpResponseMessage> PostAsync(string uri, string apiToken, CancellationToken cancellationToken);
        Task<HttpResponseMessage> PostAsync<T>(string uri, string apiToken, T request);
        Task<HttpResponseMessage> PostAsync<T>(string uri, string apiToken, T request, CancellationToken cancellationToken);
        Task<HttpResponseMessage> PutAsync(string uri, string apiToken);
        Task<HttpResponseMessage> PutAsync(string uri, string apiToken, CancellationToken cancellationToken);
        Task<HttpResponseMessage> PutAsync<T>(string uri, string apiToken, T request);
        Task<HttpResponseMessage> PutAsync<T>(string uri, string apiToken, T request, CancellationToken cancellationToken);
        Task<HttpResponseMessage> DeleteAsync(string uri, string apiToken);
        Task<HttpResponseMessage> DeleteAsync(string uri, string apiToken, CancellationToken cancellationToken);
        Task<HttpResponseMessage> DeleteAsync<T>(string uri, string apiToken, T request);
        Task<HttpResponseMessage> DeleteAsync<T>(string uri, string apiToken, T request, CancellationToken cancellationToken);
    }
}
