using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ultimate_Tracking.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.DeviceInfo;
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
    }
}