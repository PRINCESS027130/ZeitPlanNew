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
    public partial class UserProfile : ContentPage
    {
        public UserProfile(user u)
        {
            InitializeComponent();
            lblID.Text = u.Userid.ToString();
            lblName.Text = u.Name;
            lblEmail.Text = u.Email;
            lblPhone.Text = u.Phone;
            lblPassword.Text = u.Password;

        }
    }
}