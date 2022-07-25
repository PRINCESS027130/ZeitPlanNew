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
    public partial class Add_Department : ContentPage
    {
        public Add_Department()
        {
            InitializeComponent();
        }

        private async void btnDepartment_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDepartmentName.Text) || string.IsNullOrEmpty(txtDDegreeFID.Text) || string.IsNullOrEmpty(txtDRoomFID.Text) || string.IsNullOrEmpty(txtDTeacherFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                // App.db.CreateTable<TBL_DEPARTMENT>();
                var check = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_NAME == txtDepartmentName.Text);
                if (check != null)
                {
                    await DisplayAlert("ERROR", "Degree is  already added", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).Max(a => a.Object.DEPARTMENT_ID);
                    NewID = ++LastID;
                }


                TBL_DEPARTMENT d = new TBL_DEPARTMENT()
                {
                    DEPARTMENT_ID= NewID,
                    DEPARTMENT_NAME =txtDepartmentName.Text,
                    DEGREEFID= int.Parse(txtDDegreeFID.Text),
                    ROOMFID = int.Parse(txtDRoomFID.Text),
                    TEACHERFID = int.Parse(txtDTeacherFID.Text),

                };

                // App.db.Insert(d);
                await App.firebaseDatabase.Child("TBL_DEPARTMENT").PostAsync(d);

                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Department Added", "ok");


            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
}