using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using The_Ultimate_Tracking.ViewModel;
using static The_Ultimate_Tracking.ViewModel.LoginViewModel;
using System.ComponentModel;

namespace The_Ultimate_Tracking.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TUT_LogIn : ContentPage
    {
        LoginViewModel loginViewModel;
        public TUT_LogIn()
        {
            InitializeComponent();
            IsLoading = false;
            BindingContext = this;
            loginViewModel = new LoginViewModel();
            BindingContext = loginViewModel;
        }
        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }

            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged1;

        public void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged1 != null)
            {
                PropertyChanged1(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}