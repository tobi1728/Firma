using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Firma.PortalWWW.Services
{
    public class CmsApiClient
    {
        private readonly HttpClient _httpClient;

        public CmsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetContent(string page, string key)
        {
            var response = await _httpClient.GetAsync($"api/CmsEntry/{page}/{key}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

    }
}
