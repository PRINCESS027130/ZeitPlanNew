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
    public partial class Course_Detail : ContentPage
    {
        public Course_Detail(TBL_COURSE Co)
        {
            InitializeComponent();
             lblCourseId.Text = Co.COURSE_ID.ToString();
            lblCourseName.Text = Co.COURSE_NAME;
            lblCreditHours.Text = Co.CREDIT_HOURS;
            lblDegreeFID.Text = Co.DEGREE_FID.ToString();
            lblCourseAssignFID.Text = Co.COURSE_ASSIGNFID.ToString();
            


        }
    }
}