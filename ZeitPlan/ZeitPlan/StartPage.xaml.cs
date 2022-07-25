using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.LoginSystem;
using ZeitPlan.Views.Admin;

namespace ZeitPlan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void btnGetStarted_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new login("Student"));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
        }

        private void btnAdmin_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new login("Admin");
        }

        private void Student_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new login("Student"));
        }

        private void Teacher_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new login("Teacher"));
        }
    }
}