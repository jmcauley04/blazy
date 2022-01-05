using System.Net.Http.Json;

namespace Blazy.Abstractions
{
    public abstract class BaseHttpClient<THttpClient>
        where THttpClient : HttpClient
    {
        protected THttpClient _client;

        protected abstract string _baseEndpoint { get; }

        protected BaseHttpClient(THttpClient client)
        {
            _client = client;
        }

        #region get

        /// <summary>
        /// Use to hit an endpoint without getting items back
        /// </summary>
        protected async Task BaseGetAsync(string endpoint, CancellationToken ct = default)
        {
            var response = await _client.GetAsync(endpoint, ct);

            if (response.IsSuccessStatusCode)
                return;

            throw new Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        /// <summary>
        /// Use to get item(s) from an api endpoint
        /// </summary>
        protected async Task<TItem> BaseGetAsync<TItem>(string endpoint, CancellationToken ct = default)
        {
            var response = await _client.GetAsync(endpoint, ct);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TItem>();

            throw new Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        #endregion

        #region put

        /// <summary>
        /// Use to updating an existing item from an api endpoint
        /// </summary>
        protected async Task BasePutAsync<TItem>(string endpoint, TItem item, CancellationToken ct = default)
        {
            var response = await _client.PutAsJsonAsync(endpoint, item, ct);

            if (response.IsSuccessStatusCode)
                return;

            throw new Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        #endregion

        #region post

        /// <summary>
        /// Use to create a new item and getting an item returned from an api endpoint
        /// </summary>
        protected async Task<TItem> BasePostAsync<TItem>(string endpoint, TItem item, CancellationToken ct = default)
        {
            var response = await _client.PostAsJsonAsync(endpoint, item, ct);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TItem>();

            throw new Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        #endregion

        #region delete

        /// <summary>
        /// Use to delete an item from an api endpoint
        /// </summary>
        protected async Task BaseDeleteAsync(string endpoint, CancellationToken ct = default)
        {
            var response = await _client.DeleteAsync(endpoint, ct);

            if (response.IsSuccessStatusCode)
                return;

            throw new Exception($"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        #endregion
    }
}
