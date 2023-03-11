using System.Text.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Integration
{
    public class IntegrationService : IIntegrationService
    {
        public string Token { get; set; }

        private readonly IHttpClientService _httpClientService;
        private string _baseDomain;
        private readonly string _apiVersion = "v1";

        public IntegrationService()
        {
            _httpClientService = new HttpClientService(string.Empty); //No domain can be inferred with this constructor.
        }

        public IntegrationService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public IntegrationService(string baseUrl, string bearerToken)
        {
            _baseDomain = baseUrl;
            Token = bearerToken;
            _httpClientService = new HttpClientService(_baseDomain);
        }

        public IntegrationService(string baseUrl, string apiKey, string token)
        {
            _baseDomain = baseUrl;
            Token = token;
            _httpClientService = new HttpClientService(_baseDomain, HttpClientService.Scheme.API, apiKey);
        }

        public IntegrationService(string baseUrl, string apiKey, string token, string apiVersion)
        {
            _baseDomain = baseUrl;
            Token = token;
            _httpClientService = new HttpClientService(_baseDomain, HttpClientService.Scheme.API, apiKey);
            _apiVersion = apiVersion;
        }

        public IntegrationService(string bearerToken, IHttpClientService httpClientService)
        {
            //No domain is necessary to be passed to this constructor because it should already be present in the http client service.
            _httpClientService = httpClientService; 
            Token = bearerToken;
        }


        #region Login

        public async Task<AuthenticationResponse> PostLoginAsync(string encryptedCredentials)
        {
            var requestUri = $"{_apiVersion}/login";
            var response = await _httpClientService.LoginAsync(encryptedCredentials, requestUri);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Unauthorized)
            {
                throw new Exception($"Invalid status code returned from web API. Status Code: {response.StatusCode}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(responseString))
            {
                return JsonSerializer.Deserialize<AuthenticationResponse>(responseString) ?? new AuthenticationResponse();

            }

            return new AuthenticationResponse();
        }

        #endregion


        #region Get

        public async Task<T> GetAsync<T>(string requestUrl)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.GetAsync(uri, Token);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<T> GetAsync<T>(string requestUrl, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.GetAsync(uri, Token, cancellationToken);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        #endregion


        #region Post

        public async Task<T> PostAsync<T>(string requestUrl)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PostAsync(uri, Token);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<T> PostAsync<T>(string requestUrl, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PostAsync(uri, Token, cancellationToken);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<T> PostAsync<T>(string requestUrl, T request)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PostAsync(uri, Token, request);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<T> PostAsync<T>(string requestUrl, T request, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PostAsync(uri, Token, request, cancellationToken);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string requestUrl, TRequest request)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PostAsync(uri, Token, request);
            return await GetIntegrationResponseAsync<TResponse>(httpResponse);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string requestUrl, TRequest request, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PostAsync(uri, Token, request, cancellationToken);
            return await GetIntegrationResponseAsync<TResponse>(httpResponse);
        }

        public async Task PostAsync(string requestUrl)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PostAsync(uri, Token);
            CheckForApiErrors(httpResponse);
        }

        public async Task PostAsync(string requestUrl, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PostAsync(uri, Token, cancellationToken);
            CheckForApiErrors(httpResponse);
        }

        #endregion


        #region Put

        public async Task<T> PutAsync<T>(string requestUrl)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PutAsync(uri, Token);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<T> PutAsync<T>(string requestUrl, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PutAsync(uri, Token, cancellationToken);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<T> PutAsync<T>(string requestUrl, T value)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PutAsync(uri, Token, value);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<T> PutAsync<T>(string requestUrl, T value, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PutAsync(uri, Token, value, cancellationToken);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string requestUrl, TRequest cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PutAsync(uri, Token, cancellationToken);
            return await GetIntegrationResponseAsync<TResponse>(httpResponse);
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string requestUrl, TRequest value, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PutAsync(uri, Token, value, cancellationToken);
            return await GetIntegrationResponseAsync<TResponse>(httpResponse);
        }

        public async Task PutAsync(string requestUrl)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PutAsync(uri, Token);
            CheckForApiErrors(httpResponse);
        }

        public async Task PutAsync(string requestUrl, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.PutAsync(uri, Token, cancellationToken);
            CheckForApiErrors(httpResponse);
        }

        #endregion


        #region Delete

        public async Task<T> DeleteAsync<T>(string requestUrl)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.DeleteAsync(uri, Token);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<T> DeleteAsync<T>(string requestUrl, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.DeleteAsync(uri, Token, cancellationToken);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<T> DeleteAsync<T>(string requestUrl, T value)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.DeleteAsync(uri, Token, value);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<T> DeleteAsync<T>(string requestUrl, T value, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.DeleteAsync(uri, Token, value, cancellationToken);
            return await GetIntegrationResponseAsync<T>(httpResponse);
        }

        public async Task<TResponse> DeleteAsync<TRequest, TResponse>(string requestUrl, TRequest value)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.DeleteAsync(uri, Token, value);
            return await GetIntegrationResponseAsync<TResponse>(httpResponse);
        }

        public async Task<TResponse> DeleteAsync<TRequest, TResponse>(string requestUrl, TRequest value, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.DeleteAsync(uri, Token, value, cancellationToken);
            return await GetIntegrationResponseAsync<TResponse>(httpResponse);
        }

        public async Task DeleteAsync(string requestUrl)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.DeleteAsync(uri, Token);
            CheckForApiErrors(httpResponse);
        }

        public async Task DeleteAsync(string requestUrl, CancellationToken cancellationToken)
        {
            var uri = GetRequestUri(_apiVersion, requestUrl);
            var httpResponse = await _httpClientService.DeleteAsync(uri, Token, cancellationToken);
            CheckForApiErrors(httpResponse);
        }

        #endregion


        #region Helpers

        private async Task<T> GetIntegrationResponseAsync<T>(HttpResponseMessage httpResponse)
        {
            CheckForApiErrors(httpResponse);

            string responseString = await httpResponse.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(responseString))
            {
                return JsonSerializer.Deserialize<T>(responseString);
            }            

            return default(T);            
        }

        private void CheckForApiErrors(HttpResponseMessage httpResponse)
        {
            //Always read the code from the http response in case something went wrong with the Response.
            if (httpResponse.StatusCode >= HttpStatusCode.InternalServerError)
            {                
                throw new Exception($"Invalid status code returned from web API. Status Code: {httpResponse.StatusCode}. Response String: {httpResponse.ToString()}");
            }
            else if (httpResponse.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception($"Request returned a NotFound response.");
            }
                
        }

        private string GetRequestUri(string version, string uri)
        {
            return $"{version}/{uri}";
        }

        #endregion
    }
}
