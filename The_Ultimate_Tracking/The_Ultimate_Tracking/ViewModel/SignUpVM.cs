using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using The_Ultimate_Tracking.View;

namespace The_Ultimate_Tracking.ViewModel
{
        public class SignUpVM : INotifyPropertyChanged
        {
        private string Email;
            public string email
            {
                get { return Email; }
                set
                {
                    Email = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("email"));
                }
            }

            
        private string Password;

            public event PropertyChangedEventHandler PropertyChanged;

            public string password
            {
                get { return Password; }
                set
                {
                    Password = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("password"));
                }
            }

        private string confirmpassword;
            public string ConfirmPassword
            {
                get { return confirmpassword; }
                set
                {
                    confirmpassword = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
                }
            }
            public Command SignUpCommand
            {
                get
                {
                    return new Command(() =>
                    {
                        if (password == ConfirmPassword)
                            SignUp();
                        else
                            App.Current.MainPage.DisplayAlert("", "Mật khẩu xác nhận không đúng", "OK");
                    });
                }
            }
            private async void SignUp()
            {
                //null or empty field validation, check whether email and password is null or empty    

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    await App.Current.MainPage.DisplayAlert("Empty Values", "Vui lòng điền đủ thông tin", "OK");
                else
                {
                    //call AddUser function which we define in Firebase helper class    
                    var user = await FirebaseHelper.AddUser(email, password);
                    //AddUser return true if data insert successfuly     
                    if (user)
                    {
                        await App.Current.MainPage.DisplayAlert("Yay! Đăng ký thành công rồi!", "", "Ok");
                        //Navigate to Wellcom page after successfuly SignUp    
                        //pass user email to welcom page    
                        await App.Current.MainPage.Navigation.PushAsync(new WelcomePage(email));
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "Hiu hiu có lỗi", "OK");
                }
            }
        }
    }
