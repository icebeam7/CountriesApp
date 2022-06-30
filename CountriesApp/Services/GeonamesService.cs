using System.Text.Json;
using System.Net.Http.Headers;

using CountriesApp.Models;

namespace CountriesApp.Services
{
    internal static class GeonamesService
{
        private static HttpClient createClient(string baseUrl)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        static string key = "icebeam";
        static string url = $"http://api.geonames.org/";
        static HttpClient client = createClient(url);

        public async static Task<List<Country>> GetCountries()
        {
            var serviceUrl = $"countryInfoJSON?username={key}";
            var response = await client.GetAsync(serviceUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var info = JsonSerializer.Deserialize<CountryInfo>(json, options);
                return info.Geonames;
            }
            else
                return new List<Country>();
        }
    }
}
