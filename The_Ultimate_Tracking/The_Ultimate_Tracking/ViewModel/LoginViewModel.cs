using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using The_Ultimate_Tracking.View;

namespace The_Ultimate_Tracking.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public LoginViewModel()
        {

        }
        private string Email;
        public string email
        {
            get { return Email; }
            set
            {
                Email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string Password;
        public string password
        {
            get { return Password; }
            set
            {
                Password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public Command LoginCommand
        {
            get
            {
                return new Command(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Vui lòng điền đầy đủ thông tin", "OK");
            else
            {
                //call GetUser function which we define in Firebase helper class    
                var user = await FirebaseHelper.GetUser(email);
                //firebase return null valuse if user data not found in database    
                if (user != null)
                    if (email == user.email && password == user.password)
                    {
                        await App.Current.MainPage.DisplayAlert("Đăng nhập thành công", "", "Ok");
                        //Navigate to Welcome page after successfuly login    
                        //pass user email to welcome page    
                        await App.Current.MainPage.Navigation.PushAsync(new WelcomePage(email));
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                else
                    await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
            }
        }
        public Command NavigateToSignUpCommand
        {
            get
            {
                return new Command(Navigatetosignup);
            }
        }
        private async void Navigatetosignup()
        {
            await App.Current.MainPage.Navigation.PushAsync(new SignUpUser());
        }
    }
}