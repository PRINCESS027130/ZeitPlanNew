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
    public partial class Add_Room : ContentPage
    {
        public Add_Room()
        {
            InitializeComponent();
        }

        private async void btnRoom_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRoom_No.Text) || string.IsNullOrEmpty(txtRDepartmentFID.Text) || string.IsNullOrEmpty(txtRtblDepartmentFID.Text) || string.IsNullOrEmpty(txtRtblTimeTableFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                App.db.CreateTable<TBL_ROOM>();


                TBL_ROOM r = new TBL_ROOM()
                {

                    ROOM_NO = txtRoom_No.Text,
                    DEPARTMENT_FID = int.Parse(txtRDepartmentFID.Text),
                     TBL_DEPARTMENTFID = int.Parse(txtRtblDepartmentFID.Text),
                    TBL_TIMETABLEFID = int.Parse(txtRtblTimeTableFID.Text),
                   
                };

                App.db.Insert(r);
                await DisplayAlert("Success", "Room Added", "ok");


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
}