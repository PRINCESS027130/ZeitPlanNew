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
    public partial class Room_Detail : ContentPage
    {
        public Room_Detail(TBL_ROOM r)
        {
            InitializeComponent();
             lblRoomId.Text = r.ROOM_ID.ToString();
            lblRoom_no.Text = r.ROOM_NO;
            lblDepartmentFID.Text = r.DEPARTMENT_FID.ToString();
           lblTimeTableFID.Text = r.TIMETABLE_FID.ToString();
      
            

        }
    }
}