using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_ClassCourseAssign : ContentPage
    {
        public Add_ClassCourseAssign()
        {
            InitializeComponent();
            LoadData();
        }

        async void LoadData()
        {
            var firebaseList = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
            {
                CLASS_ID = x.Object.CLASS_ID,
                CLASS_NAME=x.Object.CLASS_NAME,
                SESSION = x.Object.SESSION,
                SECTION = x.Object.SECTION,
                SHIFT = x.Object.SHIFT,
                DEGREE_FID = x.Object.DEGREE_FID,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.CLASS_NAME).ToList();
            ddlClass.ItemsSource = refinedList;
            var firebaseList1 = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Select(x => new TBL_COURSE
            {
                COURSE_ID = x.Object.COURSE_ID,
                COURSE_NAME = x.Object.COURSE_NAME,
                CREDIT_HOURS = x.Object.CREDIT_HOURS,
                //TEACHER_FID = x.Object.TEACHER_FID

            }).ToList();
            var refinedList1 = firebaseList1.Select(x => x.COURSE_NAME).ToList();
            ddlCourse.ItemsSource = refinedList1;

        }

            private async void btnClassCourse_Clicked(object sender, EventArgs e)
            {
            try
            {

                if (ddlClass.SelectedItem == null)
                {
                    await DisplayAlert("ERROR", "Please select the Class", "ok");
                    return;
                }

                if (ddlCourse.SelectedItem == null)
                {
                    await DisplayAlert("ERROR", "Please select the Course", "ok");
                    return;
                }
                var check = (await App.firebaseDatabase.Child("TBL_CLASS_COURSEASSIGN").OnceAsync<TBL_CLASS_COURSEASSIGN>());
                if (check != null)
                {
                    await DisplayAlert("ERROR", "Class and Course  is  already added", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_CLASS_COURSEASSIGN").OnceAsync<TBL_CLASS_COURSEASSIGN>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_CLASS_COURSEASSIGN").OnceAsync<TBL_CLASS_COURSEASSIGN>()).Max(a => a.Object.CLASS_COURSEASSIGN_ID);
                    NewID = ++LastID;
                }
            
              List<TBL_CLASS> classes = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
              {
                 CLASS_ID = x.Object.CLASS_ID,
                  CLASS_NAME = x.Object.CLASS_NAME,
                  SESSION = x.Object.SESSION,
                 SECTION = x.Object.SECTION,
                 SHIFT = x.Object.SHIFT,
                 DEGREE_FID = x.Object.DEGREE_FID,

              }).ToList();
                   int selected = classes[ddlClass.SelectedIndex].CLASS_ID;

                List < TBL_COURSE > Course = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Select(x => new TBL_COURSE
                {
                    COURSE_ID = x.Object.COURSE_ID,
                    COURSE_NAME = x.Object.COURSE_NAME,
                    CREDIT_HOURS = x.Object.CREDIT_HOURS,
                    //TEACHER_FID = x.Object.TEACHER_FID

                }).ToList();
                int selected1 = Course[ddlCourse.SelectedIndex].COURSE_ID;



                TBL_CLASS_COURSEASSIGN cc = new TBL_CLASS_COURSEASSIGN()
                {
                    CLASS_COURSEASSIGN_ID = NewID,
                    CLASS_FID = selected,
                    COURSE_FID = selected1,
               
                };

            //App.db.Insert(cl);
            await App.firebaseDatabase.Child("TBL_CLASS_COURSEASSIGN").PostAsync(cc);

            LoadingInd.IsRunning = false;
            await DisplayAlert("Success", "Class & Course is Added", "ok");



            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
            }
    }
}