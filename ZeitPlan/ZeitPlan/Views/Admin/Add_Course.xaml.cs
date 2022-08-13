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
    public partial class Add_Course : ContentPage
    {
        public Add_Course()
        {
            InitializeComponent();
            //LoadData();
        }
        //async void LoadData()
        //{
        //    var firebaseList = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
        //    {
        //        TEACHER_ID = x.Object.TEACHER_ID,
        //        TEACHER_NAME = x.Object.TEACHER_NAME,
        //        TEACHER_EMAIL = x.Object.TEACHER_EMAIL,
        //        TEACHER_PASSWORD = x.Object.TEACHER_PASSWORD,
        //        TEACHER_PHNO = x.Object.TEACHER_PHNO,
        //        TEACHER_ADDRESS = x.Object.TEACHER_ADDRESS,
        //        TEACHER_IMAGE = x.Object.TEACHER_IMAGE,
        //        DEPARTMENT_FID = x.Object.DEPARTMENT_FID,

        //    }).ToList();
        //    var refinedList = firebaseList.Select(x => x.TEACHER_NAME).ToList();
        //    ddlTeacher.ItemsSource = refinedList;

        //    var firebaseList1 = (await App.firebaseDatabase.Child("TBL_CLASS_COURSEASSIGN").OnceAsync<TBL_CLASS_COURSEASSIGN>()).Select(x => new TBL_CLASS_COURSEASSIGN
        //    {
        //        CLASS_COURSEASSIGN_ID = x.Object.CLASS_COURSEASSIGN_ID,
        //        COURSE_FID = x.Object.COURSE_FID,
        //        CLASS_FID = x.Object.CLASS_FID

        //    }).ToList();
        //    var refinedList1 = firebaseList1.Select(x => x.COURSE_FID).ToList();
        //    ddlClassCourseAssign.ItemsSource = refinedList1;
        //}

            private async void btnCourse_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCOName.Text) || string.IsNullOrEmpty(txtCOCredit_Hours.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }
                //if (ddlTeacher.SelectedItem == null)
                //{
                //    await DisplayAlert("ERROR", "Please select the Teacher", "ok");
                //    return;
                //}
                //if (ddlClassCourseAssign.SelectedItem == null)
                //{
                //    await DisplayAlert("ERROR", "Please select the Class & Course", "ok");
                //    return;
                //}


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
                //List<TBL_TEACHER> teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
                //{
                //    TEACHER_ID = x.Object.TEACHER_ID,
                //    TEACHER_NAME = x.Object.TEACHER_NAME,
                //    TEACHER_EMAIL = x.Object.TEACHER_EMAIL,
                //    TEACHER_PASSWORD = x.Object.TEACHER_PASSWORD,
                //    TEACHER_PHNO = x.Object.TEACHER_PHNO,
                //    TEACHER_ADDRESS = x.Object.TEACHER_ADDRESS,
                //    TEACHER_IMAGE = x.Object.TEACHER_IMAGE,
                //    DEPARTMENT_FID = x.Object.DEPARTMENT_FID,

                //}).ToList();
                //int selected = teacher[ddlTeacher.SelectedIndex].TEACHER_ID;

                //List<TBL_CLASS_COURSEASSIGN> cc = (await App.firebaseDatabase.Child("TBL_CLASS_COURSEASSIGN").OnceAsync<TBL_CLASS_COURSEASSIGN>()).Select(x => new TBL_CLASS_COURSEASSIGN
                //{
                //    CLASS_COURSEASSIGN_ID = x.Object.CLASS_COURSEASSIGN_ID,
                //    COURSE_FID = x.Object.COURSE_FID,
                //    CLASS_FID = x.Object.CLASS_FID

                //}).ToList();
                //int selected1 = cc[ddlClassCourseAssign.SelectedIndex].CLASS_COURSEASSIGN_ID;


                TBL_COURSE co = new TBL_COURSE()
                {
                    COURSE_ID= NewID,
                    COURSE_NAME = txtCOName.Text,
                    CREDIT_HOURS = txtCOCredit_Hours.Text,
                    //TEACHER_FID=selected,
                    //CLASS_COURSE_FID=selected1
                   
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