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
    public partial class TeacherAssign_Detail : ContentPage
    {
        public TeacherAssign_Detail(TBL_TEACHER_ASSIGN t)
        {
            InitializeComponent();
             lblTeacherAssignId.Text = t.TEACHER_ASSIGN_ID.ToString();
            lblTeacherFID.Text = t.TEACHER_FID.ToString();
            lblClassFID.Text = t.CLASS_FID.ToString();
            


        }
    }
}