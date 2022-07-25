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
    public partial class Add_Class : ContentPage
    {
        public Add_Class()
        {
            InitializeComponent();
        }

        private async void btnClass_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCSession.Text) || string.IsNullOrEmpty(txtCSection.Text) || string.IsNullOrEmpty(txtCShift.Text) || string.IsNullOrEmpty(txtCDegreeFID.Text) || string.IsNullOrEmpty(txtCTblStudentFID.Text) || string.IsNullOrEmpty(txtCTblTimeTableFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                App.db.CreateTable<TBL_CLASS>();
                

                TBL_CLASS cl = new TBL_CLASS()
                {

                    SESSION = txtCSession.Text,
                    SECTION = txtCSection.Text,
                    SHIFT = txtCShift.Text,
                    DEGREE_FID = int.Parse(txtCDegreeFID.Text),
                    TBL_DEGREEFID = int.Parse(txtCTblDegreeFID.Text),
                    TBL_STUDENTFID = int.Parse(txtCTblStudentFID.Text),
                    TBL_TIMETABLEFID = int.Parse(txtCTblTimeTableFID.Text)
                };

                App.db.Insert(cl);
                await DisplayAlert("Success", "Class Added", "ok");


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
    
}