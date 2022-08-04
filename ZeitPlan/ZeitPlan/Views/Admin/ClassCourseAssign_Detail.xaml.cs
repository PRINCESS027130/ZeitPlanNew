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
    public partial class ClassCourseAssign_Detail : ContentPage
    {
        public ClassCourseAssign_Detail(TBL_CLASS_COURSEASSIGN cco)
        {
            InitializeComponent();
             lblClassCourseAssignId.Text = cco.CLASS_COURSEASSIGN_ID.ToString();
            lblCourseFID.Text = cco.COURSE_FID.ToString();
            lblClassFID.Text = cco.CLASS_FID.ToString();
           
        }
    }
}