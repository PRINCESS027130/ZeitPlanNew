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
    public partial class Add_TeacherAssign : ContentPage
    {
        public Add_TeacherAssign()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {
            var firebaseList = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
            {
                TEACHER_ID = x.Object.TEACHER_ID,
                TEACHER_NAME = x.Object.TEACHER_NAME,
                TEACHER_EMAIL = x.Object.TEACHER_EMAIL,
                TEACHER_PASSWORD = x.Object.TEACHER_PASSWORD,
                TEACHER_PHNO = x.Object.TEACHER_PHNO,
                TEACHER_ADDRESS = x.Object.TEACHER_ADDRESS,
                TEACHER_IMAGE = x.Object.TEACHER_IMAGE,
                DEPARTMENT_FID = x.Object.DEPARTMENT_FID,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.TEACHER_NAME).ToList();
            ddlTeacher.ItemsSource = refinedList;

            var firebaseList1 = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Select(x => new TBL_COURSE
            {
                COURSE_ID = x.Object.COURSE_ID,
                COURSE_NAME = x.Object.COURSE_NAME,
                CREDIT_HOURS = x.Object.CREDIT_HOURS,

            }).ToList();
            var refinedList1 = firebaseList1.Select(x => x.COURSE_NAME).ToList();
            ddlCourse.ItemsSource = refinedList1;


        }




        private async void btnTeacherAssign_Clicked(object sender, EventArgs e)
        {
            try {

                if (ddlTeacher.SelectedItem == null)
                {
                    await DisplayAlert("ERROR", "Please select the Teacher", "ok");
                    return;
                }

                if (ddlCourse.SelectedItem == null)
                {
                    await DisplayAlert("ERROR", "Please select the Class", "ok");
                    return;
                }
                //var check = (await App.firebaseDatabase.Child("TBL_TEACHER_ASSIGN").OnceAsync<TBL_TEACHER_ASSIGN>());
                //if (check != null)
                //{
                //    await DisplayAlert("ERROR", "Teacher and Student  is  already added", "ok");
                //    return;
                //}
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_TEACHER_ASSIGN").OnceAsync<TBL_TEACHER_ASSIGN>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_TEACHER_ASSIGN").OnceAsync<TBL_TEACHER_ASSIGN>()).Max(a => a.Object.TEACHER_ASSIGN_ID);
                    NewID = ++LastID;
                }

                List<TBL_TEACHER> teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
                {
                    TEACHER_ID = x.Object.TEACHER_ID,
                    TEACHER_NAME = x.Object.TEACHER_NAME,
                    TEACHER_EMAIL = x.Object.TEACHER_EMAIL,
                    TEACHER_PASSWORD = x.Object.TEACHER_PASSWORD,
                    TEACHER_PHNO = x.Object.TEACHER_PHNO,
                    TEACHER_ADDRESS = x.Object.TEACHER_ADDRESS,
                    TEACHER_IMAGE = x.Object.TEACHER_IMAGE,
                    DEPARTMENT_FID = x.Object.DEPARTMENT_FID,

                }).ToList();
                int selected = teacher[ddlTeacher.SelectedIndex].TEACHER_ID;

                List<TBL_COURSE> co = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Select(x => new TBL_COURSE
                {
                    COURSE_ID = x.Object.COURSE_ID,
                    COURSE_NAME = x.Object.COURSE_NAME,
                    CREDIT_HOURS = x.Object.CREDIT_HOURS,
                }).ToList();
                 int selected1 = co[ddlCourse.SelectedIndex].COURSE_ID;


               

                TBL_TEACHER_ASSIGN ts = new TBL_TEACHER_ASSIGN()
                {
                    TEACHER_ASSIGN_ID = NewID,
                    TEACHER_FID= selected,
                    COURSE_FID= selected1

                 };

                //App.db.Insert(cl);
                await App.firebaseDatabase.Child("TBL_TEACHER_ASSIGN").PostAsync(ts);

                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Teacher & Student is Added", "ok");


            
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

              }
         }
    }
}