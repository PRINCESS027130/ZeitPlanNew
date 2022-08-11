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
    public partial class TimeTable_Detail : ContentPage
    {
        public TimeTable_Detail(TBL_TIMETABLE tb)
        {
            InitializeComponent();
             lblTimeTableId.Text = tb.TIMETABLE_ID.ToString();
            lblCourseFID.Text = tb.COURSE_FID.ToString();
            lblClassFID.Text = tb.CLASS_FID.ToString();
            lblTeacherFID.Text = tb.TEACHER_FID.ToString();
            lblRoomFID.Text = tb.ROOM_FID.ToString();
           // lblSlot.Text = tb.SLOT_FID.ToString();
           
        }
    }
}