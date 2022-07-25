using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public AdminSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new AdminSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class AdminSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AdminSideBarFlyoutMenuItem> MenuItems { get; set; }

            public AdminSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AdminSideBarFlyoutMenuItem>(new[]
                {
                    new AdminSideBarFlyoutMenuItem { Id = 0,Icon="icon_feed.png", Title = "Add Teacher",TargetType=typeof(Add_Teachers) },
                    new AdminSideBarFlyoutMenuItem { Id = 1,Icon="icon_feed.png", Title = "Add Student",TargetType=typeof(Add_Student) },
                    new AdminSideBarFlyoutMenuItem { Id = 2,Icon="icon_feed.png", Title = "Add Degree",TargetType=typeof(Add_Degree) },
                    new AdminSideBarFlyoutMenuItem { Id = 3,Icon="icon_feed.png", Title = "Add Department",TargetType=typeof(Add_Department) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add Class",TargetType=typeof(Add_Class) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add Course",TargetType=typeof(Add_Course) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add Course Assign",TargetType=typeof(Add_Course_Assign) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add Room",TargetType=typeof(Add_Room) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add TimeTable",TargetType=typeof(Add_TimeTable) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}