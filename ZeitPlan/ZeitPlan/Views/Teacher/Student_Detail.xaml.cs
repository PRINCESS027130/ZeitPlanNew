using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Student_Detail : ContentPage
    {
        public Student_Detail(TBL_STUDENT s)
        {
            InitializeComponent();
             lblStudentId.Text = s.STUDENT_ID.ToString();
            lblStudentName.Text = s.STUDENT_NAME;
            lblStudentEmail.Text = s.STUDENT_EMAIL.ToString();
           lblStudentPassword.Text = s.STUDENT_PASSWORD;
            lblClassFID.Text = s.CLASS_FID.ToString();
           
            


        }
    }
}