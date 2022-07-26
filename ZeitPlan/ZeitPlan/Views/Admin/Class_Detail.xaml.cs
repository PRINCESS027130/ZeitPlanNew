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
    public partial class Class_Detail : ContentPage
    {
        public Class_Detail(TBL_CLASS Cl)
        {
            InitializeComponent();
             lblClassId.Text = Cl.CLASS_ID.ToString();
            lblSESSION.Text = Cl.SESSION;
            lblSECTION.Text = Cl.SECTION;
            lblSHIFT.Text = Cl.SHIFT;
            lblDegreeFID.Text = Cl.DEGREE_FID.ToString();
           
            


        }
    }
}