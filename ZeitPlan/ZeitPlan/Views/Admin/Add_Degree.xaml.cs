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
    public partial class Add_Degree : ContentPage
    {
        public Add_Degree()
        {
            InitializeComponent();
        }

        private async void btnDegree_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDegName.Text) || string.IsNullOrEmpty(txtDegDepartmentFID.Text) || string.IsNullOrEmpty(txtDegDepartmentFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                // App.db.CreateTable<TBL_DEGREE>();
                var check = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).FirstOrDefault(x => x.Object.DEGREE_NAME == txtDegName.Text);
                if (check != null)
                {
                    await DisplayAlert("ERROR", "Degree is  already added", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).Max(a => a.Object.DEGREE_ID);
                    NewID = ++LastID;
                }

                TBL_DEGREE deg = new TBL_DEGREE()
                {
                    DEGREE_ID= NewID,
                    DEGREE_NAME = txtDegName.Text,
                    DEPARTMENT_FID = int.Parse(txtDegDepartmentFID.Text),
                   
                  

                };

                //App.db.Insert(deg);
                await App.firebaseDatabase.Child("TBL_DEGREE").PostAsync(deg);

                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Degree Added", "ok");


            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
}