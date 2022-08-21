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
    public partial class Add_TimeTable : ContentPage
    {
        public Add_TimeTable()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {
            var firebaseList = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
            {
                CLASS_ID = x.Object.CLASS_ID,
                CLASS_NAME = x.Object.CLASS_NAME,
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

            var firebaseList2 = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
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
            var refinedList2 = firebaseList2.Select(x => x.TEACHER_NAME).ToList();
            ddlTeacher.ItemsSource = refinedList2;

            var firebaseList4 = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).Select(x => new TBL_ROOM
            {
                ROOM_ID = x.Object.ROOM_ID,
                ROOM_NO = x.Object.ROOM_NO,
                DEPARTMENT_FID = x.Object.DEPARTMENT_FID,

            }).ToList();
            var refinedList4 = firebaseList4.Select(x => x.ROOM_NO).ToList();
            ddlRoom.ItemsSource = refinedList4;


        }


        private async void btnTimeTable_Clicked(object sender, EventArgs e)
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

                if (ddlTeacher.SelectedItem == null)
                {
                    await DisplayAlert("ERROR", "Please select the Teacher", "ok");
                    return;
                }

                //var check = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>());
                //if (check != null)
                //{
                //    await DisplayAlert("ERROR", "This TimeTable  is  already added", "ok");
                //    return;
                //}
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).Max(a => a.Object.TIMETABLE_ID);
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

                List<TBL_COURSE> Course = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Select(x => new TBL_COURSE
                {
                    COURSE_ID = x.Object.COURSE_ID,
                    COURSE_NAME = x.Object.COURSE_NAME,
                    CREDIT_HOURS = x.Object.CREDIT_HOURS,
                    //TEACHER_FID = x.Object.TEACHER_FID

                }).ToList();
                int selected1 = Course[ddlCourse.SelectedIndex].COURSE_ID;

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
                int selected2 = teacher[ddlTeacher.SelectedIndex].TEACHER_ID;

                List<TBL_ROOM> room = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).Select(x => new TBL_ROOM
                {
                    ROOM_ID = x.Object.ROOM_ID,
                    ROOM_NO = x.Object.ROOM_NO,
                    DEPARTMENT_FID = x.Object.DEPARTMENT_FID,

                }).ToList();
                int selected4 = room[ddlRoom.SelectedIndex].ROOM_ID;


                TBL_TIMETABLE tb = new TBL_TIMETABLE()
                {
                    TIMETABLE_ID= NewID,
                    CLASS_FID = selected,
                    COURSE_FID = selected1,
                    TEACHER_FID=selected2,
                    //SLOT_FID=selected3,
                    ROOM_FID=selected4


                };

                //App.db.Insert(cl);
                await App.firebaseDatabase.Child("TBL_TIMETABLE").PostAsync(tb);

                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "TimeTable is Added", "ok");



            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }

        }
    }
}