using Firebase.Database.Query;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class Add_Teachers : ContentPage
    {
        private MediaFile _mediaFile;
        public static string PicPath = "image_picker.png";
        public Add_Teachers()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {
            var firebaseList = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).Select(x => new TBL_DEPARTMENT

            {
                DEPARTMENT_ID = x.Object.DEPARTMENT_ID,
                DEPARTMENT_NAME = x.Object.DEPARTMENT_NAME,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.DEPARTMENT_NAME).ToList();
            ddlDepartment.ItemsSource = refinedList;

            var firebaseList1 = (await App.firebaseDatabase.Child("TBL_TEACHER_ASSIGN").OnceAsync<TBL_TEACHER_ASSIGN>()).Select(x => new TBL_TEACHER_ASSIGN
            {
                TEACHER_ASSIGN_ID = x.Object.TEACHER_ASSIGN_ID,
                TEACHER_FID = x.Object.TEACHER_FID,
                CLASS_FID = x.Object.CLASS_FID

            }).ToList();
            var refinedList1 = firebaseList1.Select(x => x.TEACHER_ASSIGN_ID).ToList();
           ddlTeacherAssign.ItemsSource = refinedList1;

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var response = await DisplayActionSheet("Select Image Source", "Close", "", "From Gallery", "From Camera");
                if (response == "From Camera")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Take Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new StoreCameraMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }
                    _mediaFile = SelectedImg;
                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;


                }
                if (response == "From Gallery")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Pick Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }
                    _mediaFile = SelectedImg;
                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;


                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Something Went wrong \n" + ex.Message, "OK");

            }
        }

        private async void btnTeacher_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTeName.Text) || string.IsNullOrEmpty(txtTePhone.Text) || string.IsNullOrEmpty(txtTeEmail.Text) || string.IsNullOrEmpty(txtTePassword.Text) || string.IsNullOrEmpty(txtTeAddress.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }
                if (ddlDepartment.SelectedItem == null)
                {
                    await DisplayAlert("ERROR", "Please select the Department", "ok");
                    return;
                }
                if (ddlTeacherAssign.SelectedItem == null)
                {
                    await DisplayAlert("ERROR", "Please select the TeacherAssign", "ok");
                    return;
                }
                // App.db.CreateTable<TBL_TEACHER>();
                //  var check = App.db.Table<TBL_TEACHER>().FirstOrDefault(x => x.TEACHER_EMAIL == txtTeEmail.Text);
                //if (check != null)
                //{
                //  await DisplayAlert("ERROR", "Email already registered", "ok");
                //return;
                //}
                var check = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_EMAIL == txtTeEmail.Text);
                if (check != null)
                {
                    await DisplayAlert("ERROR", "Teacher is  already added", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Max(a => a.Object.TEACHER_ID);
                    NewID = ++LastID;
                }
                List<TBL_DEPARTMENT> dept = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).Select(x => new TBL_DEPARTMENT
                {
                    DEPARTMENT_ID = x.Object.DEPARTMENT_ID,
                    DEPARTMENT_NAME = x.Object.DEPARTMENT_NAME,

                }).ToList();
                int selected = dept[ddlDepartment.SelectedIndex].DEPARTMENT_ID;

                List<TBL_TEACHER_ASSIGN> tsn = (await App.firebaseDatabase.Child("TBL_TEACHER_ASSIGN").OnceAsync<TBL_TEACHER_ASSIGN>()).Select(x => new TBL_TEACHER_ASSIGN
                {
                    TEACHER_ASSIGN_ID = x.Object.TEACHER_ASSIGN_ID,
                    TEACHER_FID = x.Object.TEACHER_FID,
                    CLASS_FID = x.Object.CLASS_FID

                }).ToList();
                int selected1 = tsn[ddlTeacherAssign.SelectedIndex].TEACHER_ASSIGN_ID;

                var StoredImageURL = await App.FirebaseStorage
                            .Child("TEACHER_IMAGE")
                            .Child(NewID.ToString()+"_"+txtTeName.Text + ".jpg")
                            .PutAsync(_mediaFile.GetStream());



        TBL_TEACHER t = new TBL_TEACHER()
                {   TEACHER_ID= NewID,
                    TEACHER_NAME = txtTeName.Text,
                    TEACHER_EMAIL = txtTeEmail.Text,
                    TEACHER_PASSWORD = txtTePassword.Text,
                    TEACHER_PHNO = txtTePhone.Text,
                    TEACHER_ADDRESS = txtTeAddress.Text,
                    DEPARTMENT_FID=selected,
                    TEACHER_ASSIGN_FID=selected1,
                   TEACHER_IMAGE= StoredImageURL,
                };

                // App.db.Insert(u);
                await App.firebaseDatabase.Child("TBL_TEACHER").PostAsync(t);

                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Teacher Added", "ok");
               

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }

        }
    }
}