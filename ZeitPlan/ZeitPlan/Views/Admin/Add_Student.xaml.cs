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
    public partial class Add_Student : ContentPage
    {
        public Add_Student()
        {
            InitializeComponent();
        }

        private async void btnStudent_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtstdName.Text) || string.IsNullOrEmpty(txtstdEmail.Text) || string.IsNullOrEmpty(txtstdPassword.Text) || string.IsNullOrEmpty(txtstdClassFID.Text) || string.IsNullOrEmpty(txtstdTBLClassFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                App.db.CreateTable<TBL_STUDENT>();
                var check = App.db.Table<TBL_STUDENT>().FirstOrDefault(x => x.STUDENT_EMAIL == txtstdEmail.Text);
                if (check != null)
                {
                    await DisplayAlert("ERROR", "Email already registered", "ok");
                    return;
                }


                TBL_STUDENT std = new TBL_STUDENT()
                {

                    STUDENT_NAME = txtstdName.Text,
                    STUDENT_EMAIL = txtstdEmail.Text,
                    STUDENT_PASSWORD = txtstdPassword.Text,
                    CLASS_FID = int.Parse(txtstdClassFID.Text),
                    TBL_CLASSFID = int.Parse(txtstdTBLClassFID.Text)
                };

                App.db.Insert(std);
                await DisplayAlert("Success", "Student Added", "ok");


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
}