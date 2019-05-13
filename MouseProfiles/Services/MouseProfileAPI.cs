using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using MouseProfiles.Models;

namespace MouseProfiles.Services
{
    static class MouseProfileAPI
    {
        public static Task<HttpResponseMessage> GetAllProfiles(HttpClient client)
        {
            var response = client.GetAsync("https://harbaja16.sps-prosek.cz/mouseprofiles/");
            return response;
        }

        public static Task<HttpResponseMessage> GetProfileById(HttpClient client, int id)
        {
            return client.GetAsync("https://harbaja16.sps-prosek.cz/mouseprofiles/?id="+id);
        }

        public static Task<HttpResponseMessage> PostInsertData(HttpClient client, string jsonData)
        {
            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("data", jsonData));
            FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
            return client.PostAsync("https://harbaja16.sps-prosek.cz/mouseprofiles/", content);
        }
    }
}
