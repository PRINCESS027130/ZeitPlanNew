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
    public partial class Degree_Detail : ContentPage
    {
        public Degree_Detail(TBL_DEGREE deg)
        {
            InitializeComponent();
             lblDegreeId.Text = deg.DEGREE_ID.ToString();
            lblDegreeName.Text = deg.DEGREE_NAME;
            lblDepartmentFID.Text = deg.DEPARTMENT_FID.ToString();
          


        }
    }
}