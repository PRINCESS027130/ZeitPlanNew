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
    public partial class Department_Detail : ContentPage
    {
        public Department_Detail(TBL_DEPARTMENT d)
        {
            InitializeComponent();
             lblDepartmntId.Text = d.DEPARTMENT_ID.ToString();
            lblDepartmentName.Text = d.DEPARTMENT_NAME;
            lblDegreeFID.Text = d.DEGREEFID.ToString();
           lblRoomFID.Text = d.ROOMFID.ToString();
            lblTeacherFID.Text = d.TEACHERFID.ToString();
            


        }
    }
}