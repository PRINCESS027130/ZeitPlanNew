using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_Course : ContentPage
    {
        public Add_Course()
        {
            InitializeComponent();
        }

        private async void btnCourse_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCOName.Text) || string.IsNullOrEmpty(txtCOCredit_Hours.Text) || string.IsNullOrEmpty(txtCODegreeFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

               // App.db.CreateTable<TBL_COURSE>();
                var check =(await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(x => x.Object.COURSE_NAME ==txtCOName.Text);
                if (check != null)
                {
                    await DisplayAlert("ERROR", "Course is  already added", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Max(a => a.Object.COURSE_ID);
                    NewID = ++LastID;
                }


                TBL_COURSE co = new TBL_COURSE()
                {
                    COURSE_ID= NewID,
                    COURSE_NAME = txtCOName.Text,
                    CREDIT_HOURS = txtCOCredit_Hours.Text,
                    DEGREE_FID=int.Parse(txtCODegreeFID.Text),
                   
    

                };

                // App.db.Insert(co);
                await App.firebaseDatabase.Child("TBL_COURSE").PostAsync(co);

                LoadingInd.IsRunning = false;

                await DisplayAlert("Success", "Course Added", "ok");


            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
}