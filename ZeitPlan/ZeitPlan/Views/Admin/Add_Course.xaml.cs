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
    public partial class Add_Course : ContentPage
    {
        public Add_Course()
        {
            InitializeComponent();
        }

        private async void btnCourse_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCOName.Text) || string.IsNullOrEmpty(txtCOCredit_Hours.Text) || string.IsNullOrEmpty(txtCODegreeFID.Text) || string.IsNullOrEmpty(txtCOTBL_Course_AssignFID.Text) || string.IsNullOrEmpty(txtCOTBL_DegreeFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                App.db.CreateTable<TBL_COURSE>();

                TBL_COURSE co = new TBL_COURSE()
                {

                    COURSE_NAME= txtCOName.Text,
                    CREDIT_HOURS = txtCOCredit_Hours.Text,
                    DEGREE_FID=int.Parse(txtCODegreeFID.Text),
                    TBL_COURSE_ASSIGNFID = int.Parse(txtCOTBL_Course_AssignFID.Text),
                    TBL_DEGREEFID = int.Parse(txtCOTBL_DegreeFID.Text),

                };

                App.db.Insert(co);
                await DisplayAlert("Success", "Course Added", "ok");


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
}