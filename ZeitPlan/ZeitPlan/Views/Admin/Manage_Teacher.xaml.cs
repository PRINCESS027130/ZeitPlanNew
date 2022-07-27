using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;
using ZeitPlan.Views.Admin;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Manage_Teacher: ContentPage
    {
        public Manage_Teacher()
        {
            InitializeComponent();
            //try
            //{
              //  DataList.ItemsSource = App.db.Table<user>().ToList();
            //}
           // catch (Exception ex)
            //{

              //  DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");
            //}
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                LoadingInd.IsRunning = true;
                LoadData();
                LoadingInd.IsRunning = false;
            }
            catch (Exception ex)
            {

               await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");
            }
            

        }

        async void LoadData()
        {
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
            {
                TEACHER_ID = x.Object.TEACHER_ID,
                TEACHER_NAME = x.Object.TEACHER_NAME,
                TEACHER_EMAIL= x.Object.TEACHER_EMAIL,
                TEACHER_PASSWORD = x.Object.TEACHER_PASSWORD,
                TEACHER_PHNO = x.Object.TEACHER_PHNO,
                TEACHER_ADDRESS = x.Object.TEACHER_ADDRESS,
                TEACHER_IMAGE = x.Object.TEACHER_IMAGE,
                DEPARTMENT_FID = x.Object.DEPARTMENT_FID,
               
               

            }).ToList();
        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as TBL_TEACHER;
            var item = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(a => a.Object.TEACHER_ID == selected.TEACHER_ID);
            var choice = await DisplayActionSheet("Option", "Cancel", "Delete", "Veiw", "Edit");
            if (choice=="Veiw")
            {
                //await DisplayAlert("Detail",
                // "\nUser Id : " + item.Object.Userid +
                //"\nName :  " + item.Object.Name+
                //"\nPassword : " + item.Object.Password+
                //"\nPhone : " + item.Object.Phone,"Ok"
                //);
                await Navigation.PushAsync(new Teacher_Detail(selected));
            }
            if (choice=="Delete")
            {
                var q = await DisplayAlert("Confirmation", "Are you want to delete this  " + item.Object.TEACHER_NAME,"Yes","No");
                if (q)
                {
                    //Delete Single Record =========================================================
                    await App.firebaseDatabase.Child("TBL_TEACHER").Child(item.Key).DeleteAsync();

                    //App.db.Delete(item);
                    // DataList.ItemsSource = App.db.Table<user>().ToList();
                    LoadData();
                    await DisplayAlert("Confirmation", "Deleted Permanantly  " + item.Object.TEACHER_NAME, "Ok");
                }
            }
        }
    }
}