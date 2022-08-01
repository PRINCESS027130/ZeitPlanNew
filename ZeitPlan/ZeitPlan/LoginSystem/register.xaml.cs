using Firebase.Database.Query;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;

namespace ZeitPlan.LoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class register : ContentPage
    {
        public register()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
          try 
            {   
            if (string.IsNullOrEmpty(txtName.Text)|| string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
               await DisplayAlert("ERROR", "Please fill the required field", "ok");
                return;
            }
            if (txtCPassword.Text!=txtPassword.Text)
            {
                await DisplayAlert("ERROR", "passwords do not match", "ok");
                return;
            }
                // App.db.CreateTable<user>();
                //var check = App.db.Table<user>().FirstOrDefault(x => x.Email == txtEmail.Text);
                //if (check != null)
                //{
                //  await DisplayAlert("ERROR", "Email already registered", "ok");
                //return;
                //}
              
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("user").OnceAsync<user>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("user").OnceAsync<user>()).Max(a => a.Object.Userid);
                    NewID = ++LastID;
                }


                user u = new user()
                {
                    Userid = NewID,
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    Phone = txtPhone.Text,

                };


                await App.firebaseDatabase.Child("user").PostAsync(u);

                LoadingInd.IsRunning = false;

                 // App.db.Insert(u);
                 await DisplayAlert("Success", "Account registered", "ok");
                await Navigation.PushAsync(new login(""));

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:"+ex.Message, "ok");

            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var EmailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            //if (Regex.IsMatch(e.NewTextValue, EmailPattern))
            //{
            //    lblEmailValid.IsVisible = true;
            //    lblEmailValid.Text = "Valid Email";
            //    lblEmailValid.TextColor = Color.Green;
            //}
            //else
            //{
            //    lblEmailValid.Text = "InValid Email! Email must contain @ and .com";
            //    lblEmailValid.TextColor = Color.Red;
            //}


        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (e.NewTextValue.Length < 11 || e.NewTextValue.Length > 13)
            //{
            //    lblPhoneValid.IsVisible = true;
            //    lblPhoneValid.Text = "InValid Phone! Missing digit(s)";
            //    lblPhoneValid.TextColor = Color.Red;
            //}
            //else
            //{
            //    lblPhoneValid.Text = "Valid Phone";
            //    lblPhoneValid.TextColor = Color.Green;
            //}

        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (e.NewTextValue.Length < 8)
            //{
            //    lblPasswordValid.IsVisible = true;
            //    lblPasswordValid.Text = "Password should be of at least 8 charaters";
            //    lblPasswordValid.TextColor = Color.Red;
            //}

            //var PasswordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])";
            //if (Regex.IsMatch(e.NewTextValue, PasswordPattern))
            //{
            //    lblPasswordValid.Text = "Strong Password!";
            //    lblPasswordValid.TextColor = Color.Green;
            //}
            //else
            //{
            //    lblPasswordValid.Text = "Weak Password! Password should contain Uppercase Letter , Lowercase Letter, Number(s) and special characters [$@$!%*#?&]";
            //    lblPasswordValid.TextColor = Color.Red;
            //}

        }
    }
}