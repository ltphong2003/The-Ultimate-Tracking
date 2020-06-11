using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.DeviceInfo;
using The_Ultimate_Tracking.Model;
using The_Ultimate_Tracking.View;
using The_Ultimate_Tracking.ViewModel;

namespace The_Ultimate_Tracking.ViewModel
{
    public class SignUpVehicleVM : INotifyPropertyChanged
    {
        private string email;

        public SignUpVehicleVM(string email)
        {
            this.email = email;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string User_id;
        private async void SetUserId()
        {
            User_id = await FirebaseHelper.GetAllDetails(email);
        }
        public string user_id
        {
            get { return User_id; }
        }

        private string Name;
        public string name
        {
            get { return Name; }
            set
            {
                Name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("name"));
            }
        }

        private string Lpn;
        public string lpn
        {
            get { return Lpn; }
            set
            {
                Lpn = value;
                PropertyChanged(this, new PropertyChangedEventArgs("lpn"));
            }
        }

        private string Imei = CrossDeviceInfo.Current.Id;
        public string imei
        {
            get { return Imei; }
        }
      
        public string vehicle_id { get; set; }
        
        //Command
        public Command SignUpVehicleCommand
        {
            get
            {
                return new Command(() =>
                {    
                    SignUpVehi();   
                });
            }
        }
        private async void SignUpVehi()
        {
            SetUserId();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lpn))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Vui lòng điền đủ thông tin", "OK");
            else
            {  
                var vehicle = await FirebaseHelper.AddVehicle(imei, lpn, name);   
                if (vehicle)
                {
                    await App.Current.MainPage.DisplayAlert("Yay! Bạn có thêm xe mới kìa!", "", "Ok");    
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                    await App.Current.MainPage.DisplayAlert("Error", "Hiu hiu có lỗi", "OK");
            }
        }
    }
}
