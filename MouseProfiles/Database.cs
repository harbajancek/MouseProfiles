using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles
{
    class Database
    {
        private JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        string dbPath { get; set; }
        public Database(string dbPath)
        {
            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbPath + ".json");
        }
        public List<MouseProfile> GetMouseProfiles()
        {
            return JsonConvert.DeserializeObject<List<MouseProfile>>(GetStringData());
        }
        public void SaveMouseProfiles(List<MouseProfile> profiles)
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
