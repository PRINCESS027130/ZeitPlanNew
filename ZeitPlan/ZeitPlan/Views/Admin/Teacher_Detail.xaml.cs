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
    public partial class Teacher_Detail : ContentPage
    {
        public Teacher_Detail(TBL_TEACHER t)
        {
            InitializeComponent();
             lblTeacherId.Text = t.TEACHER_ID.ToString();
            lblTeacherName.Text = t.TEACHER_NAME;
            lblTeacherEmail.Text = t.TEACHER_EMAIL.ToString();
            lblTeacherPassword.Text = t.TEACHER_PASSWORD;
            lblTeacherphno.Text = t.TEACHER_PASSWORD;
            lblTeacherAddress.Text = t.TEACHER_PASSWORD;
            lblDepartmentFID.Text = t.DEPARTMENT_FID.ToString();
            


        }
    }
}