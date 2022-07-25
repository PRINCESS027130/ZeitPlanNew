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
    public partial class Add_Room : ContentPage
    {
        public Add_Room()
        {
            InitializeComponent();
        }

        private async void btnRoom_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRoom_No.Text) || string.IsNullOrEmpty(txtRDepartmentFID.Text) || string.IsNullOrEmpty(txtRTimeTableFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                // App.db.CreateTable<TBL_ROOM>();
                var check = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).FirstOrDefault(x => x.Object.ROOM_NO == txtRoom_No.Text);
                if (check != null)
                {
                    await DisplayAlert("ERROR", "Room is  already added", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).Max(a => a.Object.ROOM_ID);
                    NewID = ++LastID;
                }

                TBL_ROOM r = new TBL_ROOM()
                {
                    ROOM_ID= NewID,
                    ROOM_NO = txtRoom_No.Text,
                    DEPARTMENT_FID = int.Parse(txtRDepartmentFID.Text),
                    TIMETABLE_FID = int.Parse(txtRTimeTableFID.Text),
                   
                };

                //App.db.Insert(r);
                await App.firebaseDatabase.Child("TBL_ROOM").PostAsync(r);

                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Room Added", "ok");


            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
}