using MouseProfiles.Models;
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
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        readonly string dbPath;
        public Database(string dbName)
        {
            this.dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName + ".json");
        }
        public async Task<IEnumerable<MouseProfileModel>> GetMouseProfilesAsync()
        {
            return await Task.Factory.StartNew<IEnumerable<MouseProfileModel>>(() =>
            {
                return GetMouseProfiles();
            });
        }
        public IEnumerable<MouseProfileModel> GetMouseProfiles()
        {
            return JsonConvert.DeserializeObject<IEnumerable<MouseProfileModel>>(GetStringData());
        }
        public async Task SaveMouseProfilesAsync(IEnumerable<MouseProfileModel> profiles)
        {
            await Task.Factory.StartNew(() =>
            {
                 SaveMouseProfiles(profiles);
            });
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
