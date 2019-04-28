using MouseProfiles.Models;
using MouseProfiles.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles.ViewModels
{
    public class MouseProfileViewModel : INotifyPropertyChanged
    {
        public static async Task<MouseProfileViewModel> Create()
        {
            var ret = new MouseProfileViewModel();
            await ret.Initialize();
            return ret;
        }
        private MouseProfileViewModel()
        {
            Profiles = new ObservableCollection<MouseProfileModel>();
            Service = new MouseProfileService();
            App.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            SaveProfiles().Wait();
        }

        private async Task Initialize()
        {
            IEnumerable<MouseProfileModel> profiles = await Service.GetProfiles();
            await PopulateProfiles(profiles);
        }

        private async Task PopulateProfiles(IEnumerable<MouseProfileModel> profiles)
        {
            await Task.Factory.StartNew(async () =>
            {
                Profiles.Clear();
                Profiles.Add(Service.GetDefaultMouseProfile());
                Profiles.Add(await Service.GetCurrentMouseProfile());
                foreach (var item in profiles)
                {
                    Profiles.Add(item);
                }
            });
        }
        public async Task SaveProfiles()
        {
            await Service.SaveProfiles(Profiles);
        }
        public MouseProfileService Service;
        public ObservableCollection<MouseProfileModel> Profiles;
        private MouseProfileModel selectedProfile;
        public MouseProfileModel SelectedProfile
        {
            get { return selectedProfile; }
            set
            {
                selectedProfile = value;
                RaisePropertyChanged("CurrentProfile");
            }
        }
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
