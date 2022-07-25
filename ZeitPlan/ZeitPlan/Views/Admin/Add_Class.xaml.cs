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
    public partial class Add_Class : ContentPage
    {
        public Add_Class()
        {
            InitializeComponent();
        }

        private async void btnClass_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCSession.Text) || string.IsNullOrEmpty(txtCSection.Text) || string.IsNullOrEmpty(txtCShift.Text) || string.IsNullOrEmpty(txtCDegreeFID.Text) || string.IsNullOrEmpty(txtCStudentFID.Text) || string.IsNullOrEmpty(txtCTimeTableFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                //  App.db.CreateTable<TBL_CLASS>();
                var check = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.SESSION ==txtCSession.Text);
                if (check != null)
                {
                    await DisplayAlert("ERROR", "Course is  already added", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Max(a => a.Object.CLASS_ID);
                    NewID = ++LastID;
                }


                TBL_CLASS cl = new TBL_CLASS()
                {
                    CLASS_ID = NewID,
                    SESSION = txtCSession.Text,
                    SECTION = txtCSection.Text,
                    SHIFT = txtCShift.Text,
                    DEGREE_FID = int.Parse(txtCDegreeFID.Text),
                    STUDENTFID = int.Parse(txtCStudentFID.Text),
                   TIMETABLEFID = int.Parse(txtCTimeTableFID.Text)
                };

                //App.db.Insert(cl);
                await App.firebaseDatabase.Child("TBL_CLASS").PostAsync(cl);

                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Class Added", "ok");


            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
    
}