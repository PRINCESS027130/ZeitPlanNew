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
    public partial class Add_Degree : ContentPage
    {
        public Add_Degree()
        {
            InitializeComponent();
        }

        private async void btnDegree_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDegName.Text) || string.IsNullOrEmpty(txtDegDepartmentFID.Text) || string.IsNullOrEmpty(txtDegTBL_ClassFID.Text) || string.IsNullOrEmpty(txtDegTBL_CourseAssignFID.Text) || string.IsNullOrEmpty(txtDegTBL_CourseFID.Text) || string.IsNullOrEmpty(txtDegTBL_DepartmentFID.Text))
                {
                    await DisplayAlert("ERROR", "Please fill the required field", "ok");
                    return;
                }

                App.db.CreateTable<TBL_DEGREE>();

                TBL_DEGREE deg = new TBL_DEGREE()
                {

                    DEGREE_NAME = txtDegName.Text,
                    DEPARTMENT_FID = int.Parse(txtDegDepartmentFID.Text),
                    TBL_CLASSFID = int.Parse(txtDegTBL_ClassFID.Text),
                    TBL_COURSE_ASSIGNFID = int.Parse(txtDegTBL_CourseAssignFID.Text),
                    TBL_COURSEFID = int.Parse(txtDegTBL_CourseFID.Text),
                    TBL_DEPARTMENTFID = int.Parse(txtDegTBL_DepartmentFID.Text),

                };

                App.db.Insert(deg);
                await DisplayAlert("Success", "Degree Added", "ok");


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Somethimg went wrong,Please try again later\nError:" + ex.Message, "ok");

            }
        }
    }
}