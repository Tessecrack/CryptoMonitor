using CryptoMonitor.Interfaces.Base.Entities;
using CryptoMonitor.Interfaces.Base.Repositories;
using System.Net.Http.Json;
using System.Net;

namespace CryptoMonitor.WebAPIClients.Repositories
{
    public class WebRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly HttpClient _client;

        public WebRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            var response = await _client.PostAsJsonAsync<T>("", item, cancel).ConfigureAwait(false);

            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancel)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<T> DeleteAsync(T item, CancellationToken cancel = default)
        {
            //return await DeleteByIdAsync(item.Id, cancel).ConfigureAwait(false);

            var request = new HttpRequestMessage(HttpMethod.Delete, "")
            {
                Content = JsonContent.Create(item)
            };

            var response = await _client.SendAsync(request, cancel).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancel)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<T> DeleteByIdAsync(int id, CancellationToken cancel = default)
        {
            var response = await _client.DeleteAsync($"{id}").ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            var result = await response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancel)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<bool> ExistAsync(T item, CancellationToken cancel = default)
        {
            var response = await _client.PostAsJsonAsync($"exist", item, cancel).ConfigureAwait(false);

            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        public async Task<bool> ExistIdAsync(int id, CancellationToken cancel = default)
        {
            var response = await _client.GetAsync($"exist/id/{id}", cancel).ConfigureAwait(false);

            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default)
        {
            return await _client.GetFromJsonAsync<IEnumerable<T>>("", cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAsync(int skip, int count, CancellationToken cancel = default)
        {
            return await _client.GetFromJsonAsync<IEnumerable<T>>($"items[{skip}:{count}]", cancel).ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancel = default)
        {
            return await _client.GetFromJsonAsync<T>($"{id}", cancel).ConfigureAwait(false);
        }

        public async Task<int> GetCountAsync(CancellationToken cancel = default)
        {
            return await _client.GetFromJsonAsync<int>($"count", cancel).ConfigureAwait(false);
        }

        public async Task<IPage<T>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            var response = await _client.GetAsync($"page[{pageIndex}/{pageSize}]", cancel).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new PageItems()
                {
                    Items = Enumerable.Empty<T>(),
                    TotalCount = 0, 
                    PageIndex = pageIndex, 
                    PageSize = pageSize
                };
            }

            return await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<PageItems>(cancel)
                .ConfigureAwait(false);
        }

        private class PageItems : IPage<T>
        {
            public IEnumerable<T> Items { get; init; }

            public int TotalCount { get; init; }

            public int PageIndex { get; init; }

            public int PageSize { get; init; }
        }

        public async Task<T> UpdateAsync(T item, CancellationToken cancel = default)
        {
            var response = await _client.PutAsJsonAsync<T>("", item, cancel).ConfigureAwait(false);

            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancel)
                .ConfigureAwait(false);

            return result;
        }
    }
}
