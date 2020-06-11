using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.DeviceInfo;
using The_Ultimate_Tracking.Model;
using The_Ultimate_Tracking.ViewModel;
using static The_Ultimate_Tracking.ViewModel.SignUpVehicleVM;

namespace The_Ultimate_Tracking.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TUT_SignUp : ContentPage
    {
        SignUpVehicleVM signUpVehicleVM;
        public TUT_SignUp(string email)
        {
            InitializeComponent();
            signUpVehicleVM = new SignUpVehicleVM(email);  
            BindingContext = signUpVehicleVM;           
        }

        /*private string Vehicle_id;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var value = await FirebaseHelper.GetAllDetailsVehi();
            /*foreach (PastValueVehi item in value)
            {
                //Access values by item.Object.<field_name> and item.Key
                Vehicle_id = item.number;
                //await App.Current.MainPage.DisplayAlert("La no co chay qua", "", "Ok");
            }
            VehicleID.Text = Vehicle_id;
            lstDetail.ItemsSource = value;
        }

        public string vehicle_id
        {
            get { return Vehicle_id; }
        }*/
    }
}