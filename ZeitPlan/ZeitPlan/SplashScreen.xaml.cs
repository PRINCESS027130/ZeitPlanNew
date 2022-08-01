using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
            Animation();
        }

        public async void Animation()
        {
            ssImage.Opacity = 0;

            await Task.WhenAll(
                ssImage.FadeTo(100, 3000),
                ssImage.ScaleTo(1.3, 3000)
                );

            Application.Current.MainPage = new StartPage();
        }
    }
}