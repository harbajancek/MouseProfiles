using MouseProfiles.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles.Services
{
    class Database
    {
        readonly JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        readonly HttpClient client = new HttpClient();
        readonly string dbPath;
        public Database(string dbName)
        {
            this.dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName + ".json");
            settings.Converters.Add(new JsonBooleanConverter());
        }
        public IEnumerable<MouseProfileModel> GetProfilesFromAPI()
        {
            var response = MouseProfileAPI.GetAllProfiles(client).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<IEnumerable<MouseProfileModel>>(json);
        }
        public IEnumerable<MouseProfileModel> GetMouseProfiles()
        {
            var profiles = GetProfilesFromAPI();
            if (profiles != null)
            {
                return profiles;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<MouseProfileModel>>(GetStringData());
            }
        }
        public void SaveProfilesToAPI(IEnumerable<MouseProfileModel> profiles)
        {
            settings.TypeNameHandling = TypeNameHandling.None;
            string data = JsonConvert.SerializeObject(profiles, settings);
            var response = MouseProfileAPI.PostInsertData(client, data).GetAwaiter().GetResult();
            var str = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
        public void SaveMouseProfiles(IEnumerable<MouseProfileModel> profiles)
        {
            string data = JsonConvert.SerializeObject(profiles, settings);
            File.WriteAllText(dbPath, data);
            SaveProfilesToAPI(profiles);
        }
        string GetStringData()
        {
            try
            {
                return File.ReadAllText(dbPath);
            }
            catch
            {
                File.WriteAllText(dbPath, "");
                return File.ReadAllText(dbPath);
            }

        }
    }
}
