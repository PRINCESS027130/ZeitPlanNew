﻿using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;
using ZeitPlan.Views.Teacher;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Manage_Student: ContentPage
    {
        public Manage_Student()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).Select(x => new TBL_STUDENT
            {
                STUDENT_ID = x.Object.STUDENT_ID,
                STUDENT_NAME = x.Object.STUDENT_NAME,
                STUDENT_EMAIL = x.Object.STUDENT_EMAIL,
                STUDENT_PASSWORD = x.Object.STUDENT_PASSWORD,
                CLASS_FID=x.Object.CLASS_FID,
               
            }).ToList();
        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as TBL_STUDENT;
            var item = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(a => a.Object.STUDENT_ID == selected.STUDENT_ID);
            var choice = await DisplayActionSheet("Option", "Cancel", "Delete", "Veiw", "Edit");
            if (choice=="Veiw")
            {
                //await DisplayAlert("Detail",
                // "\nUser Id : " + item.Object.Userid +
                //"\nName :  " + item.Object.Name+
                //"\nPassword : " + item.Object.Password+
                //"\nPhone : " + item.Object.Phone,"Ok"
                //);
                await Navigation.PushAsync(new Student_Detail(selected));
            }
            if (choice=="Delete")
            {
                var q = await DisplayAlert("Confirmation", "Are you want to delete this  " + item.Object.STUDENT_NAME,"Yes","No");
                if (q)
                {
                    //Delete Single Record =========================================================
                    await App.firebaseDatabase.Child("TBL_STUDENT").Child(item.Key).DeleteAsync();

                    //App.db.Delete(item);
                    // DataList.ItemsSource = App.db.Table<user>().ToList();
                    LoadData();
                    await DisplayAlert("Confirmation", "Deleted Permanantly  " + item.Object.STUDENT_NAME, "Ok");
                }
            }
        }
    }
}