﻿using System;
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
                    new AdminSideBarFlyoutMenuItem { Id = 0,Icon="icon_feed.png", Title = "Manage Teacher",TargetType=typeof(Manage_Teacher) },
                    new AdminSideBarFlyoutMenuItem { Id = 0,Icon="icon_feed.png", Title = "Add TeacherAssign",TargetType=typeof(Add_TeacherAssign) },
                    new AdminSideBarFlyoutMenuItem { Id = 0,Icon="icon_feed.png", Title = "Manage TeacherAssign",TargetType=typeof(Manage_TeacherAssign) },
                    new AdminSideBarFlyoutMenuItem { Id = 2,Icon="icon_feed.png", Title = "Add Degree",TargetType=typeof(Add_Degree) },
                    new AdminSideBarFlyoutMenuItem { Id = 2,Icon="icon_feed.png", Title = "Manage Degree",TargetType=typeof(Manage_Degree) },
                    new AdminSideBarFlyoutMenuItem { Id = 3,Icon="icon_feed.png", Title = "Add Department",TargetType=typeof(Add_Department) },
                    new AdminSideBarFlyoutMenuItem { Id = 3,Icon="icon_feed.png", Title = "Manage Department",TargetType=typeof(Manage_Department) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add Class",TargetType=typeof(Add_Class) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Manage Class",TargetType=typeof(Manage_Class) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add Course",TargetType=typeof(Add_Course) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Manage Course",TargetType=typeof(Manage_Course) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add ClassCourseAssign",TargetType=typeof(Add_ClassCourseAssign) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Manage ClassCourseAssign",TargetType=typeof(Manage_ClassCourseAssign) },

                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add Room",TargetType=typeof(Add_Room) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Manage Room",TargetType=typeof(Manage_Room) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Add TimeTable",TargetType=typeof(Add_TimeTable) },
                    new AdminSideBarFlyoutMenuItem { Id = 4,Icon="icon_feed.png", Title = "Manage TimeTable",TargetType=typeof(Manage_TimeTable) },
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