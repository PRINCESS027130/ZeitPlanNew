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
using ZeitPlan.Views.Admin;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public TeacherSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new TeacherSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class TeacherSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<TeacherSideBarFlyoutMenuItem> MenuItems { get; set; }

            public TeacherSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<TeacherSideBarFlyoutMenuItem>(new[]
                {
                   new TeacherSideBarFlyoutMenuItem { Id = 0,Icon="icon_feed.png", Title = "Add Teacher",TargetType=typeof(Add_Teachers) },
                    new TeacherSideBarFlyoutMenuItem { Id = 1,Icon="icon_feed.png", Title = "Add Student",TargetType=typeof(Add_Student) },
                    new TeacherSideBarFlyoutMenuItem { Id = 2,Icon="icon_feed.png", Title = "Add Degree",TargetType=typeof(Add_Degree) },
                    new TeacherSideBarFlyoutMenuItem { Id = 3,Icon="icon_feed.png", Title = "Add Department",TargetType=typeof(Add_Department) },
                    new TeacherSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add Class",TargetType=typeof(Add_Class) },
                    new TeacherSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add Course",TargetType=typeof(Add_Course) },
                    new TeacherSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add Course Assign",TargetType=typeof(Add_Course_Assign) },
                    new TeacherSideBarFlyoutMenuItem{ Id = 4,Icon="icon_feed.png", Title = "Add Room",TargetType=typeof(Add_Room) },
                    new TeacherSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add TimeTable",TargetType=typeof(Add_TimeTable) },
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