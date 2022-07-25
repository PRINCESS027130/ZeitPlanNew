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
    public partial class Add_Department : ContentPage
    {
        public Add_Department()
        {
            InitializeComponent();
        }

        private async void btnDepartment_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDName.Text) || string.IsNullOrEmpty(txtDTBL_DegreeFID.Text) || string.IsNullOrEmpty(txtDTBL_RoomFID.Text) || string.IsNullOrEmpty(txtDTBL_TeacherFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                App.db.CreateTable<TBL_DEPARTMENT>();


                TBL_DEPARTMENT d = new TBL_DEPARTMENT()
                {

                    DEPARTMENT_NAME =txtDName.Text,
                    TBL_DEGREEFID= int.Parse(txtDTBL_DegreeFID.Text),
                    TBL_ROOMFID = int.Parse(txtDTBL_RoomFID.Text),
                    TBL_TEACHERFID = int.Parse(txtDTBL_TeacherFID.Text),

                };

                App.db.Insert(d);
                await DisplayAlert("Success", "Department Added", "ok");


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
}