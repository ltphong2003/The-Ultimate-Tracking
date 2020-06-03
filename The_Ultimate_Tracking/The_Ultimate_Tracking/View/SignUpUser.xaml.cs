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
    public partial class SignUpUser : ContentPage
    {
        SignUpVM signUpVM;
        public SignUpUser()
        {
            InitializeComponent();
            signUpVM = new SignUpVM();
            BindingContext = signUpVM;
        }
    }
}