using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_Student : ContentPage
    {
        public Add_Student()
        {
            InitializeComponent();
        }

        private async void btnStudent_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtstdName.Text) || string.IsNullOrEmpty(txtstdEmail.Text) || string.IsNullOrEmpty(txtstdPassword.Text) || string.IsNullOrEmpty(txtstdClassFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                //App.db.CreateTable<TBL_STUDENT>();
                var check = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(x => x.Object.STUDENT_EMAIL == txtstdEmail.Text);
                if (check != null)
                {
                    await DisplayAlert("ERROR", "Student is  already added", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).Max(a => a.Object.STUDENT_ID);
                    NewID = ++LastID;
                }


                TBL_STUDENT std = new TBL_STUDENT()
                {
                    STUDENT_ID= NewID,
                    STUDENT_NAME = txtstdName.Text,
                    STUDENT_EMAIL = txtstdEmail.Text,
                    STUDENT_PASSWORD = txtstdPassword.Text,
                    CLASS_FID = int.Parse(txtstdClassFID.Text),
                    
                };

                //App.db.Insert(std);
                await App.firebaseDatabase.Child("TBL_STUDENT").PostAsync(std);

                LoadingInd.IsRunning = false;
                
                await DisplayAlert("Success", "Student Added", "ok");


            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;

                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
}