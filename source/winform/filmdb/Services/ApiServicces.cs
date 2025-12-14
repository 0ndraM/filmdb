using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using filmdb.Models;
using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace filmdb.Services
{
    public static class ApiService
    {
        private static readonly string baseUrl =
            "https://0ndra.maweb.eu/FilmDB/";

        public static async Task<List<Film>> GetFilmsAsync(string orderBy, string search)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = baseUrl + "winformapi.php?order_by=" + orderBy + "&search=" + search;

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                        return new List<Film>(); // pokud není 200

                    string json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<Film>>(json);
                }
            }
            catch
            {
                return new List<Film>();  // při chybě serveru atd.
            }
        }

        public static string GetPosterUrl(string poster)
        {
            return baseUrl + poster;
        }
    }
}
