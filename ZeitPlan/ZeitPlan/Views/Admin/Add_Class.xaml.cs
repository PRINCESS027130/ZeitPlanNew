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
    public partial class Add_Class : ContentPage
    {
        public Add_Class()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {
            var firebaseList = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).Select(x => new TBL_DEGREE
            {
                DEGREE_ID = x.Object.DEGREE_ID,
                DEGREE_NAME = x.Object.DEGREE_NAME,
                DEPARTMENT_FID = x.Object.DEPARTMENT_FID,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.DEGREE_NAME).ToList();
            ddlDegree.ItemsSource = refinedList;

            //var firebaseList1 = (await App.firebaseDatabase.Child("TBL_CLASS_COURSEASSIGN").OnceAsync<TBL_CLASS_COURSEASSIGN>()).Select(x => new TBL_CLASS_COURSEASSIGN
            //{
            //    CLASS_COURSEASSIGN_ID = x.Object.CLASS_COURSEASSIGN_ID,
            //    COURSE_FID = x.Object.COURSE_FID,
            //    CLASS_FID = x.Object.CLASS_FID

            //}).ToList();
            //var refinedList1 = firebaseList1.Select(x => x.CLASS_COURSEASSIGN_ID).ToList();
            // ddlClassCourseAssign.ItemsSource = refinedList1;

            //var firebaseList2 = (await App.firebaseDatabase.Child("TBL_TEACHER_ASSIGN").OnceAsync<TBL_TEACHER_ASSIGN>()).Select(x => new TBL_TEACHER_ASSIGN
            //{
            //    TEACHER_ASSIGN_ID = x.Object.TEACHER_ASSIGN_ID,
            //    TEACHER_FID = x.Object.TEACHER_FID,
            //    CLASS_FID = x.Object.CLASS_FID

            //}).ToList();
            //var refinedList2 = firebaseList2.Select(x => x.TEACHER_ASSIGN_ID).ToList();
            //ddlTeacherAssign.ItemsSource = refinedList2;

        }



        private async void btnClass_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCSession.Text) || string.IsNullOrEmpty(txtCSection.Text) || string.IsNullOrEmpty(txtCShift.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                if(ddlDegree.SelectedItem==null)
                {
                    await DisplayAlert("ERROR", "Please select the degree", "ok");
                    return;
                }

                //if (ddlClassCourseAssign.SelectedItem == null)
                //{
                //    await DisplayAlert("ERROR", "Please select the Class & Course", "ok");
                //    return;
                //}

                //if (ddlTeacherAssign.SelectedItem == null)
                //{
                //    await DisplayAlert("ERROR", "Please select the TeacherAssign", "ok");
                //    return;
                //}

                //if (ddlStudent.SelectedItem == null)
                //{
                //    await DisplayAlert("ERROR", "Please select the Student", "ok");
                //    return;
                //}

                //  App.db.CreateTable<TBL_CLASS>();
                var check = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_NAME ==txtCName.Text);
                if (check != null)
                {
                    await DisplayAlert("ERROR", "Class is  already added", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Max(a => a.Object.CLASS_ID);
                    NewID = ++LastID;
                }

                List<TBL_DEGREE>degree = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).Select(x => new TBL_DEGREE
                {
                    DEGREE_ID = x.Object.DEGREE_ID,
                    DEGREE_NAME = x.Object.DEGREE_NAME,
                    DEPARTMENT_FID = x.Object.DEPARTMENT_FID,

                }).ToList();
                int selected = degree[ddlDegree.SelectedIndex].DEGREE_ID;

                //List<TBL_CLASS_COURSEASSIGN> cc = (await App.firebaseDatabase.Child("TBL_CLASS_COURSEASSIGN").OnceAsync<TBL_CLASS_COURSEASSIGN>()).Select(x => new TBL_CLASS_COURSEASSIGN
                //{
                //    CLASS_COURSEASSIGN_ID = x.Object.CLASS_COURSEASSIGN_ID,
                //    COURSE_FID = x.Object.COURSE_FID,
                //    CLASS_FID = x.Object.CLASS_FID

                //}).ToList();
                //int selected1 = cc[ddlClassCourseAssign.SelectedIndex].CLASS_COURSEASSIGN_ID;

                //List<TBL_TEACHER_ASSIGN> tsn = (await App.firebaseDatabase.Child("TBL_TEACHER_ASSIGN").OnceAsync<TBL_TEACHER_ASSIGN>()).Select(x => new TBL_TEACHER_ASSIGN
                //{
                //    TEACHER_ASSIGN_ID = x.Object.TEACHER_ASSIGN_ID,
                //    TEACHER_FID = x.Object.TEACHER_FID,
                //    CLASS_FID = x.Object.CLASS_FID

                //}).ToList();
                //int selected2 = tsn[ddlTeacherAssign.SelectedIndex].TEACHER_ASSIGN_ID;



                TBL_CLASS cl = new TBL_CLASS()
                {
                    CLASS_ID = NewID,
                    CLASS_NAME=txtCName.Text,
                    SESSION = txtCSession.Text,
                    SECTION = txtCSection.Text,
                    SHIFT = txtCShift.Text,
                    DEGREE_FID = selected,
                    //CLASS_COURSE_FID=selected1,
                    //TEACHER_ASSIGN_FID=selected2
                   // STUDENT_FID=
       
                };

                //App.db.Insert(cl);
                await App.firebaseDatabase.Child("TBL_CLASS").PostAsync(cl);

                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Class Added", "ok");


            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
    
}