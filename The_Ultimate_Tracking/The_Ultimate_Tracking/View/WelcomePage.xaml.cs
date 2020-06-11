using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ultimate_Tracking.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static The_Ultimate_Tracking.ViewModel.SignUpVM;

namespace The_Ultimate_Tracking.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        WelcomeVM welcomeVM;
        private Entry email;

        public WelcomePage()
        {
        }

        public WelcomePage(string email)
        {
            InitializeComponent();
            welcomeVM = new WelcomeVM(email);
            BindingContext = welcomeVM;
        }

        public WelcomePage(Entry email)
        {
            this.email = email;
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allVehicles = await FirebaseHelper.GetAllVehis();
            lstVehicle.ItemsSource = allVehicles;
        }
    }
}