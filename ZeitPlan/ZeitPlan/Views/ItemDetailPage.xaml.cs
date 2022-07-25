using System.ComponentModel;
using Xamarin.Forms;
using ZeitPlan.ViewModels;

namespace ZeitPlan.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}