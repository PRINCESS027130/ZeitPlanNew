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
            LoadData();
        }
        async void LoadData()
        {
            var firebaseList = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
            {
                CLASS_ID = x.Object.CLASS_ID,
                CLASS_NAME = x.Object.CLASS_NAME,
                SESSION = x.Object.SESSION,
                SECTION = x.Object.SECTION,
                SHIFT = x.Object.SHIFT,
                DEGREE_FID = x.Object.DEGREE_FID,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.CLASS_NAME).ToList();
            ddlClass.ItemsSource = refinedList;
        }

            private async void btnStudent_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtstdName.Text) || string.IsNullOrEmpty(txtstdEmail.Text) || string.IsNullOrEmpty(txtstdPassword.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }
                if (ddlClass.SelectedItem == null)
                {
                    await DisplayAlert("ERROR", "Please select the Class", "ok");
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

                List<TBL_CLASS> classes = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
                {
                    CLASS_ID = x.Object.CLASS_ID,
                    SESSION = x.Object.SESSION,
                    SECTION = x.Object.SECTION,
                    SHIFT = x.Object.SHIFT,
                    DEGREE_FID = x.Object.DEGREE_FID,

                }).ToList();
                int selected = classes[ddlClass.SelectedIndex].CLASS_ID;

                TBL_STUDENT std = new TBL_STUDENT()
                {
                    STUDENT_ID= NewID,
                    STUDENT_NAME = txtstdName.Text,
                    STUDENT_EMAIL = txtstdEmail.Text,
                    STUDENT_PASSWORD = txtstdPassword.Text,
                  
                    
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