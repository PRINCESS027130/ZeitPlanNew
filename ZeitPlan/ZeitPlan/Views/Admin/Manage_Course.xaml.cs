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
    public partial class Manage_Course : ContentPage
    {
        public Manage_Course()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Select(x => new TBL_COURSE
            {
                COURSE_ID = x.Object.COURSE_ID,
                COURSE_NAME = x.Object.COURSE_NAME,
                CREDIT_HOURS = x.Object.CREDIT_HOURS,
                //TEACHER_FID = x.Object.TEACHER_FID

            }).ToList();
        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as TBL_COURSE;
            var item = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(a => a.Object.COURSE_ID == selected.COURSE_ID);
            var choice = await DisplayActionSheet("Option", "Cancel", "Delete", "Veiw", "Edit");
            if (choice=="Veiw")
            {
                //await DisplayAlert("Detail",
                // "\nUser Id : " + item.Object.Userid +
                //"\nName :  " + item.Object.Name+
                //"\nPassword : " + item.Object.Password+
                //"\nPhone : " + item.Object.Phone,"Ok"
                //);
                await Navigation.PushAsync(new Course_Detail(selected));
            }
            if (choice=="Delete")
            {
                var q = await DisplayAlert("Confirmation", "Are you want to delete this  " + item.Object.COURSE_NAME,"Yes","No");
                if (q)
                {
                    //Delete Single Record =========================================================
                    await App.firebaseDatabase.Child("TBL_COURSE").Child(item.Key).DeleteAsync();

                    //App.db.Delete(item);
                    // DataList.ItemsSource = App.db.Table<user>().ToList();
                    LoadData();
                    await DisplayAlert("Confirmation", "Deleted Permanantly  " + item.Object.COURSE_NAME, "Ok");
                }
            }
        }
    }
}