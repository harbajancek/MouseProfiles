using MouseProfiles.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles
{
    class Database
    {
        readonly JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        readonly HttpClient client = new HttpClient();
        readonly string dbPath;
        public Database(string dbName)
        {
            this.dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName + ".json");
        }
        public IEnumerable<MouseProfileModel> GetProfilesFromPHP()
        {
            var response = client.GetAsync("https://harbaja16.sps-prosek.cz/MouseProfiles/jsonGetMouseProfiles.php").GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<IEnumerable<MouseProfileModel>>(json);
        }
        public IEnumerable<MouseProfileModel> GetMouseProfiles()
        {
            var profiles = GetProfilesFromPHP();
            if (profiles != null)
            {
                return profiles;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<MouseProfileModel>>(GetStringData());
            }
        }
        public void SaveMouseProfiles(IEnumerable<MouseProfileModel> profiles)
        {
            string data = JsonConvert.SerializeObject(profiles, settings);
            File.WriteAllText(dbPath, data);
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
