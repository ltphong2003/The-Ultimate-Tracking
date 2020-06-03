using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using The_Ultimate_Tracking.View;
using The_Ultimate_Tracking.ViewModel;
using The_Ultimate_Tracking.Model;
using System.Diagnostics;

namespace The_Ultimate_Tracking.ViewModel
{
    public class WelcomeVM
    {
        private string email;

        public WelcomeVM(string email)
        {
            this.email = email;
        }
        
        //Log out
        public Command LogOutCommand
        {
            get { return new Command(Logout); }
        }
        private async void Logout()
        {
            await App.Current.MainPage.Navigation.PushAsync(new View.TUT_LogIn());
        }

        //Sign Up new vehicle
        public Command NavigateToSignUpVehicleCommand
        {
            get { return new Command(navigateToSignUpVehicleCommand); }
        }
        private async void navigateToSignUpVehicleCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new TUT_SignUp(email));
        }
        
        //Update information
        public Command UpdateCommand

        {
            get { return new Command(Update); }
        }
        private async void Update()
        {
            try
            {
                if (!string.IsNullOrEmpty(Password))
                {
                    var isupdate = await FirebaseHelper.UpdateUser(email, Password);
                    if (isupdate)
                        await App.Current.MainPage.DisplayAlert("Update Success", "", "Ok");
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "Record not update", "Ok");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Password Require", "Please Enter your password", "Ok");
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error:{e}");
            }
        }
        
        //Delete account
        public Command DeleteCommand

        {
            get { return new Command(Delete); }
        }
        private async void Delete()
        {
            try
            {
                var isdelete = await FirebaseHelper.DeleteUser(email);
                if (isdelete)
                    await App.Current.MainPage.Navigation.PopAsync();
                else
                    await App.Current.MainPage.DisplayAlert("Error", "Record not delete", "Ok");
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error:{e}");
            }
        }
        
        public string Password { get; private set; }
        public string Name { get; private set; }
        public string Number { get; private set; }
    }
}
