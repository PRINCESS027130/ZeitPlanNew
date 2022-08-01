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
                if (string.IsNullOrEmpty(txtTeName.Text) || string.IsNullOrEmpty(txtTePhone.Text) || string.IsNullOrEmpty(txtTeEmail.Text) || string.IsNullOrEmpty(txtTePassword.Text) || string.IsNullOrEmpty(txtTeDepartmentFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
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
                    DEPARTMENT_FID=int.Parse(txtTeDepartmentFID.Text),
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