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
        public MouseProfileViewModel()
        {
            Profiles = new ObservableCollection<MouseProfileModel>();
            Service = new MouseProfileService();
            App.Current.Exit += Current_Exit;
            IEnumerable<MouseProfileModel> profiles = Service.GetProfiles();
            PopulateProfiles(profiles);
        }

        private void Current_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            SaveProfiles();
        }

        private void PopulateProfiles(IEnumerable<MouseProfileModel> profiles)
        {
            Profiles.Clear();
            Profiles.Add(Service.GetDefaultMouseProfile());
            if (profiles == null)
            {
                return;
            }
            foreach (var item in profiles)
            {
                Profiles.Add(item);
            }
        }
        public void ActivateSelectedProfile()
        {
            Service.ApplyProfile(SelectedProfile);
        }
        public void CreateNewProfile(string name)
        {
            if (Profiles.Any(e => e.Name == name))
            {
                return;
            }
            MouseProfileModel profile = Service.GetDefaultMouseProfile();
            profile.Name = name;
            Profiles.Add(profile);
        }
        public void DeleteSelectedProfile()
        {
            Profiles.Remove(SelectedProfile);
        }
        public void SaveProfiles()
        {
            Service.SaveProfiles(Profiles);
        }
        public MouseProfileService Service { get; set; }
        public ObservableCollection<MouseProfileModel> Profiles { get; set; }
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
